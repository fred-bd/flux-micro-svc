using Kubernetes.Repository.Interfaces;
using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;

namespace Kubernetes.Repository;

public class FluxRepository(IClientWrapper wrapper) : IFluxRepository
{
  private readonly IClientWrapper _wrapper = wrapper;

  public async Task<V1Kustomization> GetAsync(string ns, string rscName) =>
    await _wrapper.GetAsync(ns, rscName);

  public async Task<V1KustomizationList> GetListAsync(string ns) =>
    await _wrapper.GetListAsync(ns);
}