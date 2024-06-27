using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats;
using static SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats.ItemStatsResult;

namespace SatisfactoryPlanner.IntegrationTests.GetItemStats;

internal class GetItemStatsTests : IntegrationTest
{
    [Test]
    public async Task TapOneNodeScenario()
    {
        var worldId = Guid.NewGuid();

        await SpawnWorldNodes(worldId);

        var nodes = await GetWorldNodes(worldId);

        var ironOreNode = nodes.First(node => node.ResourceName == "Iron Ore");
        var nodeDetails = await GetNodeDetails(worldId, ironOreNode.Id);
        var extractor = nodeDetails.AvailableExtractors.First();
        await TapWorldNode(worldId, ironOreNode.Id, extractor.Id);

        var itemStatsResult = await GetItemStats(worldId);

        itemStatsResult.Should().BeEquivalentTo(new ItemStatsResult
        {
            Items = [
                new WarehouseItem {
                    ItemId = "IronOre",
                    ItemName = "Iron Ore",
                    AmountProduced = 0,
                    AmountExported = 0,
                    AmountAvailable = 0,
                    AmountConsumed = 0,
                    AmountImported = 0,
                    ProducedAt = [
                        ProductionSource(nodeDetails.NodeName, 0, 0, 0)
                    ],
                    ConsumedAt = []
                }
            ]
        });
    }

    [Test]
    public async Task TapMultipleNodesScenario()
    {
        var worldId = Guid.NewGuid();

        await SpawnWorldNodes(worldId);

        var nodes = await GetWorldNodes(worldId);

        var firstIronOreNode = nodes.First(node => node.ResourceName == "Iron Ore");
        var firstNodeDetails = await GetNodeDetails(worldId, firstIronOreNode.Id);
        var firstNodeExtractor = firstNodeDetails.AvailableExtractors.First();
        await TapWorldNode(worldId, firstIronOreNode.Id, firstNodeExtractor.Id);

        var secondIronOreNode = nodes.Last(node => node.ResourceName == "Iron Ore");
        var secondNodeDetails = await GetNodeDetails(worldId, secondIronOreNode.Id);
        var secondNodeExtractor = secondNodeDetails.AvailableExtractors.First();
        await TapWorldNode(worldId, secondIronOreNode.Id, secondNodeExtractor.Id);

        var bauxiteNode = nodes.First(node => node.ResourceName == "Bauxite");
        var bauxiteNodeDetails = await GetNodeDetails(worldId, bauxiteNode.Id);
        var bauxiteNodeExtractor = bauxiteNodeDetails.AvailableExtractors.First();
        await TapWorldNode(worldId, bauxiteNode.Id, bauxiteNodeExtractor.Id);

        var itemStatsResult = await GetItemStats(worldId);

        itemStatsResult.Should().BeEquivalentTo(new ItemStatsResult
        {
            Items = [
                new WarehouseItem {
                    ItemId = "IronOre",
                    ItemName = "Iron Ore",
                    AmountProduced = 0,
                    AmountExported = 0,
                    AmountAvailable = 0,
                    AmountConsumed = 0,
                    AmountImported = 0,
                    ProducedAt = [
                        ProductionSource(firstNodeDetails.NodeName, 0, 0, 0),
                        ProductionSource(secondNodeDetails.NodeName, 0, 0, 0)
                    ],
                    ConsumedAt = []
                },
                new WarehouseItem {
                    ItemId = "Bauxite",
                    ItemName = "Bauxite",
                    AmountProduced = 0,
                    AmountExported = 0,
                    AmountAvailable = 0,
                    AmountConsumed = 0,
                    AmountImported = 0,
                    ProducedAt = [
                        ProductionSource(bauxiteNodeDetails.NodeName, 0, 0, 0)
                    ],
                    ConsumedAt = []
                }
            ]
        });
    }
    [Test]
    public async Task UpdateNodeAmountScenario()
    {
        var worldId = Guid.NewGuid();

        await SpawnWorldNodes(worldId);

        var nodes = await GetWorldNodes(worldId);

        var ironOreNode = nodes.First(node => node.ResourceName == "Iron Ore");
        var nodeDetails = await GetNodeDetails(worldId, ironOreNode.Id);
        var extractor = nodeDetails.AvailableExtractors.First();
        await TapWorldNode(worldId, ironOreNode.Id, extractor.Id);

        await IncreaseExtractionRate(worldId, ironOreNode.Id, 10);

        var itemStatsResult = await GetItemStats(worldId, warehouseItem => warehouseItem.AmountProduced > 0);

        // outbox has rate of 10, but warehouses inbox has it as 0 so its getting lost somewhere--probably in json 
        // I can use domain objects in my domain events right? Having trouble deserializing a value object 
        itemStatsResult.Should().BeEquivalentTo(new ItemStatsResult
        {
            Items = [
                new WarehouseItem {
                    ItemId = "IronOre",
                    ItemName = "Iron Ore",
                    AmountProduced = 10,
                    AmountExported = 0,
                    AmountAvailable = 10,
                    AmountConsumed = 0,
                    AmountImported = 0,
                    ProducedAt = [
                        ProductionSource(nodeDetails.NodeName, 10, 0, 10)
                    ],
                    ConsumedAt = []
                }
            ]
        });
    }

