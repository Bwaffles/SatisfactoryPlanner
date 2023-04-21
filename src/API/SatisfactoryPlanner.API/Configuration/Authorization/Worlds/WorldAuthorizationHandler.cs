using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SatisfactoryPlanner.API.Configuration.Extensions;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Worlds
{
    internal class WorldAuthorizationHandler : AttributeAuthorizationHandler<WorldAuthorizationRequirement,
        WorldAuthorizationAttribute>
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IWorldsModule _worldsModule;

        public WorldAuthorizationHandler(IExecutionContextAccessor executionContextAccessor, IWorldsModule worldsModule)
        {
            _executionContextAccessor = executionContextAccessor;
            _worldsModule = worldsModule;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            WorldAuthorizationRequirement requirement,
            WorldAuthorizationAttribute attribute)
        {
            try
            {
                var request = ((DefaultHttpContext?)context.Resource)?.HttpContext.Request;
                if (request == null)
                {
                    context.Fail();
                    return;
                }

                // Try getting WorldId from the Body like in a post request
                if (attribute.BodyType != null)
                {
                    var worldIdProperty = attribute.BodyType.GetProperties()
                        .FirstOrDefault(property => property.Name == "WorldId");
                    if (worldIdProperty != null)
                    {
                        var body = await request.ReadAsJsonAsync(attribute.BodyType, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        var worldId = (Guid?)worldIdProperty.GetValue(body);
                        var worlds = await _worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
                        if (worlds.Any(x => x.Id == worldId))
                        {
                            context.Succeed(requirement);
                            return;
                        }
                    }
                }

                // Try getting WorldId from a query string
                var query = request.Query;
                if (query.ContainsKey("worldId"))
                {
                    var worldId = Guid.Parse(query["worldId"].First()!);
                    var worlds = await _worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
                    if (worlds.Any(x => x.Id == worldId))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }

                // If WorldId wasn't found, but this attribute is on the endpoint then it needs to fail
                context.Fail();
            }
            catch (Exception ex)
            { // Catching exceptions so that it runs through all handlers and gives the correct response code
                context.Fail(new AuthorizationFailureReason(this, ex.Message));
            }
        }
    }
}