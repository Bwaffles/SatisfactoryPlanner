using MediatR;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Configuration
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : QueryBase<TResult>;
}
