using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using JSMCodeChallenge.Models;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JSMCodeChallenge.Repositories;
using Microsoft.AspNetCore.TestHost;
using Serilog;

namespace JSMCodeChallenge.Tests.Fixtures
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Api.Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Api.Startup>()
                        .UseTestServer();
                })
                .ConfigureServices((ctx, service) =>
                {
                    IEnumerable<User> users = Enumerable.Range(0, 100).Select(_ => new User());
                    service.AddSingleton<UserRepository>(_ => new UserRepository(users));
                });
    }
}