using SatisfactoryPlanner.API.Modules.Resources.WorldNodes;
using System.Net;
using System.Net.Http.Json;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;

public static class UpgradeExtractor
{
    public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId, Guid nodeId, Guid extractorId)
        => await client.PostAsJsonAsync($"api/worlds/{worldId}/nodes/{nodeId}/upgrade-extractor", new UpgradeExtractorRequest
        {
            ExtractorId = extractorId
        });

    [TestFixture]
    public class Tests : IntegrationTest
    {
        [Test]
        public async Task HappyPath()
        {
            var settings = await new TappedWorldNodeFixture(Client)
                .Create("Iron Ore", "Miner Mk.1");

            var fasterExtractor = settings.AvailableExtractors.First(extractor => extractor.MaxExtractionRate > settings.CurrentExtractor.MaxExtractionRate);
            var response = await Execute(Client, settings.WorldId, settings.NodeId, fasterExtractor.Id);

            response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        }
    }
}