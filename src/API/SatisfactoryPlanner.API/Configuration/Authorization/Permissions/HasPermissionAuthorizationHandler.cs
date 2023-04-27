using Microsoft.AspNetCore.Authorization;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Permissions
{
    internal class HasPermissionAuthorizationHandler : AttributeAuthorizationHandler<
        HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IUserAccessModule _userAccessModule;

        public HasPermissionAuthorizationHandler(
            IExecutionContextAccessor executionContextAccessor,
            IUserAccessModule userAccessModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _userAccessModule = userAccessModule;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {
            try
            {
                var permissions =
                    await _userAccessModule.ExecuteQueryAsync(
                        new GetUserPermissionsQuery(_executionContextAccessor.UserId));

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

        private Task<bool> AuthorizeAsync(string permission, List<UserPermissionDto> permissions)
        {
            return Task.FromResult(permissions.Any(x => x.Code == permission));
        }
    }
}