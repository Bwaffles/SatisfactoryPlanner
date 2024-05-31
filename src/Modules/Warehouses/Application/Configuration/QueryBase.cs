using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Configuration
{
    public abstract record QueryBase<TResult> : IQuery<TResult>
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
