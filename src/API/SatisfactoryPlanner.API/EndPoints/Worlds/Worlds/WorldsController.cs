using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;

namespace SatisfactoryPlanner.API.Modules.Worlds.Worlds
{
    [Route("api/worlds/[controller]")]
    [ApiController]
    public class WorldsController(IWorldsModule module) : ControllerBase
    {
        /// <summary>
        ///     Get the worlds for the currently logged in pioneer.
        /// </summary>
        /// <response code="200">
        ///     Returns their worlds as a list of <see cref="PioneerWorldDto" />.
        /// </response>
        [Authorize]
        [NoPermissionRequired]
        [HttpGet("@me")]
        [ProducesResponseType(typeof(List<PioneerWorldDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentPioneerWorlds()
        {
            var worlds = await module.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
            return Ok(worlds);
        }
    }
}