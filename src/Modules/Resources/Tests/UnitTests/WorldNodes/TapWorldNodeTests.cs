using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    public class TapWorldNodeTests
    {
        // Happy path tests
        [Fact]
        public void WhenDataIsValid_IsSuccessful()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();
            var extractor = new ExtractorFixture()
                .CanExtract(worldNodeTestData.ResourceId)
                .Create();

            worldNodeTestData.WorldNode.Tap(extractor, worldNodeTestData.ResourceId);

            var domainEvent =
                DomainEventAssertions.AssertPublishedEvent<WorldNodeTappedDomainEvent>(worldNodeTestData.WorldNode);
            domainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
            domainEvent.WorldId.Should().Be(worldNodeTestData.WorldId);
            domainEvent.NodeId.Should().Be(worldNodeTestData.NodeId);
            domainEvent.ExtractorId.Should().Be(extractor.Id);
        }

        // Business rule tests
        [Fact]
        public void WhenNodeIsAlreadyTapped_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();
            var extractor = new ExtractorFixture()
                .CanExtract(worldNodeTestData.ResourceId)
                .Create();

            worldNodeTestData.WorldNode.Tap(extractor, worldNodeTestData.ResourceId);

            RuleAssertions.AssertBrokenRule<CannotAlreadyBeTappedRule>(() =>
            {
                worldNodeTestData.WorldNode.Tap(extractor, worldNodeTestData.ResourceId);
            });
        }

        [Fact]
        public void WhenExtractorCannotExtractTheResource_RuleIsBroken()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();
            var extractor = new ExtractorFixture()
                .CannotExtract(worldNodeTestData.ResourceId)
                .Create();

            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                worldNodeTestData.WorldNode.Tap(extractor, worldNodeTestData.ResourceId);
            });
        }
    }
}