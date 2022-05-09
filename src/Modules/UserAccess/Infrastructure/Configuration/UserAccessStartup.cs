using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Emails;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Emails;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Email;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.EventsBus;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Security;
using Serilog;
using Serilog.Extensions.Logging;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration
{
    public class UserAccessStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender)
        {
            var moduleLogger = logger.ForContext("Module", "UserAccess");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                logger,
                emailsConfiguration,
                textEncryptionKey,
                emailSender);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            EmailsConfiguration emailsConfiguration,
            string textEncryptionKey,
            IEmailSender emailSender)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "UserAccess")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule());
            containerBuilder.RegisterModule(new MediatorModule());

            var domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("NewUserRegisteredNotification", typeof(NewUserRegisteredNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            containerBuilder.RegisterModule(new QuartzModule());
            containerBuilder.RegisterModule(new EmailModule(emailsConfiguration, emailSender));
            containerBuilder.RegisterModule(new SecurityModule(textEncryptionKey));

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            UserAccessCompositionRoot.SetContainer(_container);
        }
    }
}
