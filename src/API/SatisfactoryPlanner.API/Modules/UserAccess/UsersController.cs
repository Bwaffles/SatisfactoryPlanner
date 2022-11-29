using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser;
using System;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    [Route("api/user-access/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public UsersController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        /// <summary>
        ///     Get the currently logged in user.
        /// </summary>
        /// <response code="200">Returned when the user has been created and returns their details as <see cref="CurrentUserDto"/>.</response>
        /// <response code="204">Returned when the user has not been created yet and CreateNewUser needs to be called..</response>
        [Authorize]
        [HttpGet("@me")]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = await _userAccessModule.ExecuteQueryAsync(new GetCurrentUserQuery());
            if (currentUser == null)
                return NoContent();

            return Ok(currentUser);
        }

        [NoPermissionRequired]
        [AllowAnonymous]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateNewUser(RegisterNewUserRequest request)
        {
            await _userAccessModule.ExecuteCommandAsync(new RegisterNewUserCommand(
                request.Username,
                request.Password,
                request.Email,
                request.ConfirmLink));

            return Ok();
        }

        [NoPermissionRequired]
        [AllowAnonymous]
        [HttpPost("{userRegistrationId}/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
        {
            await _userAccessModule.ExecuteCommandAsync(new ConfirmRegistrationCommand(userRegistrationId));

            return Ok();
        }
    }
}
