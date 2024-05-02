using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    [Route("api")]
    [ApiController]
    public class ResourcesController(IResourcesModule module) : Controller
    {
        /// <summary>
        ///     Get the details of the resource.
        /// </summary>
        /// <paramref name="resourceId">The id of the resource to retrieve the details for.</paramref>
        /// <response code="200">
        ///     Returns the details as a <see cref="ResourceDetailsDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetResourceDetails)]
        [HttpGet("resources/[controller]/{resourceId}")]
        [ProducesResponseType(typeof(ResourceDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetails([FromRoute] Guid resourceId)
        {
            var resource = await module.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceId));
            return Ok(resource);
        }
    }
}