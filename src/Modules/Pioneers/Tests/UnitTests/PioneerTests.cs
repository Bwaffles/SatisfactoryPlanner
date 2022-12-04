using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers.Events;

namespace SatisfactoryPlanner.Modules.Pioneers.Domain.UnitTests
{
    [TestFixture]
    public class PioneerTests
    {
        [Test]
        public void SpawnPioneer_IsSuccessful()
        {
            var pioneerId = Guid.NewGuid();
            var pioneer = Pioneer.Spawn(pioneerId);

            pioneer.Id.Value.Should().Be(pioneerId);

            var pioneerSpawnedDomainEvent = DomainEventAssertions.AssertPublishedEvent<PioneerSpawnedDomainEvent>(pioneer);

            pioneerSpawnedDomainEvent.PioneerId.Should().Be(pioneer.Id);
        }
    }
}