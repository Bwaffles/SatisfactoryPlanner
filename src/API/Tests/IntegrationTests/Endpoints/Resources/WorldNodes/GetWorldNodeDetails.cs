using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using System.Net;
using static SatisfactoryPlanner.API.Modules.Resources.WorldNodes.GetWorldNodeDetails;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class GetWorldNodeDetails
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId) => await client.GetAsync($"api/worlds/{worldId}/nodes/{nodeId}");

        internal static async Task<WorldNodeDetailsResult> GetResult(HttpClient client, Guid worldId, Guid nodeId)
        {
            var ironOreNodeDetailsResponse = await Execute(client, worldId, nodeId);
            return (await ironOreNodeDetailsResponse.ReadContentAsync<GetWorldNodeDetailsResponse>())!.Data;
        }

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                var worldId = await new WorldFixture(Client).CreateEmptyWorld();
                var anyNode = await new WorldNodeFixture(Client).FindWorldNode(worldId, _ => true);

                var response = await Execute(Client, worldId, anyNode.Id);

                AssertAll(async () =>
                {
                    response.Should().HaveStatusCode(HttpStatusCode.OK);

                    var responseContent = (await response.ReadContentAsync<GetWorldNodeDetailsResponse>())!;
                    responseContent.Data.Should().NotBeNull();
                });
            }
        }
    }
}