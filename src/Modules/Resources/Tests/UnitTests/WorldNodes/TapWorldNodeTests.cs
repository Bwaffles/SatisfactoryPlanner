using FluentAssertions;
using NUnit.Framework;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Rules;
using SatisfactoryPlanner.Modules.Resources.UnitTests.Extractors;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    [TestFixture]
    public class TapWorldNodeTests
    {
        // Happy path tests
        [Test]
        public void WhenDataIsValid_IsSuccessful()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();
            var extractor = new ExtractorFixture()
                .CanExtract(worldNodeTestData.ResourceId)
                .Create();

            worldNodeTestData.WorldNode.Tap(extractor, worldNodeTestData.ResourceId);

            var domainEvent = DomainEventAssertions.AssertPublishedEvent<WorldNodeTappedDomainEvent>(worldNodeTestData.WorldNode);
            domainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
            domainEvent.ExtractorId.Should().Be(extractor.Id);
        }

        // Business rule tests
        [Test]
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

        [Test]
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