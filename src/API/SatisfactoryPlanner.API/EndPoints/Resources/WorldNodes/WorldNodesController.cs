using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    [Route("api")]
    public class WorldNodesController(IResourcesModule module) : Controller
    {
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