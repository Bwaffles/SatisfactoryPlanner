using System;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <inheritdoc cref="IDomainEventNotificationMapper"/>
    public class DomainEventNotificationMapper(BiDictionary<string, Type> domainNotificationsMap) : IDomainEventNotificationMapper
    {
        private readonly BiDictionary<string, Type> _domainNotificationsMap = domainNotificationsMap;

        public string GetName(Type type)
        {
            if (_domainNotificationsMap.TryGetBySecond(type, out var name))
                return name;

            throw new InvalidOperationException();
        }

        public Type GetType(string name)
        {
            if (_domainNotificationsMap.TryGetByFirst(name, out var type))
                return type;

            throw new InvalidOperationException();
        }
    }
}