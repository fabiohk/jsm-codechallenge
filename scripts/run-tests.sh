#!/bin/bash
echo "[TESTS]: Running Tests with dotnet..."
dotnet test --logger "console;verbosity=detailed"