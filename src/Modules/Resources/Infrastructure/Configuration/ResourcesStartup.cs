using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
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
using System;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Resources module.
    ///     This will set up the logging and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class ResourcesStartup
    {
        public static void Initialize(string connectionString, IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Resources");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, logger);

            QuartzStartup.Initialize(moduleLogger);
            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop()
        {
            QuartzStartup.Shutdown();
        }

        private static void ConfigureCompositionRoot(string connectionString,
            IExecutionContextAccessor executionContextAccessor, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Resources")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule());
            containerBuilder.RegisterModule(new MediatorModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            //domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            ResourcesCompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}