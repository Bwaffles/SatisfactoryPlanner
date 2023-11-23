using FluentAssertions;
using NUnit.Framework;
using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    [TestFixture]
    public class DismantleExtractorTests
    {
        // Happy path tests
        [Test]
        public void WhenDataIsValid_IsSuccessful()
        {
            var worldNodeTestData = new WorldNodeFixture().IsTapped().Create();

            worldNodeTestData.WorldNode.DismantleExtractor();

            var extractorDismantledDomainEvent =
                DomainEventAssertions.AssertPublishedEvent<ExtractorDismantledDomainEvent>(worldNodeTestData.WorldNode);
            extractorDismantledDomainEvent.WorldNodeId.Should().Be(worldNodeTestData.WorldNode.Id);
        }

        [Test]
        public void WhenNotTapped_IsIgnored()
        {
            var worldNodeTestData = new WorldNodeFixture().Create();

            worldNodeTestData.WorldNode.DismantleExtractor();

            DomainEventAssertions
                .AssertEventIsNotPublished<ExtractorDismantledDomainEvent>(worldNodeTestData.WorldNode);
        }
    }
}