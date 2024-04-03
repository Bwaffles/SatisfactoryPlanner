using NSubstitute;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Events;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Production.UnitTests.ProductionLines
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

            [TestCase("Original Name on Set Up", "ORIGINAL NAME ON SET UP")]
            [TestCase("Original Name on Set Up", "Original Name on Set Up")]
            public void IgnoreWhenRenamingToCurrentName(string initialName, string renameName)
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As(initialName);
                var counter = Substitute.For<IProductionLineCounter>();
                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);

                productionLine.Rename(ProductionLineName.As(renameName), counter);

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