using Microsoft.AspNetCore.Authorization;
using System;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Worlds
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class WorldAuthorizationAttribute : AuthorizeAttribute
    {
        internal const string HasPermissionPolicyName = "WorldAuthorization";

        public Type? BodyType { get; }

        public WorldAuthorizationAttribute() : base(HasPermissionPolicyName) { }

        public WorldAuthorizationAttribute(Type bodyType)
            : base(HasPermissionPolicyName)
        {
            BodyType = bodyType;
        }
    }
}