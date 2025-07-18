using FluentAssertions;
using FluentAssertions.Execution;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats;
using SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.RegisterNode;
using SatisfactoryPlanner.Modules.Warehouses.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Warehouses.IntegrationTests.ItemSources;

[TestFixture]
public class RegisterNodeTests : IntegrationTest
{
    // Happy path tests
    [Test]
    public async Task WhenDataIsValid_IsSuccessful()
    {
        var id = Guid.NewGuid();
        var worldId = Guid.NewGuid();
        var nodeId = Guid.NewGuid();
        var nodeName = "Blue Crater Iron 1";
        var item = Item.IronOre;

        await WarehousesModule.ExecuteCommandAsync(new RegisterNodeCommand(id, worldId, nodeId, nodeName, item.Id));

        using (new AssertionScope())
        {
            var itemStats = await WarehousesModule.ExecuteQueryAsync(new GetItemStatsQuery(worldId));
            var ironItemStats = itemStats.Items.Single(itemStat => itemStat.ItemId == item.Id);
            ironItemStats.ItemName.Should().Be(item.Name);
            ironItemStats.AmountProduced.Should().Be(0);
            ironItemStats.AmountExported.Should().Be(0);
            ironItemStats.AmountAvailable.Should().Be(0);
            ironItemStats.AmountConsumed.Should().Be(0);
            ironItemStats.AmountImported.Should().Be(0);
            ironItemStats.ProducedAt.Should().BeEquivalentTo(
            [
                new ItemStatsResult.ProductionSource()
                {
                    Name = nodeName,
                    AmountProduced = 0,
                    AmountExported = 0,
                    AmountAvailable = 0,
                }
            ]);
            ironItemStats.ConsumedAt.Should().BeEmpty();
        }
    }

    [Test]
    public async Task WhenNodeAlreadyRegistered_IsSuccessful()
    {
        var id = Guid.NewGuid();
        var worldId = Guid.NewGuid();
        var nodeId = Guid.NewGuid();
        var nodeName = "Blue Crater Iron 1";
        var item = Item.IronOre;

        await WarehousesModule.ExecuteCommandAsync(new RegisterNodeCommand(id, worldId, nodeId, nodeName, item.Id));
        // If the node had its extractor dismantled, the node remains in the warehouse.
        // When we tap it again, we should expect the node to be there and do nothing.
        await WarehousesModule.ExecuteCommandAsync(new RegisterNodeCommand(id, worldId, nodeId, nodeName, item.Id));

        using (new AssertionScope())
        {
            var itemStats = await WarehousesModule.ExecuteQueryAsync(new GetItemStatsQuery(worldId));
            var ironItemStats = itemStats.Items.Single(itemStat => itemStat.ItemId == item.Id);
            ironItemStats.ItemName.Should().Be(item.Name);
            ironItemStats.AmountProduced.Should().Be(0);
            ironItemStats.AmountExported.Should().Be(0);
            ironItemStats.AmountAvailable.Should().Be(0);
            ironItemStats.AmountConsumed.Should().Be(0);
            ironItemStats.AmountImported.Should().Be(0);
            ironItemStats.ProducedAt.Should().BeEquivalentTo(
            [
                new ItemStatsResult.ProductionSource()
                {
                    Name = nodeName,
                    AmountProduced = 0,
                    AmountExported = 0,
                    AmountAvailable = 0,
                }
            ]);
            ironItemStats.ConsumedAt.Should().BeEmpty();
        }
    }

    // CommandValidator tests
    [Test]
    public void WhenIdIsEmpty_ThrowsInvalidCommandException()
    {
        Assert.CatchAsync<InvalidCommandException>(async () =>
        {
            await WarehousesModule.ExecuteCommandAsync(
                new RegisterNodeCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), "", "IronOre"));
        });
    }

    [Test]
    public void WhenWorldIdIsEmpty_ThrowsInvalidCommandException()
    {
        Assert.CatchAsync<InvalidCommandException>(async () =>
        {
            await WarehousesModule.ExecuteCommandAsync(
                new RegisterNodeCommand(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), "", "IronOre"));
        });
    }

    [Test]
    public void WhenNodeIdIsEmpty_ThrowsInvalidCommandException()
    {
        Assert.CatchAsync<InvalidCommandException>(async () =>
        {
            await WarehousesModule.ExecuteCommandAsync(
                new RegisterNodeCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty, "", "IronOre"));
        });
    }

    [Test]
    public void WhenItemIdIsEmpty_ThrowsInvalidCommandException()
    {
        Assert.CatchAsync<InvalidCommandException>(async () =>
        {
            await WarehousesModule.ExecuteCommandAsync(
                new RegisterNodeCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "", ""));
        });
    }

    // Command Tests
    [Test]
    public Task WhenItemDoesNotExist_ThrowsInvalidCommandException()
    {
        var id = Guid.NewGuid();
        var worldId = Guid.NewGuid();
        var nodeId = Guid.NewGuid();
        var nodeName = "Blue Crater Iron 1";
        var itemId = "NotAnItem";

        Assert.CatchAsync<InvalidCommandException>(async () =>
        {
            await WarehousesModule.ExecuteCommandAsync(new RegisterNodeCommand(id, worldId, nodeId, nodeName, itemId));
        });

        return Task.CompletedTask;
    }
}
