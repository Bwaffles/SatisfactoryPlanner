using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceDetails;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceNodes;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResources;
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

        [HttpGet("{resourceCode}")]
        [ProducesResponseType(typeof(ResourceDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetails([FromRoute] string resourceCode)
        {
            var resource = await factoriesModule.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceCode));
            return Ok(resource);
        }

        [HttpGet("{resourceCode}/nodes")]
        [ProducesResponseType(typeof(List<ResourceNodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceNodes([FromRoute] string resourceCode)
        {
            var resourceNodes = await factoriesModule.ExecuteQueryAsync(new GetResourceNodesQuery(resourceCode));
            return Ok(resourceNodes);
        }

        [HttpGet("{resourceCode}/extractors")]
        [ProducesResponseType(typeof(List<ResourceExtractorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceExtractors([FromRoute] string resourceCode)
        {
            var resourceExtractors = await factoriesModule.ExecuteQueryAsync(new GetResourceExtractorsQuery(resourceCode));
            return Ok(resourceExtractors);
        }
    }
}
