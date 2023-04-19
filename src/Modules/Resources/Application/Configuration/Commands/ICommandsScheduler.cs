using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}