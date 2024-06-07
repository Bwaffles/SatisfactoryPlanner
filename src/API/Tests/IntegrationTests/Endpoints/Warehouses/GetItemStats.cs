using FluentAssertions.Execution;
using SatisfactoryPlanner.API.Endpoints.Warehouses;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Worlds;
using System.Net;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Warehouses
{
    public static class GetItemStats
    {
        public static async Task<HttpResponseMessage> Execute(HttpClient client, Guid worldId)
            => await client.GetAsync($"api/worlds/{worldId}/warehouse/item-stats");

        [TestFixture]
        public class Tests : IntegrationTest
        {
            [Test]
            public async Task HappyPath()
            {
                var worldId = await new WorldFixture(Client).CreateEmptyWorld();

                var response = await Execute(Client, worldId);

                using (new AssertionScope())
                {
                    response.Should().HaveStatusCode(HttpStatusCode.OK);

                    var responseContent = (await response.ReadContentAsync<GetItemStatsResponse>())!;
                    var data = responseContent.Data;
                    data.Should().NotBeNull();
                }
            }
        }
    }
}