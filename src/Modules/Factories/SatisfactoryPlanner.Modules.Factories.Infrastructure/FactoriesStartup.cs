using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration.Mediation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure
{
    public class FactoriesStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString, IExecutionContextAccessor executionContextAccessor)
        {
            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor
                //,
                //moduleLogger,
                //emailsConfiguration,
                //eventsBus
                );
        }

        private static void ConfigureCompositionRoot(
           string connectionString,
           IExecutionContextAccessor executionContextAccessor
            //,
           //ILogger logger,
           //EmailsConfiguration emailsConfiguration,
           //IEventsBus eventsBus
            )
        {
            var containerBuilder = new ContainerBuilder();

            //containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Meetings")));

            //var loggerFactory = new SerilogLoggerFactory(logger);
            //containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            //containerBuilder.RegisterModule(new ProcessingModule());
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

            FactoriesCompositionRoot.SetContainer(_container);
        }
    }
}
