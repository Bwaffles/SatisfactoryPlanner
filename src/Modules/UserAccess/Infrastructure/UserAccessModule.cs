using Autofac;
using MediatR;
using SatisfactoryPlanner.UserAccess.Application.Contracts;
using SatisfactoryPlanner.UserAccess.Infrastructure.Configuration;
using SatisfactoryPlanner.UserAccess.Infrastructure.Configuration.Processing;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.UserAccess.Infrastructure
{
    public class UserAccessModule : IUserAccessModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = UserAccessCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}