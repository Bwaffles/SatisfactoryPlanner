using Autofac;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;

namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    public class UserAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserAccessModule>()
                .As<IUserAccessModule>()
                .InstancePerLifetimeScope();
        }
    }
}