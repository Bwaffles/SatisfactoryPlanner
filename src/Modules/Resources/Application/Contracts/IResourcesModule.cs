using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Contracts
{
    public interface IResourcesModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}
