using Autofac;
using MediatR;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration.Processing;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure
{
    public class WorldsModule : IWorldsModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command) =>
            await CommandsExecutor.Execute(command);

        public async Task ExecuteCommandAsync(ICommand command) => await CommandsExecutor.Execute(command);

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = WorldsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}