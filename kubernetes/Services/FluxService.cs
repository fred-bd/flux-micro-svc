using Kubernetes.Services.Interfaces;

using KubernetesCRDModelGen.Models.kustomize.toolkit.fluxcd.io;
using Kubernetes.Repository.Interfaces;

namespace Kubernetes.Services;

public class FluxService(IFluxRepository repository) : IFluxService
{
  private readonly IFluxRepository _fluxRepository = repository;

  public async Task<V1KustomizationList> GetKustomizations(string ns = "flux-system") =>
    await _fluxRepository.GetListAsync(ns);
}