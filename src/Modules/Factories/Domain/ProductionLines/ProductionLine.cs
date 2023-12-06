using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public class ProductionLine : Entity, IAggregateRoot
    {
        private readonly WorldId _worldId;
        private ProductionLineName _name;

        public ProductionLineId Id { get; }

        private ProductionLine(WorldId worldId, ProductionLineName name)
        {
            Id = new ProductionLineId(Guid.NewGuid());
            _worldId = worldId;
            _name = name;

            AddDomainEvent(new ProductionLineSetUpDomainEvent(Id, _worldId, _name));
        }

        public static ProductionLine SetUp(WorldId worldId, ProductionLineName name) => new(worldId, name);

        public void Rename(ProductionLineName name)
        {
            if (_name == name)
                return;

            _name = name;

            AddDomainEvent(new ProductionLineRenamedDomainEvent(Id, _name));
        }

        public ProcessedItem ProcessItem(ItemId item, Recipe recipe) => ProcessedItem.CreateNew(Id, item, recipe);
    }
}