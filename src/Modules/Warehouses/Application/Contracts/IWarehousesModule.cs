namespace SatisfactoryPlanner.Modules.Warehouses.Application.Contracts
{
    public interface IWarehousesModule
    {
        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}