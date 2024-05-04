using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.UserAccess.Users
{
    [ApiController]
    public class GetCurrentUser(IUserAccessModule module) : ControllerBase
    {
        [Authorize]
        [NoPermissionRequired]
        [HttpGet("api/user-access/users/@me")]
        [SwaggerOperation(
            Summary = "Get the currently authenticated user.", 
            Description = "Gets the currently authenticated user based on the user id in the access token.",
            Tags = [Tags.Users])]
        [SwaggerResponse(200, Type = typeof(CurrentUserDto))]
        [SwaggerResponse(204, "The user was not found.")]
        public async Task<IActionResult> HandleAsync()
        {
            var currentUser = await module.ExecuteQueryAsync(new GetCurrentUserQuery());
            if (currentUser == null)
                return NoContent();

            return Ok(currentUser);
        }
    }
}
