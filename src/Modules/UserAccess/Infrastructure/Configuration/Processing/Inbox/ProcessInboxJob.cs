using Quartz;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox
{
    [DisallowConcurrentExecution]
    public class ProcessInboxJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await CommandsExecutor.Execute(new ProcessInboxCommand());
        }
    }
}