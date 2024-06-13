using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.InternalCommands
{
    internal record ProcessInternalCommandsCommand : CommandBase, IRecurringCommand { }
}