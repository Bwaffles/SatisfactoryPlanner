using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;

public static class DismantleExtractor
{
    public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId)
        => await client.PostAsJsonAsync($"api/worlds/{worldId}/nodes/{nodeId}/dismantle-extractor", new { });

    [TestFixture]
    public class Tests : IntegrationTest
    {
        [Test]
        public async Task HappyPath()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture(Client).Create();

            var response = await Execute(Client, worldId, nodeId);

            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }
    }
}