using MediatR;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> { }
}