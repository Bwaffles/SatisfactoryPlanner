using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    public class TapWorldNodeTests
    {
        // Happy path tests
        [Fact]
        public void ValidData_IsSuccessful()
        {
            var (worldNode, worldId, nodeId) = new WorldNodeFixture().Create();
            var resourceId = new ResourceId(Guid.NewGuid());
            var extractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .Create();

            worldNode.Tap(extractor, resourceId);

            var domainEvent = DomainEventAssertions.AssertPublishedEvent<WorldNodeTappedDomainEvent>(worldNode);
            domainEvent.WorldNodeId.Should().Be(worldNode.Id);
            domainEvent.WorldId.Should().Be(worldId);
            domainEvent.NodeId.Should().Be(nodeId);
            domainEvent.ExtractorId.Should().Be(extractor.Id);
        }

        // Business rule tests
        [Fact]
        public void WhenNodeIsAlreadyTapped_RuleIsBroken()
        {
            var (worldNode, _, _) = new WorldNodeFixture().Create();
            var resourceId = new ResourceId(Guid.NewGuid());
            var extractor = new ExtractorFixture()
                .CanExtract(resourceId)
                .Create();

            worldNode.Tap(extractor, resourceId);

            RuleAssertions.AssertBrokenRule<CannotAlreadyBeTappedRule>(() =>
            {
                worldNode.Tap(extractor, resourceId);
            });
        }

        [Fact]
        public void WhenExtractorCannotExtractTheResource_RuleIsBroken()
        {
            var (worldNode, _, _) = new WorldNodeFixture().Create();
            var resourceId = new ResourceId(Guid.NewGuid());
            var extractor = new ExtractorFixture()
                .CannotExtract(resourceId)
                .Create();

            RuleAssertions.AssertBrokenRule<ExtractorMustBeAbleToExtractResourceRule>(() =>
            {
                worldNode.Tap(extractor, resourceId);
            });
        }
    }
}