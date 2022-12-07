using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration.Processing;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Factories module.
    ///     This will set up the logging and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class FactoriesStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger)
        {
            ConfigureCompositionRoot(connectionString, executionContextAccessor, logger);
        }

        private static void ConfigureCompositionRoot(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Factories")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new ProcessingModule());
            //containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            //containerBuilder.RegisterModule(new AuthenticationModule());

            //var domainNotificationsMap = new BiDictionary<string, Type>();
            //domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            //containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));
            
            //containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            FactoriesCompositionRoot.SetContainer(_container);
        }
    }
}
