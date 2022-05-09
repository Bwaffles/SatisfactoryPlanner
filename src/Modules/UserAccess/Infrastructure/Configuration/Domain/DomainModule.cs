using Autofac;
using SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Domain
{
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}