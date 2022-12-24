using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.TappedNodes
{
    [ApiController]
    [Route("api/resources/[controller]")]
    public class TappedNodesController : Controller
    {
        private readonly IResourcesModule _module;

        public TappedNodesController(IResourcesModule module)
        {
            _module = module;
        }

        // TODO this should be in the NodeController
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TapNode([FromBody] TapNodeRequest request)
        { // TODO authorization, permissions, return id when working on the tap node ui
            await _module.ExecuteCommandAsync(new TapNodeCommand(
                request.WorldId,
                request.NodeId,
                request.ExtractorId,
                request.AmountToExtract,
                request.Name
            ));

            return Ok();
        }
    }
}
