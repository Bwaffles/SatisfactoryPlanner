using SatisfactoryPlanner.UserAccess.Application.Contracts;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.UserAccess.Application.Configuration.Commands
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync(ICommand command);

        Task EnqueueAsync<T>(ICommand<T> command);
    }
}