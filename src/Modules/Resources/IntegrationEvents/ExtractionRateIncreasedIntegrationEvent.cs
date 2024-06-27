using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationEvents;

public class ExtractionRateIncreasedIntegrationEvent(Guid id, DateTime occurredOn, Guid worldId, Guid nodeId, string itemId, decimal rate) : IntegrationEvent(id, occurredOn)
{
    public Guid WorldId { get; } = worldId;
    public Guid NodeId { get; } = nodeId;
    public string ItemId { get; } = itemId;
    public decimal Rate { get; } = rate;
}
