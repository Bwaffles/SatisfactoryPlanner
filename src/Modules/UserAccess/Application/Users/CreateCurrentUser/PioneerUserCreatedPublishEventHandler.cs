using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser
{
    // ReSharper disable once UnusedMember.Global
    public class PioneerUserCreatedPublishEventHandler : INotificationHandler<PioneerUserCreatedNotification>
    {
        private readonly IEventsBus _eventsBus;

        public PioneerUserCreatedPublishEventHandler(IEventsBus eventsBus) => _eventsBus = eventsBus;

        public async Task Handle(PioneerUserCreatedNotification notification, CancellationToken cancellationToken) =>
            await _eventsBus.Publish(new PioneerUserCreatedIntegrationEvent(
                notification.Id,
                notification.DomainEvent.OccurredOn,
                notification.DomainEvent.UserId.Value
            ));
    }
}