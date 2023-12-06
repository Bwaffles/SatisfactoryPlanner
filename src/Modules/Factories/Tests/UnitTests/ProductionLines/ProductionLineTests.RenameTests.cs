using NSubstitute;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;

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

                productionLine.Rename(ProductionLineName.As("New Name After Rename"));

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

                productionLine.Rename(ProductionLineName.As("Original Name on Set Up"));

                DomainEventAssertions
                    .AssertEventIsNotPublished<ProductionLineRenamedDomainEvent>(productionLine,
                        "because the production line is already using the new name and nothing has changed.");
            }
        }
    }
}