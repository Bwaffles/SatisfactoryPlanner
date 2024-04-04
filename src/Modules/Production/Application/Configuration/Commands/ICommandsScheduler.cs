using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}