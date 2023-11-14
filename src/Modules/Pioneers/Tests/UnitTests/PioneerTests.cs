using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.UnitTests
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

            var pioneerSpawnedDomainEvent =
                DomainEventAssertions.AssertPublishedEvent<PioneerSpawnedDomainEvent>(pioneer);

            pioneerSpawnedDomainEvent.PioneerId.Should().Be(pioneer.Id);
        }
    }
}