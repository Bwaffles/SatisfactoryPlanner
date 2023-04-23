using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    [ApiController]
    [Route("api")]
    public class NodesController : Controller
    {
        private readonly IResourcesModule _module;

        public NodesController(IResourcesModule module)
        {
            _module = module;
        }

        /// <summary>
        ///     Get nodes in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns the nodes as a list of <see cref="NodeDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetNodes)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/nodes")]
        [ProducesResponseType(typeof(List<NodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNodes([FromRoute] Guid worldId, [FromQuery] GetNodesRequest request)
        {
            var nodes = await _module.ExecuteQueryAsync(new GetNodesQuery(worldId, request.ResourceId));
            return Ok(nodes);
        }

        /// <summary>
        ///     Get the details of a node in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns the node as a <see cref="NodeDetailsDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetNodeDetails)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/nodes/{nodeId}")]
        [ProducesResponseType(typeof(NodeDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNodeDetails([FromRoute] Guid worldId, [FromRoute] Guid nodeId)
        {
            var nodeDetails = await _module.ExecuteQueryAsync(new GetNodeDetailsQuery(worldId, nodeId));
            return Ok(nodeDetails);
        }

        /// <summary>
        ///     Tap the node with an extractor.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.TapNode)]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/tap")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Tap([FromRoute] Guid worldId, [FromRoute] Guid nodeId, [FromBody] TapNodeRequest request)
        {
            await _module.ExecuteCommandAsync(new TapWorldNodeCommand(
                worldId,
                nodeId,
                request.ExtractorId
            ));

            return Ok();
        }

        /// <summary>
        ///     Increase the extraction rate of resources from the node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.IncreaseNodeExtractionRate)]
        [WorldAuthorization(typeof(IncreaseExtractionRateRequest))]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/increase-extraction-rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseExtractionRate([FromRoute]Guid worldId, [FromRoute] Guid nodeId,
            [FromBody] IncreaseExtractionRateRequest request)
        {
            await _module.ExecuteCommandAsync(new IncreaseExtractionRateCommand(
                worldId,
                nodeId,
                request.ExtractionRate
            ));

            return Ok();
        }
    }
}
