using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Configuration
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            var scopeClaim = context
                .User
                .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);
            if (scopeClaim == null)
                return Task.CompletedTask;
            
            var scopes = scopeClaim.Value.Split(' ');
            if (scopes.Any(scope => scope == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
