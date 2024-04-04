using MediatR;

namespace SatisfactoryPlanner.Modules.Production.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}