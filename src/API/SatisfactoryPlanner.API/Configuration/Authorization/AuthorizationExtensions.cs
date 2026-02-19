using Microsoft.AspNetCore.Authorization;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;

namespace SatisfactoryPlanner.API.Configuration.Authorization
{
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Configure everything needed for authorization of the API.
        /// </summary>
        public static IServiceCollection ConfigureAuthorizationService(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder => policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement()))
                .AddPolicy(WorldAuthorizationAttribute.HasPermissionPolicyName, policyBuilder => policyBuilder.Requirements.Add(new WorldAuthorizationRequirement()));

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, WorldAuthorizationHandler>();

            return services;
        }
    }
}
