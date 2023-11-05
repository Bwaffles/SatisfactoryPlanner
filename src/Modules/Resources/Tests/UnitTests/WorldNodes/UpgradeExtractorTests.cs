using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    public class UpgradeExtractorTests
    {
        // Happy path tests
        [Fact]
        public void WhenDataIsValid_IsSuccessful()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var slowestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(slowestExtractor, resourceId)
                .Create();

            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            worldNodeTestData.WorldNode.UpgradeExtractor(fastestExtractor, resourceId, worldNodeTestData.Extractor);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractorUpgradedDomainEvent>(worldNodeTestData.WorldNode);
            domainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
            domainEvent.ExtractorId.Should().Be(fastestExtractor.Id);
        }

        [Fact]
        public void WhenExtractorIsSameAsTheCurrentExtractor_IsIgnored()
        {
            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped()
                .Create();

            worldNodeTestData.WorldNode.UpgradeExtractor(worldNodeTestData.Extractor!, worldNodeTestData.ResourceId, worldNodeTestData.Extractor);

            DomainEventAssertions.AssertEventIsNotPublished<ExtractorUpgradedDomainEvent>(worldNodeTestData.WorldNode,
                "because the extractor didn't change");
        }

        // Business rule tests
        [Fact]
        public void WhenWorldNodeIsNotTapped_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();

            RuleAssertions.AssertBrokenRule<MustBeTappedRule>(() =>
            {
                worldNodeTestData.WorldNode.UpgradeExtractor(worldNodeTestData.Extractor!, worldNodeTestData.ResourceId, null);
            });
        }

        [Fact]
        public void WhenExtractorCannotExtractTheResource_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped()
                .Create();

            var badExtractor = new ExtractorFixture()
                .CannotExtract(worldNodeTestData.ResourceId)
                .Create();

            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                worldNodeTestData.WorldNode.UpgradeExtractor(badExtractor, worldNodeTestData.ResourceId, worldNodeTestData.Extractor);
            });
        }

        [Fact]
        public void WhenExtractorIsSlowerThanCurrentExtractor_RuleIsBroken()
        {
            var resourceId = new ResourceId(Guid.NewGuid());
            var fastestExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsFastest()
                .Create();

            var worldNodeTestData = new WorldNodeFixture()
                .IsTapped(fastestExtractor, resourceId)
                .Create();

            var slowerExtractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .IsSlowest()
                .Create();

            RuleAssertions.AssertBrokenRule<CannotUpgradeToASlowerExtractorRule>(() =>
            {
                worldNodeTestData.WorldNode.UpgradeExtractor(slowerExtractor, worldNodeTestData.ResourceId, worldNodeTestData.Extractor);
            });
        }
    }
}