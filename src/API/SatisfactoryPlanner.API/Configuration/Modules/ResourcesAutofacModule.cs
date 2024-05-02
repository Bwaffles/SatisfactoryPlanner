using Autofac;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;

namespace SatisfactoryPlanner.API.Configuration.Modules
{
    public class ResourcesAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResourcesModule>()
                .As<IResourcesModule>()
                .InstancePerLifetimeScope();
        }
    }
}