using FluentAssertions;
using NSubstitute;
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
        [Theory]
        [InlineData(60)]
        [InlineData(0)]
        public void WhenDataIsValid_IsSuccessful(int extractionRate)
        {
            var worldNode = Setup(120);

            worldNode.DecreaseExtractionRate(ExtractionRate.Of(extractionRate));

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateDecreasedDomainEvent>(worldNode);
            domainEvent.WorldNodeId.Should().Be(worldNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(extractionRate));
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
        public void WhenWorldNodeIsNotTapped_RuleIsBroken()
        {
            var worldNode = Setup(isTapped: false);

            RuleAssertions.AssertBrokenRule<MustBeTappedRule>(() =>
            {
                worldNode.DecreaseExtractionRate(ExtractionRate.Of(0));
            });
        }

        [Fact]
        public void WhenNewRateIsGreaterThanCurrentRate_RuleIsBroken()
        {
            var worldNode = Setup(120);

            RuleAssertions.AssertBrokenRule<CannotDecreaseExtractionRateAboveCurrentExtractionRateRule>(() =>
            {
                worldNode.DecreaseExtractionRate(ExtractionRate.Of(121));
            });
        }

        private static WorldNode Setup(decimal? extractionRate = null, bool isTapped = true)
        {
            var worldNodeFixture = new WorldNodeFixture();
            if (isTapped)
                worldNodeFixture.IsTapped();

            var worldNodeTestData = worldNodeFixture.Create();
            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            if (isTapped)
                mockExtractionRateCalculator
                    .GetMaxExtractionRate(worldNodeTestData.NodeId, worldNodeTestData.Extractor!.Id)
                    .Returns(ExtractionRate.Of(300));

            if (extractionRate != null)
                worldNodeTestData.WorldNode.IncreaseExtractionRate(ExtractionRate.Of(extractionRate.Value), mockExtractionRateCalculator);

            return worldNodeTestData.WorldNode;
        }
    }
}