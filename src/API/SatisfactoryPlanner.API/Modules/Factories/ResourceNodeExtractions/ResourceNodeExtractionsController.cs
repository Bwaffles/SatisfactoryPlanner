using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.ResourceNodeExtractions.ExtractResourceNode;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Factories.ResourceNodeExtractions
{
    [ApiController]
    [Route("api/factories/resource-node-extractions")]
    public class ResourceNodeExtractionsController : Controller
    {
        private readonly IFactoriesModule factoriesModule;

        public ResourceNodeExtractionsController(IFactoriesModule factoriesModule)
        {
            this.factoriesModule = factoriesModule;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExtractResourceNode([FromBody] ExtractResourceNodeRequest request)
        {
            await factoriesModule.ExecuteCommandAsync(new ExtractResourceNodeCommand(
                request.ResourceNodeId,
                request.ResourceExtractorId,
                request.Amount,
                request.Name
            ));

            return Ok();
        }
    }
}
