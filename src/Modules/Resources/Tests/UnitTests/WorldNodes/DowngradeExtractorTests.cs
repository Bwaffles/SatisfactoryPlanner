using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    [TestFixture]
    public class DowngradeExtractorTests
    {
        // Happy path tests
        [Test]
        public void WhenDataIsValid_IsSuccessful()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(fastestExtractor, resourceId)
                .Create();

            var slowestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();
            mockExtractionRateCalculator
                .GetMaxExtractionRate(worldNodeTestData.NodeId, slowestExtractor.Id)
                .Returns(ExtractionRate.Of(780));

            worldNodeTestData.WorldNode.DowngradeExtractor(slowestExtractor, resourceId, worldNodeTestData.Extractor,
                mockExtractionRateCalculator);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractorDowngradedDomainEvent>(worldNodeTestData.WorldNode);
            domainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
            domainEvent.ExtractorId.Should().Be(slowestExtractor.Id);
        }

        [Test]
        public void WhenExtractorIsSameAsTheCurrentExtractor_IsIgnored()
        {
            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped()
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            worldNodeTestData.WorldNode.DowngradeExtractor(worldNodeTestData.Extractor!, worldNodeTestData.ResourceId,
                worldNodeTestData.Extractor, mockExtractionRateCalculator);

            DomainEventAssertions.AssertEventIsNotPublished<ExtractorDowngradedDomainEvent>(worldNodeTestData.WorldNode,
                "because the extractor didn't change");
        }

        [Test]
        public void WhenExtractingMoreThanNewMaxExtractionRate_ExtractionRateReduced()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(fastestExtractor, resourceId)
                .Create();

            var slowestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();
            mockExtractionRateCalculator.GetMaxExtractionRate(worldNodeTestData.NodeId, fastestExtractor.Id)
                .Returns(ExtractionRate.Of(780));

            mockExtractionRateCalculator
                .GetMaxExtractionRate(worldNodeTestData.NodeId, slowestExtractor.Id)
                .Returns(ExtractionRate.Of(300));

            worldNodeTestData.WorldNode.IncreaseExtractionRate(ExtractionRate.Of(780),
                mockExtractionRateCalculator);

            worldNodeTestData.WorldNode.DowngradeExtractor(slowestExtractor, resourceId, worldNodeTestData.Extractor,
                mockExtractionRateCalculator);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractionRateDecreasedDomainEvent>(worldNodeTestData
                    .WorldNode);
            domainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
            domainEvent.ExtractionRate.Should().Be(ExtractionRate.Of(300));
        }

        [Theory]
        [TestCase(300, 300)]
        [TestCase(300, 299)]
        public void WhenNotExtractingThanNewMaxExtractionRate_IsIgnored(int newMaxExtractionRate, int extractionRate)
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(fastestExtractor, resourceId)
                .Create();

            var slowestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();
            mockExtractionRateCalculator
                .GetMaxExtractionRate(worldNodeTestData.NodeId, fastestExtractor.Id)
                .Returns(ExtractionRate.Of(780));

            mockExtractionRateCalculator
                .GetMaxExtractionRate(worldNodeTestData.NodeId, slowestExtractor.Id)
                .Returns(ExtractionRate.Of(newMaxExtractionRate));

            worldNodeTestData.WorldNode.IncreaseExtractionRate(ExtractionRate.Of(extractionRate),
                mockExtractionRateCalculator);

            worldNodeTestData.WorldNode.DowngradeExtractor(slowestExtractor, resourceId, worldNodeTestData.Extractor,
                mockExtractionRateCalculator);

            DomainEventAssertions.AssertEventIsNotPublished<ExtractionRateDecreasedDomainEvent>(worldNodeTestData
                .WorldNode);
        }

        // Business rule tests
        [Test]
        public void WhenWorldNodeIsNotTapped_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            RuleAssertions.AssertBrokenRule<MustBeTappedRule>(() =>
            {
                worldNodeTestData.WorldNode.DowngradeExtractor(worldNodeTestData.Extractor!,
                    worldNodeTestData.ResourceId, null, mockExtractionRateCalculator);
            });
        }

        [Test]
        public void WhenExtractorCannotExtractTheResource_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped()
                .Create();

            var badExtractor = new ExtractorFixture()
                .CannotExtract(worldNodeTestData.ResourceId)
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                worldNodeTestData.WorldNode.DowngradeExtractor(badExtractor, worldNodeTestData.ResourceId,
                    worldNodeTestData.Extractor, mockExtractionRateCalculator);
            });
        }

        [Test]
        public void WhenExtractorIsFasterThanCurrentExtractor_RuleIsBroken()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var slowerExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(slowerExtractor, resourceId)
                .Create();

            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            var mockExtractionRateCalculator = Substitute.For<IExtractionRateCalculator>();

            RuleAssertions.AssertBrokenRule<CannotDowngradeToAFasterExtractorRule>(() =>
            {
                worldNodeTestData.WorldNode.DowngradeExtractor(fastestExtractor, worldNodeTestData.ResourceId,
                    worldNodeTestData.Extractor, mockExtractionRateCalculator);
            });
        }
    }
}