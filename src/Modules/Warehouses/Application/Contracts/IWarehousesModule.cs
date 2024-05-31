namespace SatisfactoryPlanner.Modules.Warehouses.Application.Contracts
{
    public interface IWarehousesModule
    {
        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}