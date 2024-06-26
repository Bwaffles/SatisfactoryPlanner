using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds
{
    internal class WorldFixture(HttpClient client)
    {
        private readonly HttpClient _client = client;

        /// <summary>
        /// Create an empty world for the currently logged in user.
        /// NOTE: World Nodes not guaranteed to be created at this point, just the world.
        /// </summary>
        /// <returns>Returns the id of the new world.</returns>
        internal async Task<Guid> CreateEmptyWorld()
        {
            await CreateCurrentUser.Execute(_client);

            var currentPioneerWorlds = (await Polling.GetEventually(new GetCurrentPioneerWorlds.Probe(_client), 10000))!;
            return (await currentPioneerWorlds.ReadContentAsync<List<PioneerWorldDto>>())[0].Id;
        }
    }
}
