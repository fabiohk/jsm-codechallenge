ARG PROJECT_NAME=JSMCodeChallenge
ARG PROJECT_SRC=./src/${PROJECT_NAME}

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
ARG PROJECT_SRC

# Copy csproj and restore as distinct layers
COPY ${PROJECT_SRC}/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ${PROJECT_SRC} ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ARG PROJECT_NAME
ENV PROJECT_DLL="${PROJECT_NAME}.dll"

COPY --from=build-env /app/out .
ENTRYPOINT dotnet ${PROJECT_DLL}