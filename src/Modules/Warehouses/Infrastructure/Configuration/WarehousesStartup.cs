using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Mediation;
using Serilog;
using Serilog.Extensions.Logging;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Warehouses module.
    ///     Should be called from the main application startup.
    /// </summary>
    public static class WarehousesStartup
    {
        public static void Start(string connectionString, IExecutionContextAccessor executionContextAccessor,
            ILogger logger, IEventsBus eventsBus)
        {
            var moduleLogger = logger.ForContext("Module", "Warehouses");

            ConfigureCompositionRoot(connectionString, executionContextAccessor, moduleLogger, eventsBus);

            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop() { }

        private static void ConfigureCompositionRoot(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger, IEventsBus eventsBus)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, new SerilogLoggerFactory(logger)));

            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            CompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}
