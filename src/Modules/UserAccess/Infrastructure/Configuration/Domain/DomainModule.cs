using Autofac;
using SatisfactoryPlanner.UserAccess.Application.UserRegistrations.RegisterNewUser;
using SatisfactoryPlanner.UserAccess.Domain.UserRegistrations;

namespace SatisfactoryPlanner.UserAccess.Infrastructure.Configuration.Domain
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