using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Infrastructure.Configuration.Processing
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default, Guid? internalCommandId = null);
    }
}
