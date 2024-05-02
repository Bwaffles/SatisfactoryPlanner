using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class DecreaseWorldNodeExtractionRate(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.DecreaseWorldNodeExtractionRate)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/decrease-extraction-rate")]
        [SwaggerOperation(
            Summary = "Decrease the extraction rate of resources from the world node.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(204)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId,
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
    }

    public class DecreaseWorldNodeExtractionRateRequest
    {
        [BindRequired]
        public decimal ExtractionRate { get; set; }
    }
}