using MediatR;

namespace SatisfactoryPlanner.Modules.Resources.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}