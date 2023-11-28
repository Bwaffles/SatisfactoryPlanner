using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Rules;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProductionLines
{
    public partial class ProductionLineTests
    {
        [TestFixture]
        public class ProcessItemTests
        {
            [Test]
            public void CanProcessItem()
            {
                var productionLine = ProductionLine.SetUp(ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
                var item = Item.As();
                var recipe = Recipe.As([item]);

                var processedItem = productionLine.ProcessItem(item, recipe);

                var domainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(processedItem);

                domainEvent.ProcessedItemId.Should().Be(processedItem.Id);
                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.Item.Should().Be(item);
                domainEvent.Recipe.Should().Be(recipe);
            }

            [Test]
            public void CanProcessSameItemMultipleTimes()
            {
                var productionLine = ProductionLine.SetUp(ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
                var item = Item.As();
                var recipe = Recipe.As([item]);

                var firstProcessedItem = productionLine.ProcessItem(item, recipe);
                var firstDomainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(firstProcessedItem);
                firstDomainEvent.ProcessedItemId.Should().Be(firstProcessedItem.Id);

                var secondProcessedItem = productionLine.ProcessItem(item, recipe);
                var secondDomainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(secondProcessedItem);
                secondDomainEvent.ProcessedItemId.Should().Be(secondProcessedItem.Id);
            }

            [Test]
            public void CannotProcessItemWhenItsNotAnIngredientOfTheRecipe()
            {
                var productionLine = ProductionLine.SetUp(ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
                var item = Item.As();
                var recipe = Recipe.As([]);

                RuleAssertions.AssertBrokenRule<ItemMustBeIngredientOfRecipeRule>(
                    () => productionLine.ProcessItem(item, recipe)
                );
            }
        }
    }
}