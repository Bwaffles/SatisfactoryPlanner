using Autofac;
using MediatR;
using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration.Processing;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure
{
    public class PioneersModule : IPioneersModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command) =>
            await CommandsExecutor.Execute(command);

        public async Task ExecuteCommandAsync(ICommand command) => await CommandsExecutor.Execute(command);

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = PioneersCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}