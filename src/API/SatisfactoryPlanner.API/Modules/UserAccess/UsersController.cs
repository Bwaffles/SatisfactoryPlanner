using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;

namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    [Route("api/user-access/[controller]")]
    [ApiController]
    public class UsersController(IUserAccessModule module) : ControllerBase
    {
        /// <summary>
        ///     Create the current user. After signing up with Auth0, the user needs to be created in our database.
        /// </summary>
        /// <response code="201">Returns the id of the new user as a <see cref="Guid" />.</response>
        [Authorize]
        [NoPermissionRequired]
        [HttpPost("@me")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCurrentUser([FromBody] CreateCurrentUserRequest request)
        {
            var currentUserId = await module.ExecuteCommandAsync(new CreateCurrentUserCommand(
                request.Auth0UserId
            ));

            return Created("", currentUserId);
        }
    }
}