using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails;
using SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Resources
{
    [ApiController]
    [Route("api/resources")]
    public class ResourcesController : Controller
    {
        private readonly IResourcesModule _resourcesModule;

        public ResourcesController(IResourcesModule resourcesModule)
        {
            _resourcesModule = resourcesModule;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ResourceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResources()
        {
            var resources = await _resourcesModule.ExecuteQueryAsync(new GetResourcesQuery());
            return Ok(resources);
        }

        [HttpGet("{resourceId}")]
        [ProducesResponseType(typeof(ResourceDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetails([FromRoute] Guid resourceId)
        {
            var resource = await _resourcesModule.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceId));
            return Ok(resource);
        }
    }
}
