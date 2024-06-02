using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Events
{
    public interface IDomainEventNotification<out TDomainEvent> : IDomainEventNotification
        where TDomainEvent : IDomainEvent
    {
        TDomainEvent DomainEvent { get; }
    }

    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
}