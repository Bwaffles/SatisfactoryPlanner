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
        private readonly BiDictionary<string, Type> _domainNotificationsMap;

        public OutboxModule(BiDictionary<string, Type> domainNotificationsMap) =>
            _domainNotificationsMap = domainNotificationsMap;

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OutboxAccessor>()
                .As<IOutbox>()
                .FindConstructorsWith(new AllConstructorFinder())
                .InstancePerLifetimeScope();

            CheckMappings();

            builder.RegisterType<DomainNotificationsMapper>()
                .As<IDomainNotificationsMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("domainNotificationsMap", _domainNotificationsMap)
                .SingleInstance();
        }

        private void CheckMappings()
        {
            var domainEventNotifications = Assemblies.Application
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)))
                .ToList();

            var notMappedNotifications = domainEventNotifications
                .Where(domainEventNotification =>
                    !_domainNotificationsMap.TryGetBySecond(domainEventNotification, out _))
                .ToList();

            if (notMappedNotifications.Any())
                throw new ApplicationException(
                    $"Domain Event Notifications {notMappedNotifications.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
        }
    }
}