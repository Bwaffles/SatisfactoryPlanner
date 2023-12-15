using NSubstitute;
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
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);
                var itemId = new ItemId(Guid.NewGuid());
                var ingredient = Ingredient.Of(itemId);
                var recipe = Recipe.As([ingredient]);

                var processedItem = productionLine.ProcessItem(itemId, recipe);

                var domainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(processedItem);

                domainEvent.ProcessedItemId.Should().Be(processedItem.Id);
                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.ItemId.Should().Be(itemId);
                domainEvent.Recipe.Should().Be(recipe);
            }

            [Test]
            public void CanProcessSameItemMultipleTimes()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);
                var itemId = new ItemId(Guid.NewGuid());
                var ingredient = Ingredient.Of(itemId);
                var recipe = Recipe.As([ingredient]);

                var firstProcessedItem = productionLine.ProcessItem(itemId, recipe);
                var firstDomainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(firstProcessedItem);
                firstDomainEvent.ProcessedItemId.Should().Be(firstProcessedItem.Id);

                var secondProcessedItem = productionLine.ProcessItem(itemId, recipe);
                var secondDomainEvent = DomainEventAssertions.AssertPublishedEvent<ItemProcessedDomainEvent>(secondProcessedItem);
                secondDomainEvent.ProcessedItemId.Should().Be(secondProcessedItem.Id);
            }

            [Test]
            public void CannotProcessItemWhenItsNotAnIngredientOfTheRecipe()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);
                var itemId = new ItemId(Guid.NewGuid());
                var recipe = Recipe.As([]);

                RuleAssertions.AssertBrokenRule<ItemMustBeIngredientOfRecipeRule>(
                    () => productionLine.ProcessItem(itemId, recipe)
                );
            }
        }
    }
}