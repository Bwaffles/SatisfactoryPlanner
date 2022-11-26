using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;
using SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.GetPioneerDetails;
using SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.SpawnPioneer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Pioneers.Pioneers
{
    [Route("api/pioneers/pioneers")]
    [ApiController]
    public class PioneersController : ControllerBase
    {
        private readonly IPioneersModule _pioneersModule;

        public PioneersController(IPioneersModule pioneersModule) => _pioneersModule = pioneersModule;

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SpawnPioneer([FromBody] SpawnPioneerRequest request)
        {
            var pioneerId = await _pioneersModule.ExecuteCommandAsync(new SpawnPioneerCommand(
                request.Auth0UserId
            ));
            
            return Created($"pioneers/pioneers/{pioneerId}", pioneerId);
        }

        [HttpGet("{pioneerId:guid}")]
        [ProducesResponseType(typeof(List<PioneerDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPioneerDetails([FromRoute]Guid pioneerId)
        {
            var pioneerDetails = await _pioneersModule.ExecuteQueryAsync(new GetPioneerDetailsQuery(pioneerId));
            return pioneerDetails == null ? NotFound() : Ok(pioneerDetails);
        }
    }
}