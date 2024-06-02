using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Events
{
    public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : IDomainEventNotification<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        public Guid Id { get; } = domainEvent.Id;

        public TDomainEvent DomainEvent { get; } = domainEvent;
    }
}