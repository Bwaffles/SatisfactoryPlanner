using Autofac;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Outbox;
using System;
using System.Linq;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.Outbox
{
    internal class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
               .As<IOutbox>()
               .FindConstructorsWith(new AllConstructorFinder())
               .InstancePerLifetimeScope();

            var domainEventNotifications = Assemblies.Application
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)));

            var domainNotificationsMap = new BiDictionary<string, Type>();

            foreach (var domainEventNotification in domainEventNotifications)
                domainNotificationsMap.Add(domainEventNotification.Name, domainEventNotification);

            builder.RegisterType<DomainEventNotificationMapper>()
                .As<IDomainEventNotificationMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("domainNotificationsMap", domainNotificationsMap)
                .SingleInstance();
        }
    }
}