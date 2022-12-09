using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
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
    ///     This will set up the logging, domain services and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class WorldsStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Worlds");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger);

            QuartzStartup.Initialize(moduleLogger);
            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop() => QuartzStartup.Shutdown();

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger
        )
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule());
            containerBuilder.RegisterModule(new MediatorModule());
            //containerBuilder.RegisterModule(new AuthenticationModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            //domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            WorldsCompositionRoot.SetContainer(_container);
        }
    }
}