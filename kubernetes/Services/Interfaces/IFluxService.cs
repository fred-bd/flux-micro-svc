using k8s.Models;
using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Services.Interfaces;

public interface IFluxService
{
  public Task<V1KustomizationList> GetKustomizations(string ns = "flux-system");
}