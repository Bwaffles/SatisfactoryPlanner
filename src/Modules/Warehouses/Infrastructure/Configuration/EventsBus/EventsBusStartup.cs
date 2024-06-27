using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Resources.IntegrationEvents;
using Serilog;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.EventsBus;

public static class EventsBusStartup
{
    public static void Initialize(ILogger logger)
    {
        var eventBus = CompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

        SubscribeToIntegrationEvent<NodeTappedIntegrationEvent>(eventBus, logger);
        SubscribeToIntegrationEvent<ExtractionRateIncreasedIntegrationEvent>(eventBus, logger);
    }

    private static void SubscribeToIntegrationEvent<T>(IEventsBus eventBus, ILogger logger)
        where T : IntegrationEvent
    {
        logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
        eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
    }
}