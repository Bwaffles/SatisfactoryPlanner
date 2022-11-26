using MediatR;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}