using MediatR;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    { }
}