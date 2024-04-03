using Autofac;
using MediatR;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration.Processing;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure
{
    public class ProductionModule : IProductionModule
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
            using (var scope = ProductionCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}