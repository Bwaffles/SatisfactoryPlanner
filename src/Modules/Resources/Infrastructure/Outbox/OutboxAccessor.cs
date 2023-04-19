using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly ResourcesContext _context;

        public OutboxAccessor(ResourcesContext context) => _context = context;

        public void Add(OutboxMessage message) => _context.OutboxMessages.Add(message);

        public Task Save() =>
            Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    }
}