using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Configuration
{
    public abstract record InternalCommandBase : ICommand
    {
        protected InternalCommandBase(Guid id) => Id = id;

        public Guid Id { get; }
    }

    public abstract record InternalCommandBase<TResult> : ICommand<TResult>
    {
        protected InternalCommandBase() => Id = Guid.NewGuid();

        protected InternalCommandBase(Guid id) => Id = id;

        public Guid Id { get; }
    }
}