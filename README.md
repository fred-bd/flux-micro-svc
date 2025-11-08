# Creating the grpc project
dotnet new webapi -o kubernetes

# git ignore
dotnet new gitignore -o .

# Model
dotnet new classlib -o model 
dotnet add model package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add model package Microsoft.EntityFrameworkCore.Design
dotnet add model package Microsoft.EntityFrameworkCore.Relational

# Tests
dotnet new xunit -o unittests

# Solution
dotnet new sln -o .
dotnet sln add model
dotnet sln add unittests
dotnet sln add grpc-service

# Install ef tools
dotnet tool install --global dotnet-ef 

# setting migrations
dotnet ef migrations add InitialCreate -p model -s grpc-service

# Some dev tools
dotnet user-secrets init --project grpc-service
cat secret.json | dotnet user-secrets set --project grpc-service

# Adding swagger
dotnet add kubernetes package Swashbuckle.AspNetCore
dotnet add kubernetes package KubernetesCRDModelGen.Models.fluxcd.io