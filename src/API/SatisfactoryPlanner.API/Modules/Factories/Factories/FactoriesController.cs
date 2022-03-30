using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.Factories.GetFactories;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Factories.Factories
{
    [ApiController]
    [Route("api/factories/factories")]
    public class FactoriesController : ControllerBase
    {
        private readonly IFactoriesModule factoriesModule;

        public FactoriesController(IFactoriesModule factoriesModule)
        {
            this.factoriesModule = factoriesModule;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFactories()
        {
            var factories = await factoriesModule.ExecuteQueryAsync(new GetFactoriesQuery());
            return Ok(factories);
        }
    }
}
