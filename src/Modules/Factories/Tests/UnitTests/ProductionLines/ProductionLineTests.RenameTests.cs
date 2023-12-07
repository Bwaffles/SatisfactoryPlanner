using NSubstitute;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProductionLines
{
    public partial class ProductionLineTests
    {
        [TestFixture]
        public class RenameTests
        {
            [Test]
            public void CanRenameAProductionLine()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Original Name on Set Up");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);

                productionLine.Rename(ProductionLineName.As("New Name After Rename"), counter);

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProductionLineRenamedDomainEvent>(productionLine);

                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.Name.Should().Be(ProductionLineName.As("New Name After Rename"));
            }

            [Test]
            public void IgnoreWhenRenamingToCurrentName()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Original Name on Set Up");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);

                productionLine.Rename(ProductionLineName.As("Original Name on Set Up"), counter);

                DomainEventAssertions
                    .AssertEventIsNotPublished<ProductionLineRenamedDomainEvent>(productionLine,
                        "because the production line is already using the new name and nothing has changed.");
            }

            [Test]
            public void CannotRenameToAnExistingNameInWorld()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);

                var newName = ProductionLineName.As("Completely different name");
                counter.CountProductionLinesWithName(worldId, newName)
                    .Returns(1); // production line with new name already exists

                RuleAssertions.AssertBrokenRule<ProductionLineNameMustBeUniqueRule>(() =>
                {
                    productionLine.Rename(newName, counter);
                });
            }

            [Test]
            public void CanRenameToNameUsedInAnotherWorld()
            {
                var worldAId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldAId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldAId, productionLineName, counter);

                var newName = ProductionLineName.As("Completely different name");
                var worldBId = new WorldId(Guid.NewGuid());
                counter.CountProductionLinesWithName(worldBId, newName)
                    .Returns(1); // New name exists in world B, but I should be able to use it in world A

                RuleAssertions.AssertRuleNotBroken<ProductionLineNameMustBeUniqueRule>(() =>
                {
                    productionLine.Rename(newName, counter);
                });
            }
        }
    }
}