using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Contracts
{
    public interface IPioneersModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);
    }
}