using Autofac;
using MediatR;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure
{
    public class WarehousesModule : IWarehousesModule
    {
        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using var scope = CompositionRoot.BeginLifetimeScope();

            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(query);
        }
    }
}
