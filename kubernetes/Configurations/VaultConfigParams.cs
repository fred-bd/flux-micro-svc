using Kubernetes.Enums;

namespace Kubernetes.Configurations;

public class VaultConfigParams
{
  public VaultConfigParams()
  {
    Address = string.Empty;
    Method = VaultMethods.Approle;
    ApproleId = string.Empty;
    SecretId = string.Empty;
  }

  public string Address { get; set; }
  public VaultMethods Method { get; set; }

  public string ApproleId { get; set; }
  public string SecretId { get; set; }
}