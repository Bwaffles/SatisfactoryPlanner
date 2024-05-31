using System;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Mapper for all <see cref="IDomainEventNotification"/>s in the module to help with serializing and deserializing notifications from the Outbox.
    /// </summary>
    public interface IDomainEventNotificationMapper
    {
        string GetName(Type type);

        Type GetType(string name);
    }
}