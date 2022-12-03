using SatisfactoryPlanner.Modules.Pioneers.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}