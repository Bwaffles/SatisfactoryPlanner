using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.EventBus
{
    public sealed class InMemoryEventBus
    {
        private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

        public static InMemoryEventBus Instance { get; } = new();

        static InMemoryEventBus() { }

        private InMemoryEventBus()
        {
            _handlersDictionary = [];
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            var eventType = typeof(T).FullName;
            if (eventType == null)
                return;

            if (_handlersDictionary.TryGetValue(eventType, out var handlers))
            {
                if (handlers.Any(_ => _.GetType() == handler.GetType()))
                {
                    // Making the assumption that an event handler being subscribed more than once is a configuration error and won't come up normally.
                    // Want this to fail loud and fast so no one has to spend hours debugging code silently running twice.
                    throw new InvalidOperationException($"Handler '{handler.GetType().FullName}' is already subscribed to event '{eventType}'. " +
                        "Ensure you aren't subscribing with the same handler more than once.");
                }

                handlers.Add(handler);
            }
            else
            {
                _handlersDictionary.Add(eventType, [handler]);
            }
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            var eventType = @event.GetType().FullName;
            if (eventType == null)
                return;

            if (!_handlersDictionary.TryGetValue(eventType, out var integrationEventHandlers))
                throw new InvalidOperationException($"Event '{eventType}' published without any subscribers.");

            foreach (var integrationEventHandler in integrationEventHandlers)
            {
                if (integrationEventHandler is IIntegrationEventHandler<T> handler)
                    await handler.Handle(@event);
            }
        }

        public void Reset() { _handlersDictionary.Clear(); }
    }
}