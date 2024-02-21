using Microsoft.AspNetCore.Authorization;
using SatisfactoryPlanner.API.Configuration.Extensions;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
using System.Text.Json;

namespace SatisfactoryPlanner.API.Configuration.Authorization.Worlds
{
    internal class WorldAuthorizationHandler(IWorldsModule worldsModule)
        : AttributeAuthorizationHandler<WorldAuthorizationRequirement,
            WorldAuthorizationAttribute>
    {
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
                        var worlds = await worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
                        if (worlds.Any(x => x.Id == worldId))
                        {
                            context.Succeed(requirement);
                            return;
                        }
                    }
                }

                if (request.RouteValues.TryGetValue("worldId", out var routeWorldId))
                {
                    var worlds = await worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
                    if (worlds.Any(x => x.Id == Guid.Parse((string)routeWorldId!)))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }

                // Try getting WorldId from a query string
                var query = request.Query;
                if (query.ContainsKey("worldId"))
                {
                    var worldId = Guid.Parse(query["worldId"].First()!);
                    var worlds = await worldsModule.ExecuteQueryAsync(new GetCurrentPioneerWorldsQuery());
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