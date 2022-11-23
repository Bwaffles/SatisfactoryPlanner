using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using System;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    [Route("api/user-access/user-registrations")]
    [ApiController]
    public class UserRegistrationsController : ControllerBase
    {
        private readonly IUserAccessModule _userAccessModule;

        public UserRegistrationsController(IUserAccessModule userAccessModule)
        {
            _userAccessModule = userAccessModule;
        }

        [NoPermissionRequired]
        [AllowAnonymous]
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
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
