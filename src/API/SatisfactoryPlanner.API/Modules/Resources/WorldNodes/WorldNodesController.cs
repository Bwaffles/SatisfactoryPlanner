using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    [Route("api")]
    public class WorldNodesController : Controller
    {
        private readonly IResourcesModule _module;

        public WorldNodesController(IResourcesModule module)
        {
            _module = module;
        }

        /// <summary>
        ///     Get nodes in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns the world nodes as a list of <see cref="WorldNodeDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetWorldNodes)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/nodes")]
        [ProducesResponseType(typeof(List<WorldNodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorldNodes([FromRoute] Guid worldId, [FromQuery] GetWorldNodesRequest request)
        {
            var worldNodes = await _module.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, request.ResourceId));
            return Ok(worldNodes);
        }

        /// <summary>
        ///     Get the details of a node in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns the world node as a <see cref="WorldNodeDetailsDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetWorldNodeDetails)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/nodes/{nodeId}")]
        [ProducesResponseType(typeof(WorldNodeDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorldNodeDetails([FromRoute] Guid worldId, [FromRoute] Guid nodeId)
        {
            var worldNodeDetails = await _module.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            return Ok(worldNodeDetails);
        }

        /// <summary>
        ///     Tap the world node with an extractor.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.TapWorldNode)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/tap")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TapWorldNode([FromRoute] Guid worldId, [FromRoute] Guid nodeId, [FromBody] TapWorldNodeRequest request)
        {
            await _module.ExecuteCommandAsync(new TapWorldNodeCommand(
                worldId,
                nodeId,
                request.ExtractorId
            ));

            return Ok();
        }

        /// <summary>
        ///     Increase the extraction rate of resources from the world node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.IncreaseWorldNodeExtractionRate)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/increase-extraction-rate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> IncreaseWorldNodeExtractionRate([FromRoute]Guid worldId, [FromRoute] Guid nodeId,
            [FromBody] IncreaseWorldNodeExtractionRateRequest request)
        {
            await _module.ExecuteCommandAsync(new IncreaseExtractionRateCommand(
                worldId,
                nodeId,
                request.ExtractionRate
            ));

            return NoContent();
        }
    }
}
