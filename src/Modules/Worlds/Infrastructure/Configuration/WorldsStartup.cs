using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Worlds.Application.Worlds.CreateStarterWorld;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Worlds module.
    ///     Should be called from the main application startup.
    /// </summary>
    public static class WorldsStartup
    {
        public static void Start(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus)
        {
            var moduleLogger = logger.ForContext("Module", "Worlds");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, eventsBus);

            QuartzStartup.Initialize(moduleLogger);
            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop() => QuartzStartup.Shutdown();

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add(nameof(WorldCreatedNotification), typeof(WorldCreatedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            WorldsCompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}