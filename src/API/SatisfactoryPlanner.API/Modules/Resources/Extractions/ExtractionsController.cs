using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Extractions
{
    [ApiController]
    [Route("api/resources/extractions")]
    public class ExtractionsController : Controller
    {
        private readonly IResourcesModule _resourcesModule;

        public ExtractionsController(IResourcesModule resourcesModule)
        {
            _resourcesModule = resourcesModule;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExtractResource([FromBody] ExtractResourceRequest request)
        {
            await _resourcesModule.ExecuteCommandAsync(new ExtractResourceNodeCommand(
                request.ResourceNodeId,
                request.ExtractorId,
                request.Amount,
                request.Name
            ));

            return Ok();
        }
    }
}
