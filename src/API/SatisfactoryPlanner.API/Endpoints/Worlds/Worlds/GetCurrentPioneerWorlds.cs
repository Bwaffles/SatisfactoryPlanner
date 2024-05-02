using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Worlds.Worlds
{
    [ApiController]
    public class GetCurrentPioneerWorlds(IWorldsModule module) : ControllerBase
    {
        [Authorize]
        [NoPermissionRequired]
        [HttpGet("api/worlds/worlds/@me")]
        [SwaggerOperation(
            Summary = "Get the worlds for the current pioneer.",
            Description = "Gets the worlds for the currently authenticated user based on the user id in the access token.",
            Tags = ["Worlds"])]
        [SwaggerResponse(200, Type = typeof(List<PioneerWorldDto>))]
        public async Task<IActionResult> HandleAsync()
        {
            var worlds = await module.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
            return Ok(worlds);
        }
    }
}