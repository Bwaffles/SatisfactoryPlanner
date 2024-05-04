using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class DowngradeExtractor(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.DowngradeExtractor)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/downgrade-extractor")]
        [SwaggerOperation(
            Summary = "Downgrade the extractor used to extract resources from the world node.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(204)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId,
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
    }

    public class DowngradeExtractorRequest
    {
        /// <summary>
        ///     The id of the extractor to downgrade to.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}