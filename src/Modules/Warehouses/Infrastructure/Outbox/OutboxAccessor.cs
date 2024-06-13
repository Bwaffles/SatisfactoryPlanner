using SatisfactoryPlanner.BuildingBlocks.Application.Outbox;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Outbox
{
    public class OutboxAccessor : IOutbox
    {
        private readonly WarehousesContext _context;

        public OutboxAccessor(WarehousesContext context) => _context = context;

        public void Add(OutboxMessage message) => _context.OutboxMessages.Add(message);

        public Task Save() => Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    }
}