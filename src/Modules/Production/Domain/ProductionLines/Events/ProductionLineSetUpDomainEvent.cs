using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Events
{
    public class ProductionLineSetUpDomainEvent(
        ProductionLineId productionLineId,
        WorldId worldId,
        ProductionLineName name) : DomainEventBase
    {
        public ProductionLineName Name { get; } = name;

        public ProductionLineId ProductionLineId { get; } = productionLineId;

        public WorldId WorldId { get; } = worldId;
    }
}