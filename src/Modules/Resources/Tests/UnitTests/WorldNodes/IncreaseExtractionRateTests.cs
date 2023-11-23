using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    [TestFixture]
    public class IncreaseExtractionRateTests
    {
        // Happy path tests
        [Test]
        public void WhenDataIsValid_IsSuccessful()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateIncreasedDomainEvent>(worldNode);
            domainEvent.WorldNodeId.Should().Be(worldNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(120));
        }

        [Test]
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
        [Test]
        public void WhenWorldNodeIsNotTapped_RuleIsBroken()
        {
            var (worldNode, extractionRateCalculator) = Setup(120, false);

            RuleAssertions.AssertBrokenRule<MustBeTappedRule>(() =>
            {
                worldNode.IncreaseExtractionRate(ExtractionRate.Of(120), extractionRateCalculator);
            });
        }

        [Test]
        public void WhenIncreasingAboveTheMaxRate_RuleIsBroken()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            RuleAssertions.AssertBrokenRule<CannotIncreaseExtractionRateAboveMaxExtractionRateRule>(() =>
            {
                worldNode.IncreaseExtractionRate(ExtractionRate.Of(121), extractionRateCalculator);
            });
        }

        [Test]
        public void WhenNewRateIsLessThanCurrentRate_RuleIsBroken()
        {
            var (worldNode, extractionRateCalculator) = Setup(120);

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(60), extractionRateCalculator);

            RuleAssertions.AssertBrokenRule<CannotIncreaseExtractionRateBelowCurrentExtractionRateRule>(() =>
            {
                worldNode.IncreaseExtractionRate(ExtractionRate.Of(59), extractionRateCalculator);
            });
        }

        private static (WorldNode worldNode, IExtractionRateCalculator extractionRateCalculator) Setup(
            decimal maxExtractionRate, bool isTapped = true)
        {
            var worldNodeFixture = new WorldNodeFixture();
            if (isTapped)
                worldNodeFixture.IsTapped();

            var worldNodeTestData = worldNodeFixture.Create();
            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            if (isTapped)
                mockExtractionRateCalculator
                    .GetMaxExtractionRate(worldNodeTestData.NodeId, worldNodeTestData.Extractor!.Id)
                    .Returns(ExtractionRate.Of(maxExtractionRate));

            return (worldNodeTestData.WorldNode, mockExtractionRateCalculator);
        }
    }
}