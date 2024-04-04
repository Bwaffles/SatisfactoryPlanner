using NSubstitute;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Production.UnitTests.ProcessedItems
{
    [TestFixture]
    public partial class ProcessedItemTests
    {
        [TestFixture]
        public class DismantleTests
        {
            [Test]
            public void CanDismantleProcessedItem()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);
                var itemId = new ItemId(Guid.NewGuid());
                var ingredient = Ingredient.Of(itemId);
                var recipe = Recipe.As([ingredient]);

                var processedItem = productionLine.ProcessItem(itemId, recipe);

                processedItem.Dismantle();

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProcessedItemDismantledDomainEvent>(processedItem);

                domainEvent.ProcessedItemId.Should().Be(processedItem.Id);
            }
        }
    }
}