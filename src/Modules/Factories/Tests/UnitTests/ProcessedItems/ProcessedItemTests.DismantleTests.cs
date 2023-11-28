using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProcessedItems
{
    [TestFixture]
    public class ProcessedItemTests
    {
        [TestFixture]
        public class DismantleTests
        {
            [Test]
            public void CanDismantleProcessedItem()
            {
                var productionLine = ProductionLine.SetUp(ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
                var item = Item.As();
                var recipe = Recipe.As([item]);

                var processedItem = productionLine.ProcessItem(item, recipe);

                processedItem.Dismantle();

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProcessedItemDismantledDomainEvent>(processedItem);

                domainEvent.ProcessedItemId.Should().Be(processedItem.Id);
            }
        }
    }
}