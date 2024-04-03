using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Contracts
{
    public interface IFactoriesModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}