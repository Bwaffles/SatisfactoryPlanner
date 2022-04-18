using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.TappedNodes
{
    [ApiController]
    [Route("api/resources/tapped-nodes")]
    public class TappedNodesController : Controller
    {
        private readonly IResourcesModule _module;

        public TappedNodesController(IResourcesModule module)
        {
            _module = module;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TapNode([FromBody] TapNodeRequest request)
        {
            await _module.ExecuteCommandAsync(new TapNodeCommand(
                request.NodeId,
                request.ExtractorId,
                request.AmountToExtract,
                request.Name
            ));

            return Ok();
        }
    }
}
