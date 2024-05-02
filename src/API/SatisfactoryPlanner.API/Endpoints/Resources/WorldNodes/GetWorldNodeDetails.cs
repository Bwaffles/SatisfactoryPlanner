using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class GetWorldNodeDetails(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.GetWorldNodeDetails)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/nodes/{nodeId}")]
        [SwaggerOperation(
            Summary = "Get the details of a node in the world.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(200, Type = typeof(WorldNodeDetailsDto))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromRoute] Guid nodeId)
        {
            var worldNodeDetails = await module.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId));
            return Ok(worldNodeDetails);
        }
    }
}