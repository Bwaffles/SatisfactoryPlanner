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
                var productionLine = ProductionLine.SetUp(new WorldId(Guid.NewGuid()), ProductionLineName.As("Original Name on Set Up"));

                productionLine.Rename(ProductionLineName.As("New Name After Rename"));

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProductionLineRenamedDomainEvent>(productionLine);

                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.Name.Should().Be(ProductionLineName.As("New Name After Rename"));
            }

            [Test]
            public void IgnoreWhenRenamingToCurrentName()
            {
                var productionLine = ProductionLine.SetUp(new WorldId(Guid.NewGuid()), ProductionLineName.As("Original Name on Set Up"));

                productionLine.Rename(ProductionLineName.As("Original Name on Set Up"));

                DomainEventAssertions
                    .AssertEventIsNotPublished<ProductionLineRenamedDomainEvent>(productionLine,
                        "because the production line is already using the new name and nothing has changed.");
            }
        }
    }
}