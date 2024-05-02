using Autofac;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Infrastructure;

namespace SatisfactoryPlanner.API.Configuration.Modules
{
    public class ProductionAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductionModule>()
                .As<IProductionModule>()
                .InstancePerLifetimeScope();
        }
    }
}