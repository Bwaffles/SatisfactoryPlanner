using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.UpgradeExtractor;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class UpgradeExtractor(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.UpgradeExtractor)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/upgrade-extractor")]
        [SwaggerOperation(
            Summary = "Upgrade the extractor used to extract resources from the world node.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(204)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId,
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
    }

    public class UpgradeExtractorRequest
    {
        /// <summary>
        ///     The id of the extractor to upgrade to.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}