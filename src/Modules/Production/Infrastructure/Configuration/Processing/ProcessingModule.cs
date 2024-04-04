using Autofac;
using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application.Events;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing.InternalCommands;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainEventsDispatcher>()
                .As<IDomainEventsDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DomainEventsAccessor>()
                .As<IDomainEventsAccessor>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandsScheduler>()
                .As<ICommandsScheduler>()
                .InstancePerLifetimeScope();

            builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            builder.RegisterGenericDecorator(typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));
            builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            builder.RegisterGenericDecorator(typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));
            builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

            builder.RegisterGenericDecorator(
                typeof(DomainEventsDispatcherNotificationHandlerDecorator<>),
                typeof(INotificationHandler<>));

            builder.RegisterAssemblyTypes(Assemblies.Application)
                .AsClosedTypesOf(typeof(IDomainEventNotification<>))
                .InstancePerDependency()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}