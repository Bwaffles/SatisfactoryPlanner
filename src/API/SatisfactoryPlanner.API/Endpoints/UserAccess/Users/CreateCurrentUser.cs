using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.UserAccess.Users
{
    [ApiController]
    public class CreateCurrentUser(IUserAccessModule module) : ControllerBase
    {
        [Authorize]
        [NoPermissionRequired]
        [HttpPost("api/user-access/users/@me")]
        [SwaggerOperation(
            Summary = "Create the currently authenticated user.",
            Description = "Create the currently authenticated user based on the user id in the access token.",
            Tags = [Tags.Users])]
        [SwaggerResponse(201, Type = typeof(CreateCurrentUserResponse))]
        public async Task<IActionResult> HandleAsync([FromBody] CreateCurrentUserRequest request)
        {
            var currentUserId = await module.ExecuteCommandAsync(new CreateCurrentUserCommand(
                request.Auth0UserId
            ));

            return Created("", new CreateCurrentUserResponse(currentUserId));
        }
    }

    public class CreateCurrentUserRequest
    {
        [BindRequired]
        public required string Auth0UserId { get; set; }
    }

    public record CreateCurrentUserResponse(Guid UserId);
}