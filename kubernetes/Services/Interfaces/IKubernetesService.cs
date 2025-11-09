using k8s.Models;
using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Services.Interfaces;

public interface IKubernetesService
{
  public Task<V1NamespaceList> GetNamespaces();
  public Task<V1KustomizationList> GetKustomizations();
}