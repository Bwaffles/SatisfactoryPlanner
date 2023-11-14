using System.Threading.Tasks;

namespace SatisfactoryPlanner.BuildingBlocks.Application.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);

        Task Save();
    }
}