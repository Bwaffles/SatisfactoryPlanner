using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;
using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class TapWorldNode
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId, Guid extractorId)
            => await client.PostAsJsonAsync($"api/worlds/{worldId}/nodes/{nodeId}/tap", new TapWorldNodeRequest
            {
                ExtractorId = extractorId
            });

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                var worldId = await new WorldFixture(Client).CreateEmptyWorld();
                var ironOreNode = await new WorldNodeFixture(Client)
                    .FindWorldNode(worldId, worldNode => worldNode.ResourceName == "Iron Ore");
                var ironOreNodeDetailsResult = await GetWorldNodeDetails.GetResult(Client, worldId, ironOreNode.Id);
                var extractor = ironOreNodeDetailsResult.Details.AvailableExtractors.First();

                var response = await Execute(Client, worldId, ironOreNode.Id, extractor.Id);

                response.Should().HaveStatusCode(HttpStatusCode.OK);
            }
        }
    }
}