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
    public class IncreaseExtractionRateTests
    {
        // Happy path tests
        [Fact]
        public void WhenDataIsValid_IsSuccessful()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateIncreasedDomainEvent>(worldNode);
            domainEvent.WorldNodeId.Should().Be(worldNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(120));
        }

        [Fact]
        public void WhenNewRateIsSameAsTheCurrentRate_IsIgnored()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);
            worldNode.ClearDomainEvents();

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);

            DomainEventAssertions.AssertEventIsNotPublished<ExtractionRateIncreasedDomainEvent>(worldNode,
                "because the rate didn't change");
        }

        // Business rule tests
        [Fact]
        public void WhenIncreasingHigherThanMaxRate_RuleIsBroken()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            RuleAssertions.AssertBrokenRule<CannotIncreaseExtractionRateAboveTheMaxExtractionRateRule>(() =>
            {
                worldNode.IncreaseExtractionRate(ExtractionRate.Of(121), extractionRateCalculator);
            });
        }

        [Fact]
        public void WhenNewRateIsLessThanCurrentRate_RuleIsBroken()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(60), extractionRateCalculator);

            RuleAssertions.AssertBrokenRule<CannotLowerExtractionRateBelowCurrentExtractionRateRule>(() =>
            {
                worldNode.IncreaseExtractionRate(ExtractionRate.Of(59), extractionRateCalculator);
            });
        }

        private static (WorldNode worldNode, IExtractionRateCalculator extractionRateCalculator) Setup(
            decimal maxExtractionRate)
        {
            var (worldNode, _, nodeId, extractorId) = new TapNodeExecuter().Execute();
            var mockExtractionRateCalculator = new Mock<IExtractionRateCalculator>();
            mockExtractionRateCalculator
                .Setup(_ => _.GetMaxExtractionRate(nodeId, extractorId))
                .Returns(ExtractionRate.Of(maxExtractionRate));

            return (worldNode, mockExtractionRateCalculator.Object);
        }
    }
}