﻿using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationEvents;
using Serilog;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.EventsBus
{
    public static class EventsBusStartup
    {
        public static void Initialize(
            ILogger logger)
        {
            SubscribeToIntegrationEvents(logger);
        }

        private static void SubscribeToIntegrationEvents(ILogger logger)
        {
            var eventBus = WorldsCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();

            SubscribeToIntegrationEvent<PioneerUserCreatedIntegrationEvent>(eventBus, logger);
        }

        private static void SubscribeToIntegrationEvent<T>(IEventsBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.Information("Subscribe to {@IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(
                new IntegrationEventGenericHandler<T>());
        }
    }
}