using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class DismantleExtractor(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.DismantleExtractor)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/dismantle-extractor")]
        [SwaggerOperation(
            Summary = "Dismantle the extractor used to extract resources from the world node.",
            Description = "Dismantle the extractor used to extract the resources from the world node. " +
                            "After it's been dismantled, the node will be untapped. " +
                            "To start extracting resources from it again, you will need to tap the node again.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(204)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromRoute] Guid nodeId)
        {
            await module.ExecuteCommandAsync(new DismantleExtractorCommand(
                worldId,
                nodeId
            ));

            return NoContent();
        }
    }
}