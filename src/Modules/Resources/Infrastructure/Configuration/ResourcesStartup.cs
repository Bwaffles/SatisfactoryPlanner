using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Resources module.
    ///     Should be called from the main application startup.
    /// </summary>
    public static class ResourcesStartup
    {
        public static void Start(string connectionString, IExecutionContextAccessor executionContextAccessor,
            ILogger logger, IEventsBus eventsBus, ResourcesConfiguration configuration)
        {
            var moduleLogger = logger.ForContext("Module", "Resources");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, eventsBus);

            QuartzStartup.Initialize(moduleLogger, configuration.InternalProcessingExecutionInterval);
            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop() => QuartzStartup.Shutdown();

        private static void ConfigureCompositionRoot(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger, IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, new SerilogLoggerFactory(logger)));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new OutboxModule());
            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            ResourcesCompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}