using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProcessedItems
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
                var productionLine = ProductionLine.SetUp(new WorldId(Guid.NewGuid()), ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
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