using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails;

namespace SatisfactoryPlanner.API.Modules.Worlds.Pioneers
{
    [Route("api/worlds/pioneers")]
    [ApiController]
    public class PioneersController(IWorldsModule module) : ControllerBase
    {
        [HttpGet("{pioneerId:guid}")]
        [ProducesResponseType(typeof(List<PioneerDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPioneerDetails([FromRoute] Guid pioneerId)
        {
            var pioneerDetails = await module.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));
            return pioneerDetails == null ? NotFound() : Ok(pioneerDetails);
        }
    }
}