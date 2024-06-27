using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources.Events;

namespace SatisfactoryPlanner.Modules.Warehouses.UnitTests.ItemSources
{
    public static class ItemSourceTests
    {
        public class RegisterTests : UnitTest
        {
            [Test]
            public void WhenDataIsValid_IsSuccessful()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var source = Source.Node(new SourceId(Guid.NewGuid()), "Iron Ore - Grassy Fields 1");
                var itemSource = ItemSource.Register(worldId, source);

                var domainEvent = DomainEventAssertions.AssertPublishedEvent<ItemSourceRegisteredDomainEvent>(itemSource);
                domainEvent.ItemSourceId.Should().Be(itemSource.Id);
                domainEvent.WorldNode.Should().Be(worldId);
                domainEvent.Source.Should().Be(source);
            }
        }

        public class ProducesTests : UnitTest
        {
            [Test]
            public void WhenDataIsValid_IsSuccessful()
            {
                var itemSource = new ItemSourceFixture().CreateForNode();

                itemSource.Produces(Item.IronOre, Rate.Of(0));

                var domainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProducedDomainEvent>(itemSource);
                domainEvent.ItemSourceId.Should().Be(itemSource.Id);
                domainEvent.ItemId.Should().Be(Item.IronOre.Id);
                domainEvent.Rate.Should().Be(0);
            }
        }

        public class ChangeProductionRateTests : UnitTest
        {
            [Test]
            public void WhenDataIsValid_IsSuccessful()
            {
                var itemSource = new ItemSourceFixture().CreateForNode();
                itemSource.Produces(Item.IronOre, Rate.Of(0));

                itemSource.ChangeProductionRate(Item.IronOre, Rate.Of(250.125m));

                var domainEvent = DomainEventAssertions.AssertPublishedEvent<ProductionRateChangedDomainEvent>(itemSource);
                domainEvent.ItemSourceId.Should().Be(itemSource.Id);
                domainEvent.ItemId.Should().Be(Item.IronOre.Id);
                domainEvent.Rate.Should().Be(250.125m);

                DomainEventsTestHelper.ClearAllDomainEvents(itemSource);

                itemSource.ChangeProductionRate(Item.IronOre, Rate.Of(314));

                domainEvent = DomainEventAssertions.AssertPublishedEvent<ProductionRateChangedDomainEvent>(itemSource);
                domainEvent.ItemSourceId.Should().Be(itemSource.Id);
                domainEvent.ItemId.Should().Be(Item.IronOre.Id);
                domainEvent.Rate.Should().Be(314);

            }
        }
    }
}
