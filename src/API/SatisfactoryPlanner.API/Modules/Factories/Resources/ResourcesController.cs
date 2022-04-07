using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.Resources.GetResourceDetails;
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
            var items = await factoriesModule.ExecuteQueryAsync(new GetResourcesQuery());
            return Ok(items);
        }

        [HttpGet("{resourceCode}")]
        public async Task<IActionResult> GetResourceDetails([FromRoute] string resourceCode)
        {
            var items = await factoriesModule.ExecuteQueryAsync(new GetResourceDetailsQuery(resourceCode));
            return Ok(items);
        }
    }
}
