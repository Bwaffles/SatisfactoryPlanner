using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    [ApiController]
    [Route("api/resources/nodes")]
    public class NodesController : Controller
    {
        private readonly IResourcesModule _module;

        public NodesController(IResourcesModule module)
        {
            _module = module;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<NodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNodes([FromQuery] GetNodesRequest request)
        {
            var nodes = await _module.ExecuteQueryAsync(new GetNodesQuery(request.ResourceId));
            return Ok(nodes);
        }
    }
}
