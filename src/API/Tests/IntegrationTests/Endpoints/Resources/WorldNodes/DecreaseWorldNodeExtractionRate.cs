using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;

public static class DecreaseWorldNodeExtractionRate
{
    public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId, decimal extractionRate)
        => await client.PostAsJsonAsync($"api/worlds/{worldId}/nodes/{nodeId}/decrease-extraction-rate", new DecreaseWorldNodeExtractionRateRequest
        {
            ExtractionRate = extractionRate
        });

    [TestFixture]
    public class Tests : IntegrationTest
    {
        [Test]
        public async Task HappyPath()
        {
            var (worldId, nodeId) = await new TappedWorldNodeFixture(Client).Create();
            await IncreaseWorldNodeExtractionRate.Execute(Client, worldId, nodeId, 10);

            var response = await Execute(Client, worldId, nodeId, 3);

            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }
    }
}