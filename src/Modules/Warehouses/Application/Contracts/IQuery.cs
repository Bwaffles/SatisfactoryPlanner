using MediatR;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult> { }
}