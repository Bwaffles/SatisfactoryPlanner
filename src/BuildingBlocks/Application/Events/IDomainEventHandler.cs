using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Events
{
    /// <summary>
    /// Marker interface for direct handlers of domain events.
    /// These handlers execute in the same transaction as the domain event that triggered it and must succeed or it all rolls back.
    /// </summary>
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent;
}
