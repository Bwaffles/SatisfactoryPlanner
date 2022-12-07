using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}