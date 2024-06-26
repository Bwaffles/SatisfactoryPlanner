using SatisfactoryPlanner.API.Endpoints.Warehouses;
using SatisfactoryPlanner.API.IntegrationTests.Endpoints.Resources.WorldNodes;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;
using System.Net;
using static SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats.ItemStatsResult;

namespace SatisfactoryPlanner.API.IntegrationTests.Endpoints.Warehouses;

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
            var (worldId, _) = await new TappedWorldNodeFixture(Client).Create();

            var response = (await GetEventually(new Probe(Client, worldId, warehouseItem => true), 7000))!;

            AssertAll(() =>
            {
                response.Should().HaveStatusCode(HttpStatusCode.OK);
            });
        }
    }

    public class Probe(HttpClient client, Guid worldId, Func<WarehouseItem, bool> condition) : IProbe<HttpResponseMessage>
    {
        private readonly HttpClient _client = client;
        private readonly Guid _worldId = worldId;
        private readonly Func<WarehouseItem, bool> _condition = condition;

        public async Task<bool> IsSatisfiedAsync(HttpResponseMessage sample)
        {
            if (sample?.IsSuccessStatusCode != true)
                return false;

            var content = (await sample.ReadContentAsync<GetItemStatsResponse>())!;
            var items = content.Data.Items;

            return items.Any(_condition);
        }

        public async Task<HttpResponseMessage> GetSampleAsync() => await Execute(_client, _worldId);

        public string DescribeFailureTo() => "Cannot get item stats from the warehouse.";
    }
}