using Microsoft.AspNetCore.Authorization;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Permissions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    internal class HasPermissionAttribute(string name) : AuthorizeAttribute(HasPermissionPolicyName)
    {
        internal const string HasPermissionPolicyName = "HasPermission";

        public string Name { get; } = name;
    }
}