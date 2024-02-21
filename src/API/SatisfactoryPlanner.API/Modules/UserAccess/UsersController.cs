using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser;

namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    [Route("api/user-access/[controller]")]
    [ApiController]
    public class UsersController(IUserAccessModule module) : ControllerBase
    {
        /// <summary>
        ///     Get the currently logged in user.
        /// </summary>
        /// <response code="200">
        ///     Returned when the user has been created and returns their details as <see cref="CurrentUserDto" />.
        /// </response>
        /// <response code="204">
        ///     Returned when the user has not been created yet and <see cref="CreateCurrentUser" /> needs to be called.
        /// </response>
        [Authorize]
        [NoPermissionRequired]
        [HttpGet("@me")]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = await module.ExecuteQueryAsync(new GetCurrentUserQuery());
            if (currentUser == null)
                return NoContent();

            return Ok(currentUser);
        }

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