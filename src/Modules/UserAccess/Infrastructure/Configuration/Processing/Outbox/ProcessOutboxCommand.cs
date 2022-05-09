using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase, IRecurringCommand
    {
    }
}