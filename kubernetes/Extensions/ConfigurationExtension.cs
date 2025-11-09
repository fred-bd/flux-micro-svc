using Kubernetes.Configurations;

namespace Kubernetes.Extensions;

public static class ConfigurationExtensions
{
  public static VaultConfigParams? GetVaultConfiguration(this IConfiguration configuration) =>
    configuration.GetSection("VaultConfig").Get<VaultConfigParams>();
      
  public static string GetKubeConfigFile(this IConfiguration configuration) =>
    configuration.GetSection("KubeConfigFile").Value ?? string.Empty;
}