    private async Task SpawnWorldNodes(Guid worldId) => await ResourcesModule.ExecuteCommandAsync(new SpawnWorldNodesCommand(Guid.NewGuid(), worldId));

    private async Task<List<GetWorldNodesResult.WorldNodeDto>> GetWorldNodes(Guid worldId) => (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodesQuery(worldId, null))).WorldNodes;

    private async Task<WorldNodeDetailsResult.WorldNodeDetails> GetNodeDetails(Guid worldId, Guid nodeId) => (await ResourcesModule.ExecuteQueryAsync(new GetWorldNodeDetailsQuery(worldId, nodeId))).Details;

    private async Task TapWorldNode(Guid worldId, Guid nodeId, Guid extractorId) => await ResourcesModule.ExecuteCommandAsync(new TapWorldNodeCommand(worldId, nodeId, extractorId));

    private async Task IncreaseExtractionRate(Guid worldId, Guid nodeId, decimal extractionRate) => await ResourcesModule.ExecuteCommandAsync(new IncreaseExtractionRateCommand(worldId, nodeId, extractionRate));

    private async Task<ItemStatsResult> GetItemStats(Guid worldId) => await Polling.GetEventually(new GetItemStatsProbe(WarehousesModule, worldId, warehouseItem => true), 7000);

    private async Task<ItemStatsResult> GetItemStats(Guid worldId, Func<WarehouseItem, bool> condition) => await Polling.GetEventually(new GetItemStatsProbe(WarehousesModule, worldId, condition), 10000);

    private static ProductionSource ProductionSource(string name, decimal amountProduced, decimal amountExported, decimal amountAvailable) => new()
    {
        Name = name,
        AmountProduced = amountProduced,
        AmountExported = amountExported,
        AmountAvailable = amountAvailable
    };

    public class GetItemStatsProbe(IWarehousesModule warehousesModule, Guid worldId, Func<WarehouseItem, bool> condition) : IProbe<ItemStatsResult>
    {
        private readonly IWarehousesModule _warehousesModule = warehousesModule;
        private readonly Guid _worldId = worldId;
        private readonly Func<WarehouseItem, bool> _condition = condition;

        public async Task<bool> IsSatisfiedAsync(ItemStatsResult sample)
        {
            return await Task.Run(() => sample.Items.Count > 0 && sample.Items.Any(_condition));
        }

        public async Task<ItemStatsResult> GetSampleAsync() => await _warehousesModule.ExecuteQueryAsync(new GetItemStatsQuery(_worldId));

        public string DescribeFailureTo() => "Cannot get item stats from the warehouse.";
    }
}
