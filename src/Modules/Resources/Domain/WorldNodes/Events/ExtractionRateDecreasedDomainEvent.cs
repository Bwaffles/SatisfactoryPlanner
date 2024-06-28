using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes.Events;

/// <summary>
///     An event triggered when the extraction rate of a world node has been decreased
///     from its current extraction rate.
/// </summary>
public class ExtractionRateDecreasedDomainEvent(WorldNodeId worldNodeId, decimal extractionRate) : DomainEventBase
{
    public decimal ExtractionRate { get; } = extractionRate;
    public WorldNodeId WorldNodeId { get; } = worldNodeId;
}