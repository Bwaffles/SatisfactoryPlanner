using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers.Events;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    // ReSharper disable once UnusedMember.Global
    public class CreateStarterWorldOnPioneerSpawnedEvent(IWorldRepository worldRepository) : IDomainEventHandler<PioneerSpawnedDomainEvent>
    {
        private readonly IWorldRepository _worldRepository = worldRepository;

        public async Task Handle(PioneerSpawnedDomainEvent notification, CancellationToken cancellationToken)
        {
            var world = World.CreateStarterWorld(notification.PioneerId);

            await _worldRepository.AddAsync(world);
        }
    }
}