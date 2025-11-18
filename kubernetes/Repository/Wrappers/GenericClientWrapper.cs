using k8s;
using Kubernetes.Repository.Interfaces;
using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Repository.Wrappers;

public class GenericClientWrapper(IKubernetes kubeClient) : IClientWrapper
{
  private readonly IKubernetes _kubernetes = kubeClient;

  public async Task<V1Kustomization> GetAsync(string ns, string resourceName)
  {
    var client = GetGenericClient("kustomize.toolkit.fluxcd.io", "v1", "kustomizations");
    return await client.ReadNamespacedAsync<V1Kustomization>(ns, resourceName);
  }

  public async Task<V1KustomizationList> GetListAsync(string ns)
  {
    var client = GetGenericClient("kustomize.toolkit.fluxcd.io", "v1", "kustomizations");
    return (await client.ListNamespacedAsync<V1KustomizationList>(ns)) ?? new V1KustomizationList();
  }

  protected GenericClient GetGenericClient(string group, string version, string plural) =>
    new (_kubernetes, group, version, plural);
}