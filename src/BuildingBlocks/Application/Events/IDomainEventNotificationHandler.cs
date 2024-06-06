using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Events
{
    /// <summary>
    /// Marker interface for handling domain event notifications. These handlers happen in a seperate transaction to the one that triggered it.
    /// You can have multiple independent handlers for the same domain event notification.
    /// Typical tasks that could be handled this way are publishing integration events, sending emails, or updating other data
    /// in the module that doesn't need to be immediately consistent.
    /// </summary>
    public interface IDomainEventNotificationHandler<in TNotification, TDomainEvent> : INotificationHandler<TNotification>
        where TNotification : IDomainEventNotification<TDomainEvent>
        where TDomainEvent : IDomainEvent;
}
