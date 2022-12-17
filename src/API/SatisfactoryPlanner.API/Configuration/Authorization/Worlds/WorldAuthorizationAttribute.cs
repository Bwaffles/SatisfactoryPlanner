using Microsoft.AspNetCore.Authorization;
using System;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Worlds
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class WorldAuthorizationAttribute : AuthorizeAttribute
    {
        internal const string HasPermissionPolicyName = "WorldAuthorization";

        public WorldAuthorizationAttribute()
            : base(HasPermissionPolicyName)
        {
        }
    }
}