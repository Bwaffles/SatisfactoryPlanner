using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Pioneers module.
    ///     This will set up the logging, domain services and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class PioneersStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Pioneers");

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

            PioneersCompositionRoot.SetContainer(_container);
        }
    }
}