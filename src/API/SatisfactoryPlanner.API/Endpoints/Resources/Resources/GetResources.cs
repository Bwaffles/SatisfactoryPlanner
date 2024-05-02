using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    [ApiController]
    public class GetResources(IResourcesModule module) : Controller
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.GetResources)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/resources")]
        [SwaggerOperation(
            Summary = "Get the resources available in the world.",
            Tags = ["Resources"])]
        [SwaggerResponse(200, Type = typeof(List<ResourceDto>))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId)
        {
            var resources = await module.ExecuteQueryAsync(new GetResourcesQuery(worldId));
            return Ok(resources);
        }
    }
}