﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    [Route("api")]
    [ApiController]
    public class ResourcesController(IResourcesModule module) : Controller
    {
        /// <summary>
        ///     Get the resources available in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns the resources as a list of <see cref="ResourceDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetResources)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/[controller]")]
        [ProducesResponseType(typeof(List<ResourceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResources([FromRoute] Guid worldId)
        {
            var resources = await module.ExecuteQueryAsync(new GetResourcesQuery(worldId));
            return Ok(resources);
        }

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