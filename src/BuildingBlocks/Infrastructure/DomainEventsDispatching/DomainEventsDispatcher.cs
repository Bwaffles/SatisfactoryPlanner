using Autofac;
using Autofac.Core;
using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcher(
        IMediator mediator,
        ILifetimeScope scope,
        IOutbox outbox,
        IDomainEventsAccessor domainEventsProvider,
        IDomainEventNotificationMapper domainNotificationsMapper) : IDomainEventsDispatcher
    {
        private readonly IDomainEventsAccessor _domainEventsProvider = domainEventsProvider;
        private readonly IDomainEventNotificationMapper _domainNotificationsMapper = domainNotificationsMapper;
        private readonly IMediator _mediator = mediator;
        private readonly IOutbox _outbox = outbox;
        private readonly ILifetimeScope _scope = scope;

        public async Task DispatchEventsAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                var domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                    domainEventNotifications.Add((domainNotification as IDomainEventNotification<IDomainEvent>)!);
            }

            _domainEventsProvider.ClearAllDomainEvents();

            // Publish domain events to be handled immediately.
            // Domain events are handled as part of the transaction that caused it.
            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);

            // Save domain event notifications to the outbox to be handled when able.
            // Saving notifications to the outbox happens in the same transaction and must succeed or the transaction is rolled back.
            // Notifications are not handled in the same transaction that caused it.
            foreach (var domainEventNotification in domainEventNotifications)
            {
                var type = _domainNotificationsMapper.GetName(domainEventNotification.GetType());
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });

                var outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);

                _outbox.Add(outboxMessage);
            }
        }
    }
}