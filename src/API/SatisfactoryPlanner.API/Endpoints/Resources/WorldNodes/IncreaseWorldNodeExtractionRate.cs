using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class IncreaseWorldNodeExtractionRate(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.IncreaseWorldNodeExtractionRate)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/increase-extraction-rate")]
        [SwaggerOperation(
            Summary = "Increase the extraction rate of resources from the world node.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(204)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId,
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
    }

    public class IncreaseWorldNodeExtractionRateRequest
    {
        [BindRequired]
        public decimal ExtractionRate { get; set; }
    }
}