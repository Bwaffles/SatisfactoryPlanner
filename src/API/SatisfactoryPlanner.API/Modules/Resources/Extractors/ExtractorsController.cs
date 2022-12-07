using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Extractors
{
    [ApiController]
    [Route("api/resources/[controller]")]
    public class ExtractorsController : Controller
    {
        private readonly IResourcesModule _module;

        public ExtractorsController(IResourcesModule module)
        {
            _module = module;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<ExtractorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetExtractors([FromQuery] GetExtractorsRequest request)
        {
            var extractors = await _module.ExecuteQueryAsync(new GetExtractorsQuery(request.ResourceId));
            return Ok(extractors);
        }
    }
}
