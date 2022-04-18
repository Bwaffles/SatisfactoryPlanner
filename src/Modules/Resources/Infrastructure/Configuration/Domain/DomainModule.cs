using Autofac;
using SatisfactoryPlanner.Modules.Resources.Application.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TappedNodeExistenceChecker>()
                .As<ITappedNodeExistenceChecker>()
                .InstancePerLifetimeScope();
        }
    }
}
