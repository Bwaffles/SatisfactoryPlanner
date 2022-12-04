using MediatR;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Pioneers.Events;
using SatisfactoryPlanner.Modules.Pioneers.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Worlds.CreateStarterWorld
{
    public class CreateStarterWorldOnPioneerSpawnedEvent : INotificationHandler<PioneerSpawnedDomainEvent>
    {
        private readonly IWorldRepository _worldRepository;

        public CreateStarterWorldOnPioneerSpawnedEvent(IWorldRepository worldRepository)
        {
            _worldRepository = worldRepository;
        }

        public async Task Handle(PioneerSpawnedDomainEvent notification, CancellationToken cancellationToken)
        {
            var world = World.CreateStarterWorld(notification.PioneerId);

            await _worldRepository.AddAsync(world);
        }
    }
}