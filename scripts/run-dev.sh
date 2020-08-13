#!/bin/bash
PROJECT_NAME=JSMCodeChallenge
CSPROJ_PATH=./src/${PROJECT_NAME}/${PROJECT_NAME}.csproj

echo "[PRE-RUN]: Watching and running project..."
dotnet watch --project ${CSPROJ_PATH} run --urls=${ASPNETCORE_URLS}