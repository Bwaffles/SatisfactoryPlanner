using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class GetWorldNodes(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.GetWorldNodes)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/nodes")]
        [SwaggerOperation(
            Summary = "Get nodes in the world.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(200, Type = typeof(GetWorldNodesResponse))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId,
            [FromQuery] GetWorldNodesRequest request)
        {
            var worldNodes = await module.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, request.ResourceId));
            return Ok(new GetWorldNodesResponse(worldNodes));
        }
    }

    public record GetWorldNodesRequest
    {
        public Guid? ResourceId { get; set; }
    }

    public record GetWorldNodesResponse(GetWorldNodesResult Data);
}