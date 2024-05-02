using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    [ApiController]
    public class GetResourceDetails(IResourcesModule module) : Controller
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.GetResourceDetails)]
        [HttpGet("api/resources/resources/{resourceId}")]
        [SwaggerOperation(
            Summary = "Get the details of the resource.",
            Tags = [Tags.Resources])]
        [SwaggerResponse(200, Type = typeof(ResourceDetailsDto))]
        public async Task<IActionResult> HandleAsync(
            [FromRoute]
            [SwaggerParameter(Description = "The id of the resource to retrieve the details for.")]
            Guid resourceId)
        {
            var resource = await module.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceId));
            return Ok(resource);
        }
    }
}