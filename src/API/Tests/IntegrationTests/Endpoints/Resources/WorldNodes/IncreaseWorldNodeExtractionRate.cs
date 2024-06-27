using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;

public static class IncreaseWorldNodeExtractionRate
{
    public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId, decimal extractionRate)
        => await client.PostAsJsonAsync($"api/worlds/{worldId}/nodes/{nodeId}/increase-extraction-rate", new IncreaseWorldNodeExtractionRateRequest
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

            var response = await Execute(Client, worldId, nodeId, 10);

            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }
    }
}