using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Events
{
    public class ProductionLineRenamedDomainEvent
        (ProductionLineId productionLineId, ProductionLineName name) : DomainEventBase
    {
        public ProductionLineId ProductionLineId { get; } = productionLineId;

        public ProductionLineName Name { get; } = name;
    }
}