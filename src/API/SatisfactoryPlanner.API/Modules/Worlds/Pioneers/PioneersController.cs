using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Worlds.Pioneers
{
    [Route("api/worlds/pioneers")]
    [ApiController]
    public class PioneersController : ControllerBase
    {
        private readonly IWorldsModule _worldsModule;

        public PioneersController(IWorldsModule worldsModule) => _worldsModule = worldsModule;

        [HttpGet("{pioneerId:guid}")]
        [ProducesResponseType(typeof(List<PioneerDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPioneerDetails([FromRoute]Guid pioneerId)
        {
            var pioneerDetails = await _worldsModule.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));
            return pioneerDetails == null ? NotFound() : Ok(pioneerDetails);
        }
    }
}