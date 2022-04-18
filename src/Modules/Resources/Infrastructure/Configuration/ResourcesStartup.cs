using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.DataAccess;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Domain;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Logging;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Mediation;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Processing;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration
{
    /// <summary>
    ///     Initialize the services and configurations for the Resources module.
    ///     This will set up the logging, emailing and dependency injection for this module.
    ///     Should be called from the main application Startup.
    /// </summary>
    public class ResourcesStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger)
        {
            ConfigureCompositionRoot(connectionString, executionContextAccessor, logger
                //, emailsConfiguration, eventsBus
                );
        }

        private static void ConfigureCompositionRoot(string connectionString, IExecutionContextAccessor executionContextAccessor, ILogger logger
            //, EmailsConfiguration emailsConfiguration, IEventsBus eventsBus
            )
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Factories")));

            var loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new DomainModule());
            containerBuilder.RegisterModule(new ProcessingModule());
            //containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            //containerBuilder.RegisterModule(new AuthenticationModule());

            //var domainNotificationsMap = new BiDictionary<string, Type>();
            //domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            //domainNotificationsMap.Add("MeetingGroupProposedNotification", typeof(MeetingGroupProposedNotification));
            //domainNotificationsMap.Add("MeetingGroupCreatedNotification", typeof(MeetingGroupCreatedNotification));
            //domainNotificationsMap.Add("MeetingAttendeeAddedNotification", typeof(MeetingAttendeeAddedNotification));
            //domainNotificationsMap.Add("MemberCreatedNotification", typeof(MemberCreatedNotification));
            //domainNotificationsMap.Add("MemberSubscriptionExpirationDateChangedNotification", typeof(MemberSubscriptionExpirationDateChangedNotification));
            //domainNotificationsMap.Add("MeetingCommentLikedNotification", typeof(MeetingCommentLikedNotification));
            //domainNotificationsMap.Add("MeetingCommentUnlikedNotification", typeof(MeetingCommentUnlikedNotification));
            //containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            //containerBuilder.RegisterModule(new EmailModule(emailsConfiguration));
            //containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            ResourcesCompositionRoot.SetContainer(_container);
        }
    }
}
