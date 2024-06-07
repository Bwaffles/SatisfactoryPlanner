using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using static SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes.GetWorldNodesResult;
using GetWorldNodesTests = SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users.GetWorldNodes;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes
{
    internal class WorldNodeFixture(HttpClient client)
    {
        private readonly HttpClient _client = client;

        /// <summary>
        /// Find a world node that satisfies a specified condition.
        /// <para>
        /// Note: The world must be created prior to this call.
        /// </para>
        /// </summary>
        /// <param name="worldId">The id of the pre-existing world to search in.</param>
        /// <param name="predicate">A function to test each world node for a condition.</param>
        /// <returns>The first world node in the sequence that passes the test in the specified predicate function.</returns>
        internal async Task<WorldNodeDto> FindWorldNode(Guid worldId, Func<WorldNodeDto, bool> predicate)
        {
            var getWorldNodesResponse = await Polling.GetEventually(new GetWorldNodesTests.Probe(_client, worldId), 10000);
            var worldNodesContent = (await getWorldNodesResponse.ReadContentAsync<GetWorldNodesResponse>())!;
            return worldNodesContent.Data.WorldNodes.First(predicate);
        }
    }
}
