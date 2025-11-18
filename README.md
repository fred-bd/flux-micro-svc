### Creating the code using dotnet cli

```sh
# Creating the grpc project
dotnet new webapi -o kubernetes

# git ignore
dotnet new gitignore -o .

# Tests
dotnet new xunit -o unittests

# Solution
dotnet new sln -o .
dotnet sln add kubernetes
dotnet sln add unittests

# Configuring a secret to be used by projects
dotnet user-secrets init --project kubernetes
cat secret.json | dotnet user-secrets set --project kubernetes

# Adding swagger
dotnet add kubernetes package Swashbuckle.AspNetCore
dotnet add kubernetes package KubernetesCRDModelGen.Models.fluxcd.io

# Relating projects and unittest
dotnet add unittests reference kubernetes

```