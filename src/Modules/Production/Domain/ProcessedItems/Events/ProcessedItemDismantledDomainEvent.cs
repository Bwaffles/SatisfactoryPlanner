using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems.Events
{
    public class ProcessedItemDismantledDomainEvent(ProcessedItemId processedItemId) : DomainEventBase
    {
        public ProcessedItemId ProcessedItemId { get; } = processedItemId;
    }
}