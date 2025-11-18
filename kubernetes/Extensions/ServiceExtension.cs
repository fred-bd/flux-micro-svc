using Microsoft.OpenApi.Models;

using Kubernetes.Services;
using Kubernetes.Services.Interfaces;
using Kubernetes.Repository.Interfaces;
using Kubernetes.Repository;
using k8s;
using Kubernetes.Repository.Wrappers;

namespace Kubernetes.Extensions;

public static class ServicesExtensions
{
  public static IServiceCollection RegisterServices(this IServiceCollection services)
  {
    services.AddControllers();
    services.ConfigureSwagger();
    return ConfigureFluxServices(services);
  }

  private static IServiceCollection ConfigureFluxServices(this IServiceCollection services)
  {
    services.AddScoped<IFluxRepository, FluxRepository>();
    services.AddScoped<IFluxService, FluxService>();
    services.AddScoped<IClientWrapper, GenericClientWrapper>();

    var configurations = LoadConfigurations();
    var kubeconfig = KubernetesClientConfiguration.BuildConfigFromConfigFile(configurations.GetKubeConfigFile());
    var kubeclient = new k8s.Kubernetes(kubeconfig);

    services.AddSingleton<IKubernetes>(kubeclient);

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