using MediatR;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult> { }
}