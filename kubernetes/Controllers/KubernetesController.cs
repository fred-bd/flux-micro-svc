using Microsoft.AspNetCore.Mvc;
using Kubernetes.Services.Interfaces;

namespace Kubernetes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KubernetesController(IFluxService fluxService) : ControllerBase
{
  private readonly IFluxService _fluxService = fluxService;

  [HttpGet("kustomizations")]
  public async Task<ActionResult<IList<string>>> GetKustomizationListNames([FromQuery] string? name)
  {
    var nsList = new List<string>();
    var itemList = await _fluxService.GetKustomizations(name ?? "");

    foreach (var item in itemList.Items ?? [])
    {
      if (item.Metadata is not null)
      {
        nsList.Add(item.Metadata.Name);
      }
    }

    return Ok(nsList);
  }
}
