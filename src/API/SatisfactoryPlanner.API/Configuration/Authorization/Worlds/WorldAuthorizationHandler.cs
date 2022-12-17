using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Worlds
{
    internal class WorldAuthorizationHandler : AttributeAuthorizationHandler<WorldAuthorizationRequirement,
        WorldAuthorizationAttribute>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IWorldsModule _worldsModule;

        public WorldAuthorizationHandler(
            IExecutionContextAccessor executionContextAccessor,
            IWorldsModule worldsModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _worldsModule = worldsModule;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            WorldAuthorizationRequirement requirement,
            WorldAuthorizationAttribute attribute)
        {
            var query = ((DefaultHttpContext?)context.Resource)?.HttpContext.Request.Query;
            if (query == null || !query.ContainsKey("worldId"))
            {
                context.Succeed(requirement);
                return;
            }

            var worldId = Guid.Parse(query["worldId"].First()!);
            var worlds = await _worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
            if (worlds.All(x => x.Id != worldId))
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}