using FluentAssertions;
using Moq;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    public class DecreaseExtractionRateTests
    {
        // Happy path tests
        [Fact]
        public void WhenDataIsValid_IsSuccessful()
        {
            var worldNode = Setup(120);

            worldNode.DecreaseExtractionRate(ExtractionRate.Of(60));

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateDecreasedDomainEvent>(worldNode);
            domainEvent.WorldNodeId.Should().Be(worldNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(60));
        }

        [Fact]
        public void WhenNewRateIsSameAsTheCurrentRate_IsIgnored()
        {
            var worldNode = Setup(120);

            worldNode.DecreaseExtractionRate(ExtractionRate.Of(120));

            DomainEventAssertions.AssertEventIsNotPublished<ExtractionRateDecreasedDomainEvent>(worldNode,
                "because the rate didn't change");
        }

        // Business rule tests
        [Fact]
        public void WhenNewRateIsGreaterThanCurrentRate_RuleIsBroken()
        {
            var worldNode = Setup(120);

            RuleAssertions.AssertBrokenRule<CannotDecreaseExtractionRateAboveCurrentExtractionRateRule>(() =>
            {
                worldNode.DecreaseExtractionRate(ExtractionRate.Of(121));
            });
        }

        private static WorldNode Setup(decimal extractionRate)
        {
            var (worldNode, _, nodeId, extractorId) = new TapWorldNodeExecuter().Execute();
            var mockExtractionRateCalculator = new Mock<IExtractionRateCalculator>();
            mockExtractionRateCalculator
                .Setup(_ => _.GetMaxExtractionRate(nodeId, extractorId))
                .Returns(ExtractionRate.Of(300));

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(extractionRate), mockExtractionRateCalculator.Object);

            return worldNode;
        }
    }
}