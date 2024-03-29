﻿using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                moduleLogger);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop()
        {
            QuartzStartup.Shutdown();
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
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
            domainNotificationsMap.Add(nameof(PioneerUserCreatedNotification), typeof(PioneerUserCreatedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            UserAccessCompositionRoot.SetContainer(containerBuilder.Build());
        }
    }
}