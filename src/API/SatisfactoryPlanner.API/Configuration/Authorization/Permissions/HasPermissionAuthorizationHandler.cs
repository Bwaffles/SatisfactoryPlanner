using Microsoft.AspNetCore.Authorization;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Permissions
{
    internal class HasPermissionAuthorizationHandler(
        IExecutionContextAccessor executionContextAccessor,
        IUserAccessModule userAccessModule)
        : AttributeAuthorizationHandler<
            HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {
            try
            {
                var permissions =
                    await userAccessModule.ExecuteQueryAsync(
                        new GetUserPermissionsQuery(executionContextAccessor.UserId));

                if (!await AuthorizeAsync(attribute.Name, permissions))
                {
                    context.Fail();
                    return;
                }

                context.Succeed(requirement);
            }
            catch
            { // Catching exceptions so that it runs through all handlers and gives the correct response code
                context.Fail();
            }
        }

        private static Task<bool> AuthorizeAsync(string permission, List<UserPermissionDto> permissions)
        {
            return Task.FromResult(permissions.Any(x => x.Code == permission));
        }
    }
}