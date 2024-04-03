using Autofac;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Infrastructure;

namespace SatisfactoryPlanner.API.Modules.Production
{
    public class ProductionAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FactoriesModule>()
                .As<IFactoriesModule>()
                .InstancePerLifetimeScope();
        }
    }
}