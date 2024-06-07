using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;
using System.Net;
using static SatisfactoryPlanner.API.Modules.Resources.WorldNodes.GetWorldNodeDetails;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users
{
    public static class GetWorldNodeDetails
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId) => await client.GetAsync($"api/worlds/{worldId}/nodes/{nodeId}");

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