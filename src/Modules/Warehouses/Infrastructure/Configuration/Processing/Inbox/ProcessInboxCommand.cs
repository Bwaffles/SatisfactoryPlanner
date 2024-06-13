using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration.Processing.Inbox
{
    public record ProcessInboxCommand : CommandBase, IRecurringCommand { }
}