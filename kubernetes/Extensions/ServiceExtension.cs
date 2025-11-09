using Microsoft.OpenApi.Models;

using Kubernetes.Services;
using Kubernetes.Services.Interfaces;
using Kubernetes.Configurations;

namespace Kubernetes.Extensions;

public static class ServicesExtensions
{
  public static IServiceCollection RegisterServices(this IServiceCollection services)
  {
    services.AddControllers();

    services.AddScoped<HelloService>();
    services.AddScoped<IKubernetesService, KubernetesService>();

    services.ConfigureSwagger();

    var configurations = LoadConfigurations();
    var kubeconfig = k8s.KubernetesClientConfiguration.BuildConfigFromConfigFile(configurations.GetKubeConfigFile());

    var vaultConfig = LoadConfigurations().GetVaultConfiguration() ?? new VaultConfigParams();

    services.AddSingleton(kubeconfig);
    services.AddSingleton(vaultConfig);

    return services;
  }

  private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Kubernetes Project",
        Version = "v1",
        Description = "Swagger and CORS"
      });
    });
    
    return services;
  }
  

  // public static async Task RunMigrationsAsync(this IServiceProvider provider) => 
  //   await Model.Extensions.ServicesExtensions.RunMigrationsAsync(provider);

  private static IConfiguration LoadConfigurations() => new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>()
    .Build();
}