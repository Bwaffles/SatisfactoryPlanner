﻿using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Production module.
    ///     This will set up the logging and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class ProductionStartup
    {
        public static void Initialize(string connectionString, IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "Production");

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

            containerBuilder.RegisterModule(new LoggingModule(logger));

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

            ProductionCompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}