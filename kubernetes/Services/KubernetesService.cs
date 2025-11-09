using Kubernetes.Services.Interfaces;

using k8s;
using k8s.Models;

using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Services;

public class KubernetesService(KubernetesClientConfiguration configuration) : IKubernetesService
{
  private readonly k8s.Kubernetes _client = new k8s.Kubernetes(configuration);

  public async Task<V1NamespaceList> GetNamespaces() => await _client.CoreV1.ListNamespaceAsync();

  public async Task<V1KustomizationList> GetKustomizations()
  {
    var generic = new GenericClient(_client, "kustomize.toolkit.fluxcd.io", "v1", "kustomizations");
    return await generic.ListNamespacedAsync<V1KustomizationList>("flux-system");

    // var test = await _client.ListNamespacedCustomObjectAsync<
    
    // .CustomObjects.ListClusterCustomObjectAsync<V1Kustomization>();

  }
}