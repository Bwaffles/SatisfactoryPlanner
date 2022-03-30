using MediatR;

namespace SatisfactoryPlanner.Modules.Factories.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
