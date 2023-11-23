using MediatR;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}