using Autofac;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Mediation;
using System.Reflection;
using Module = Autofac.Module;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Mediation
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            var mediatorOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            };

            builder.RegisterSource(new ScopedContravariantRegistrationSource(mediatorOpenTypes));

            foreach (var mediatorOpenType in mediatorOpenTypes)
                builder
                    .RegisterAssemblyTypes(ThisAssembly, Assemblies.Application)
                    .AsClosedTypesOf(mediatorOpenType)
                    .AsImplementedInterfaces()
                    .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            }).InstancePerLifetimeScope();
        }
    }
}