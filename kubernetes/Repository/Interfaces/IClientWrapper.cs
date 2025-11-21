using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Repository.Interfaces;

public interface IClientWrapper
{
  Task<V1Kustomization> GetAsync(string ns, string resourceName);
  Task<V1KustomizationList> GetListAsync(string ns);
}