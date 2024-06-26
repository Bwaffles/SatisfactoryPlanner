using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.Configuration
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);
    }
}