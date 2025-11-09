using Microsoft.AspNetCore.Mvc;
using Kubernetes.Models;
using Kubernetes.Services;
using Kubernetes.Services.Interfaces;
using System.Threading.Tasks;
using k8s.Models;

namespace Kubernetes.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HelloController(HelloService helloService, IKubernetesService kubernetesService) : ControllerBase
  {
    private readonly HelloService _helloService = helloService;
    private readonly IKubernetesService _kubernetesService = kubernetesService;

    [HttpGet("{name}")]
    public async Task<ActionResult<IList<string>>> Get(string name)
    {
      var nsList = new List<string>();
      var itemList = await _kubernetesService.GetKustomizations();

      foreach (var item in itemList.Items ?? [])
      {
        if (item.Metadata is not null)
        {
          nsList.Add(item.Metadata.Name);
        }
      }

      return Ok(nsList);
    }

    [HttpPost]
    public ActionResult<string> Post([FromBody] HelloRequest request)
    {
      var message = _helloService.GetMessage(request.Name);
      return Ok(new { message });
    }
  }
}
