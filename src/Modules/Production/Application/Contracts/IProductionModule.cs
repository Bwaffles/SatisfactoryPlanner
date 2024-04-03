using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.Contracts
{
    public interface IProductionModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}