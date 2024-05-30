using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Events
{
    public class DomainEventNotificationBase<T> : IDomainEventNotification<T>
        where T : IDomainEvent
    {
        public DomainEventNotificationBase(T domainEvent, Guid id)
        {
            Id = id;
            DomainEvent = domainEvent;
        }

        public T DomainEvent { get; }

        public Guid Id { get; }
    }
}