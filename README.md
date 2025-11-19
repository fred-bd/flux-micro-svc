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

### Running sonarqube
```sh
docker run -d --rm --name=agent1 \
  --network dev-network \
  -v $PROJECTS_HOST_DIR/profile-app/flux-micro-svc:/home/jenkins/flux-svc \
  -p 2223:22 \
  dotnet-sonar-agent:0.0.1


token squ_ff82c3325d94b8c63fe1f68259d11f624551aef6

dotnet sonarscanner begin `
  /k:"Flux backend" `
  /d:sonar.host.url="http://localhost:9000" `
  /d:sonar.login="squ_ff82c3325d94b8c63fe1f68259d11f624551aef6"
  /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"

dotnet build

dotnet sonarscanner end /d:sonar.login="squ_ff82c3325d94b8c63fe1f68259d11f624551aef6"


dotnet sonarscanner begin \
  /k:"mycompany.myapp" \
  /d:sonar.host.url="http://localhost:9000" \
  /d:sonar.login="squ_ff82c3325d94b8c63fe1f68259d11f624551aef6" \
  /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"

dotnet build

dotnet test \
  /p:CollectCoverage=true \
  /p:CoverletOutputFormat=opencover

dotnet sonarscanner end /d:sonar.login="squ_ff82c3325d94b8c63fe1f68259d11f624551aef6"

```