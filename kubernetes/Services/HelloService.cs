using Kubernetes.Configurations;

namespace Kubernetes.Services;
public class HelloService(VaultConfigParams p)
{
  private readonly VaultConfigParams _params = p;

  public string GetMessage(string name)
  {
      return $"Olá, {name}! Bem-vindo à API .NET 8! Vault in {_params.ApproleId}";
  }
}
