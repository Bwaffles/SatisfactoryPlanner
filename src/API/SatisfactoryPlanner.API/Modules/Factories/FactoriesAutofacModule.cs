using Autofac;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Infrastructure;

namespace SatisfactoryPlanner.API.Modules.Factories
{
    public class FactoriesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FactoriesModule>()
                .As<IFactoriesModule>()
                .InstancePerLifetimeScope();
        }
    }
}
