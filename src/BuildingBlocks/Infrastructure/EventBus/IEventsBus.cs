using System;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventsBus : IDisposable
    {
        Task Publish<T>(T @event)
            where T : IntegrationEvent;

        void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent;

        void StartConsuming();
    }
}