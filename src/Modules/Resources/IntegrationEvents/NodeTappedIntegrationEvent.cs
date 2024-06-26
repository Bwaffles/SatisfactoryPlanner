using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationEvents
{
    public class NodeTappedIntegrationEvent(Guid id, DateTime occurredOn, Guid worldId, Guid nodeId, string nodeName, string itemId) : IntegrationEvent(id, occurredOn)
    {
        public Guid WorldId { get; } = worldId;

        public Guid NodeId { get; } = nodeId;

        public string NodeName { get; } = nodeName;

        public string ItemId { get; } = itemId;
    }
}
