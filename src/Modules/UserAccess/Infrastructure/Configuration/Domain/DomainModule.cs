using Autofac;
using SatisfactoryPlanner.Modules.UserAccess.Application.Users.CreateCurrentUser;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Domain
{
    /// <summary>
    ///     Register the domain services for the module.
    /// </summary>
    internal class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Application.UserRegistrations.RegisterNewUser.UsersCounter>()
                .As<UserAccess.Domain.UserRegistrations.IUsersCounter>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}