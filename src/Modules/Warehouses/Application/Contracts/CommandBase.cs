namespace SatisfactoryPlanner.Modules.Warehouses.Application.Contracts
{
    public abstract record CommandBase : ICommand
    {
        protected CommandBase() => Id = Guid.NewGuid();

        protected CommandBase(Guid id) => Id = id;

        public Guid Id { get; }
    }

    public abstract record CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase() => Id = Guid.NewGuid();

        protected CommandBase(Guid id) => Id = id;

        public Guid Id { get; }
    }
}