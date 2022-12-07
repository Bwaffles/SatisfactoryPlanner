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
            builder.RegisterType<UsersCounter>()
                .As<IUsersCounter>()
                .InstancePerLifetimeScope();
        }
    }
}