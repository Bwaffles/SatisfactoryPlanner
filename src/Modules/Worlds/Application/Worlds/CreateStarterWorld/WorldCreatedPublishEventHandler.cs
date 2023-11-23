using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Worlds.IntegrationEvents;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    // ReSharper disable once UnusedMember.Global
    public class WorldCreatedPublishEventHandler : INotificationHandler<WorldCreatedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public WorldCreatedPublishEventHandler(IEventsBus eventsBus) => _eventsBus = eventsBus;

        public async Task Handle(WorldCreatedNotification notification, CancellationToken cancellationToken)
            => await _eventsBus.Publish(new WorldCreatedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.WorldId.Value
            ));
    }
}