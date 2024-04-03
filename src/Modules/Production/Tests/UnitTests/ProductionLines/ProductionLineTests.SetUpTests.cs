using NSubstitute;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules;

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
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                var productionLine = ProductionLine.SetUp(worldId, productionLineName, counter);

                var domainEvent =
                    DomainEventAssertions.AssertPublishedEvent<ProductionLineSetUpDomainEvent>(productionLine);

                domainEvent.ProductionLineId.Should().Be(productionLine.Id);
                domainEvent.WorldId.Should().Be(worldId);
                domainEvent.Name.Should().Be(productionLineName);
            }

            [Test]
            public void CanUseANameFromAnotherWorld()
            {
                var worldAId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldAId, productionLineName).Returns(0);

                ProductionLine.SetUp(worldAId, productionLineName, counter);

                counter.CountProductionLinesWithName(worldAId, productionLineName).Returns(1); // Name exists in world a now

                var worldBId = new WorldId(Guid.NewGuid());
                counter.CountProductionLinesWithName(worldBId, productionLineName)
                    .Returns(0); // World b doesn't have the name yet

                RuleAssertions.AssertRuleNotBroken<ProductionLineNameMustBeUniqueRule>(() =>
                {
                    ProductionLine.SetUp(worldBId, productionLineName, counter);
                });
            }

            [Test]
            public void CannotUseAnExistingNameInWorld()
            {
                var worldId = new WorldId(Guid.NewGuid());
                var productionLineName = ProductionLineName.As("Rocky Desert Iron Ingots - Line 1");
                var counter = Substitute.For<IProductionLineCounter>();
                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(0);

                ProductionLine.SetUp(worldId, productionLineName, counter);

                counter.CountProductionLinesWithName(worldId, productionLineName).Returns(1);

                RuleAssertions.AssertBrokenRule<ProductionLineNameMustBeUniqueRule>(() =>
                {
                    ProductionLine.SetUp(worldId, productionLineName, counter);
                });
            }
        }
    }
}