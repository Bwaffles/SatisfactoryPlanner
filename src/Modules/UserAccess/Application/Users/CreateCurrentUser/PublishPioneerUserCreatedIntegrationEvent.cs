using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Events;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    internal class PublishPioneerUserCreatedIntegrationEvent(IEventsBus eventsBus) : IDomainEventNotificationHandler<PioneerUserCreatedNotification, PioneerUserCreatedDomainEvent>
    {
        private readonly IEventsBus _eventsBus = eventsBus;

        public async Task Handle(PioneerUserCreatedNotification notification, CancellationToken cancellationToken) =>
            await _eventsBus.Publish(new PioneerUserCreatedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.UserId.Value
            ));
    }
}