using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProductionLines
{
    [TestFixture]
    public partial class ProductionLineTests
    {
        [TestFixture]
        public class SetUpTests
        {
            [Test]
            public void CanSetUpAValidProductionLine()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLine = ProductionLine.SetUp(worldId, ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProductionLineSetUpDomainEvent>(productionLine);

                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.WorldId.Should().Be(worldId);
                domainEvent.Name.Should().Be(ProductionLineName.As("Rocky Desert Iron Ingots - Line 1"));
            }
        }
    }
}