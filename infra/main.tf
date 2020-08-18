terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "~>2.20.0"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name = "Codechallenge"
  location = "South Central US"

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_virtual_network" "vnet" {
  name = "codechallenge-network"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  address_space = ["10.0.0.0/16"]

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_subnet" "appgw" {
  name = "appgw"
  resource_group_name = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name

  address_prefixes = ["10.0.1.0/24"]
}

resource "azurerm_subnet" "container" {
  name = "container"
  resource_group_name = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name

  address_prefixes = ["10.0.2.0/24"]
  delegation {
    name = "container-delegation"

    service_delegation {
      name = "Microsoft.ContainerInstance/containerGroups"
      actions = ["Microsoft.Network/virtualNetworks/subnets/action"]
    }
  }
}

resource "azurerm_public_ip" "application_gw" {
  name = "codechallenge-publicip"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku = "Standard"
  allocation_method = "Static"
  domain_name_label = "codechallenge"

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_network_profile" "container_np" {
  name = "containers-networkprofile"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  container_network_interface {
    name = "network-interface"

    ip_configuration {
      name = "ip-configuration"
      subnet_id = azurerm_subnet.container.id
    }
  }

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_container_registry" "acr" {
  name = "codechallenge"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku = "Basic"
  admin_enabled = true

  provisioner "local-exec" {
    command = "az acr build -t codechallenge:latest -r codechallenge ."
  }

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_container_group" "app" {
  name = "codechallenge"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  os_type = "Linux"
  ip_address_type = "Private"
  network_profile_id = azurerm_network_profile.container_np.id
  restart_policy = "Never"

  image_registry_credential {
    server = azurerm_container_registry.acr.login_server
    username = azurerm_container_registry.acr.admin_username
    password = azurerm_container_registry.acr.admin_password
  }

  container {
    name = "web"
    image = "${azurerm_container_registry.acr.login_server}/codechallenge:latest"
    commands = ["dotnet", "JSMCodeChallenge.dll"]

    ports {
      port = 80
      protocol = "TCP"
    }

    cpu = 0.5
    memory = 1.5
  }

  tags = {
    purpose = "testing"
  }
}

resource "azurerm_application_gateway" "appgw" {
  name = "app-gateway"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    name = "Standard_v2"
    tier = "Standard_v2"
    capacity = 2
  }

  backend_address_pool {
    name = "codechallenge-backend-pool"
    ip_addresses = [azurerm_container_group.app.ip_address]
  }

  backend_http_settings {
    name = "codechallenge-http-settings"
    cookie_based_affinity = "Disabled"
    pick_host_name_from_backend_address = true
    port = 80
    protocol = "Http"
    request_timeout = 60
    probe_name = "codechallenge-probe"
  }

  frontend_ip_configuration {
    name = "codechallenge-ipconfiguration"
    public_ip_address_id = azurerm_public_ip.application_gw.id
  }

  frontend_port {
    name = "codechallenge-feport"
    port = 80
  }

  gateway_ip_configuration {
    name = "challenge"
    subnet_id = azurerm_subnet.appgw.id
  }

  http_listener {
    name = "codechallenge-listener"
    frontend_ip_configuration_name = "codechallenge-ipconfiguration"
    frontend_port_name = "codechallenge-feport"
    protocol = "Http"
  }

  request_routing_rule {
    name = "codechallenge-routing-rule"

    rule_type = "Basic"
    http_listener_name = "codechallenge-listener"
    backend_address_pool_name = "codechallenge-backend-pool"
    backend_http_settings_name = "codechallenge-http-settings"
  }

  probe {
    name = "codechallenge-probe"

    path = "/"
    protocol = "Http"
    timeout = 30
    interval = 86400
    unhealthy_threshold = 3
    pick_host_name_from_backend_http_settings = true

    match {
      status_code = [404]
    }
  }

  tags = {
    purpose = "testing"
  }
}