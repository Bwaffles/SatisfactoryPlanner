using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds.Events;
using SatisfactoryPlanner.Modules.Worlds.IntegrationEvents;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld
{
    public class PublishWorldCreatedIntegrationEvent(IEventsBus eventsBus) : IDomainEventNotificationHandler<WorldCreatedNotification, WorldCreatedDomainEvent>
    {
        private readonly IEventsBus _eventsBus = eventsBus;

        public async Task Handle(WorldCreatedNotification notification, CancellationToken cancellationToken)
            => await _eventsBus.Publish(new WorldCreatedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.WorldId.Value
            ));
    }
}