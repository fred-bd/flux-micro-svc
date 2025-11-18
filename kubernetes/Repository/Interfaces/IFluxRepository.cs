using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Repository.Interfaces;

public interface IFluxRepository
{
  Task<V1Kustomization> GetAsync(string ns, string rscName);
  Task<V1KustomizationList> GetListAsync(string ns);
}