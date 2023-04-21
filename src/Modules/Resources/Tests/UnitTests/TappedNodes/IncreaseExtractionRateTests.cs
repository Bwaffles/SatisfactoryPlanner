using FluentAssertions;
using Moq;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes.Rules;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.TappedNodes
{
    public class IncreaseExtractionRateTests
    {
        [Fact]
        public void ValidData_IsSuccessful()
        {
            var (tappedNode, extractionRateCalculator) = Setup(120);

            tappedNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateIncreasedDomainEvent>(tappedNode);
            domainEvent.TappedNodeId.Should().Be(tappedNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(120));
        }

        [Fact]
        public void NewRateIsSameAsTheCurrentRate_IsIgnored()
        {
            var (tappedNode, extractionRateCalculator) = Setup(120);

            tappedNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);
            tappedNode.ClearDomainEvents();

            tappedNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);

            DomainEventAssertions.AssertEventIsNotPublished<ExtractionRateIncreasedDomainEvent>(tappedNode,
                "because the rate didn't change");
        }

        [Fact]
        public void IncreasingHigherThanMaxRate_IsNotPossible()
        {
            var (tappedNode, extractionRateCalculator) = Setup(120);

            RuleAssertions.AssertBrokenRule<CannotIncreaseExtractionRateAboveTheMaxExtractionRateRule>(() =>
            {
                tappedNode.IncreaseExtractionRate(ExtractionRate.Of(121), extractionRateCalculator);
            });
        }

        [Fact]
        public void NewRateIsLessThanCurrentRate_IsNotPossible()
        {
            var (tappedNode, extractionRateCalculator) = Setup(120);

            tappedNode.IncreaseExtractionRate(ExtractionRate.Of(60), extractionRateCalculator);

            RuleAssertions.AssertBrokenRule<CannotLowerExtractionRateBelowCurrentExtractionRateRule>(() =>
            {
                tappedNode.IncreaseExtractionRate(ExtractionRate.Of(59), extractionRateCalculator);
            });
        }

        private static (TappedNode tappedNode, IExtractionRateCalculator extractionRateCalculator) Setup(
            decimal maxExtractionRate)
        {
            var (tappedNode, _, nodeId, extractorId) = new TapNodeExecuter().Execute();
            var mockExtractionRateCalculator = new Mock<IExtractionRateCalculator>();
            mockExtractionRateCalculator
                .Setup(_ => _.GetMaxExtractionRate(nodeId, extractorId))
                .Returns(ExtractionRate.Of(maxExtractionRate));

            return (tappedNode, mockExtractionRateCalculator.Object);
        }
    }
}