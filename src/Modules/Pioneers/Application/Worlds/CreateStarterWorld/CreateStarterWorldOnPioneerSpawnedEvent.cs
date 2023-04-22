using MediatR;
using SatisfactoryPlanner.Modules.Worlds.Domain.Pioneers.Events;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    // ReSharper disable once UnusedMember.Global
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