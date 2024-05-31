using FluentAssertions.Execution;
using SatisfactoryPlanner.API.Endpoints.Warehouses;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Production.ProcessedItems;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.UserAccess.Users;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds;
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
                // Need a user so we pass the permissions check... nearly every endpoint test will need this
                // TODO should we add a test for each endpoint that it has the right authorization? Maybe we can check its attributes?
                await CreateCurrentUser.Execute(Client);

                var currentPioneerWorlds = (await GetEventually(new GetCurrentPioneerWorlds.Probe(Client), 10000))!;
                var worldId = (await currentPioneerWorlds.ReadContentAsync<List<PioneerWorldDto>>())[0].Id;

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