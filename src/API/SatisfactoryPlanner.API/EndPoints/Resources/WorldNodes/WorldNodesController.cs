using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.UpgradeExtractor;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    [Route("api")]
    public class WorldNodesController(IResourcesModule module) : Controller
    {
        /// <summary>
        ///     Tap the world node with an extractor.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.TapWorldNode)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/tap")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> TapWorldNode([FromRoute] Guid worldId, [FromRoute] Guid nodeId,
            [FromBody] TapWorldNodeRequest request)
        {
            await module.ExecuteCommandAsync(new TapWorldNodeCommand(
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
        public async Task<IActionResult> IncreaseWorldNodeExtractionRate([FromRoute] Guid worldId,
            [FromRoute] Guid nodeId,
            [FromBody] IncreaseWorldNodeExtractionRateRequest request)
        {
            await module.ExecuteCommandAsync(new IncreaseExtractionRateCommand(
                worldId,
                nodeId,
                request.ExtractionRate
            ));

            return NoContent();
        }

        /// <summary>
        ///     Decrease the extraction rate of resources from the world node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.DecreaseWorldNodeExtractionRate)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/decrease-extraction-rate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DecreaseWorldNodeExtractionRate([FromRoute] Guid worldId,
            [FromRoute] Guid nodeId,
            [FromBody] DecreaseWorldNodeExtractionRateRequest request)
        {
            await module.ExecuteCommandAsync(new DecreaseExtractionRateCommand(
                worldId,
                nodeId,
                request.ExtractionRate
            ));

            return NoContent();
        }

        /// <summary>
        ///     Upgrade the extractor used to extract resources from the world node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.UpgradeExtractor)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/upgrade-extractor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpgradeExtractor([FromRoute] Guid worldId,
            [FromRoute] Guid nodeId,
            [FromBody] UpgradeExtractorRequest request)
        {
            await module.ExecuteCommandAsync(new UpgradeExtractorCommand(
                worldId,
                nodeId,
                request.ExtractorId
            ));

            return NoContent();
        }

        /// <summary>
        ///     Downgrade the extractor used to extract resources from the world node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.DowngradeExtractor)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/downgrade-extractor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DowngradeExtractor([FromRoute] Guid worldId,
            [FromRoute] Guid nodeId,
            [FromBody] DowngradeExtractorRequest request)
        {
            await module.ExecuteCommandAsync(new DowngradeExtractorCommand(
                worldId,
                nodeId,
                request.ExtractorId
            ));

            return NoContent();
        }

        /// <summary>
        ///     Dismantle the extractor used to extract resources from the world node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.DismantleExtractor)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/nodes/{nodeId}/dismantle-extractor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DismantleExtractor([FromRoute] Guid worldId, [FromRoute] Guid nodeId)
        {
            await module.ExecuteCommandAsync(new DismantleExtractorCommand(
                worldId,
                nodeId
            ));

            return NoContent();
        }
    }
}