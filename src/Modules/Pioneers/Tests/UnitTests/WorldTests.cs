using SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;

namespace SatisfactoryPlanner.Modules.Worlds.Domain.UnitTests
{
    [TestFixture]
    public class WorldTests : TestBase
    {
        [Test]
        public void CreateStarterWorld_IsSuccessful()
        {
            var date = DateTime.UtcNow;
            SystemClock.Set(date);
            var pioneerId = new PioneerId(Guid.NewGuid());

            var world = World.CreateStarterWorld(pioneerId);

            var worldCreatedDomainEvent = DomainEventAssertions.AssertPublishedEvent<WorldCreatedDomainEvent>(world);

            worldCreatedDomainEvent.WorldId.Should().Be(world.Id);
            worldCreatedDomainEvent.Name.Should().Be("Starter World");
            worldCreatedDomainEvent.CreatorId.Should().Be(pioneerId);
            worldCreatedDomainEvent.CreateDate.Should().Be(date);
        }
    }
}