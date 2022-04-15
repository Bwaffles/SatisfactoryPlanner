using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceDetails;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceNodes;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Factories.Resources
{
    [ApiController]
    [Route("api/factories/resources")]
    public class ResourcesController : Controller
    {
        private readonly IFactoriesModule factoriesModule;

        public ResourcesController(IFactoriesModule factoriesModule)
        {
            this.factoriesModule = factoriesModule;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ResourceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResources()
        {
            var resources = await factoriesModule.ExecuteQueryAsync(new GetResourcesQuery());
            return Ok(resources);
        }

        [HttpGet("{resourceId}")]
        [ProducesResponseType(typeof(ResourceDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetails([FromRoute] Guid resourceId)
        {
            var resource = await factoriesModule.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceId));
            return Ok(resource);
        }

        [HttpGet("{resourceId}/nodes")]
        [ProducesResponseType(typeof(List<ResourceNodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceNodes([FromRoute] Guid resourceId)
        {
            var resourceNodes = await factoriesModule.ExecuteQueryAsync(new GetResourceNodesQuery(resourceId));
            return Ok(resourceNodes);
        }

        [HttpGet("{resourceId}/extractors")]
        [ProducesResponseType(typeof(List<ResourceExtractorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceExtractors([FromRoute] Guid resourceId)
        {
            var resourceExtractors = await factoriesModule.ExecuteQueryAsync(new GetResourceExtractorsQuery(resourceId));
            return Ok(resourceExtractors);
        }
    }
}
