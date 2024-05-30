using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.EventBus
{
    public class InMemoryEventBusClient : IEventsBus
    {
        private bool disposedValue;

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            await InMemoryEventBus.Instance.Publish(@event);
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            InMemoryEventBus.Instance.Subscribe(handler);
        }

        public void Stop() => InMemoryEventBus.Instance.Reset();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}