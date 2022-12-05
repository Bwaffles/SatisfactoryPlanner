using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Worlds.Worlds
{
    [Route("api/worlds/[controller]")]
    [ApiController]
    public class WorldsController : ControllerBase
    {
        private readonly IWorldsModule _module;

        public WorldsController(IWorldsModule module)
        {
            _module = module;
        }

        /// <summary>
        ///     Get the worlds for the currently logged in pioneer.
        /// </summary>
        /// <response code="200">
        ///     Returns their worlds as a list of <see cref="WorldDto" />.
        /// </response>
        [Authorize]
        [NoPermissionRequired]
        [HttpGet("@me")]
        [ProducesResponseType(typeof(List<WorldDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentPioneerWorlds()
        {
            var worlds = await _module.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
            return Ok(worlds);
        }
    }
}