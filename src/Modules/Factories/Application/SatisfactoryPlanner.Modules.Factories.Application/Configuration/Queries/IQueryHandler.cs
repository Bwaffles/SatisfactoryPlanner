using MediatR;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
