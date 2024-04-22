using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Events;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines.Rules;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProductionLines
{
    public class ProductionLine : Entity, IAggregateRoot
    {
        private readonly WorldId _worldId;
        private ProductionLineName _name;

        public ProductionLineId Id { get; }

        // ReSharper disable once UnusedMember.Local
        private ProductionLine()
        { /* Only for EF. */
            _worldId = default!;
            _name = default!;
            Id = default!;
        }

        private ProductionLine(WorldId worldId, ProductionLineName name, IProductionLineCounter productionLineCounter)
        {
            CheckRule(new ProductionLineNameMustBeUniqueRule(worldId, name, productionLineCounter));

            Id = new ProductionLineId(Guid.NewGuid());
            _worldId = worldId;
            _name = name;

            AddDomainEvent(new ProductionLineSetUpDomainEvent(Id, _worldId, _name));
        }

        public static ProductionLine SetUp(WorldId worldId, ProductionLineName name,
            IProductionLineCounter productionLineCounter) => new(worldId, name, productionLineCounter);

        public void Rename(ProductionLineName name, IProductionLineCounter productionLineCounter)
        {
            var hasSameCasing = _name.HasSameCasingAs(name);
            if (_name == name && hasSameCasing)
                return;

            if (_name != name)
                CheckRule(new ProductionLineNameMustBeUniqueRule(_worldId, name, productionLineCounter));

            // If the name is different or uses different casing then let it be updated
            _name = name;

            AddDomainEvent(new ProductionLineRenamedDomainEvent(Id, _name));
        }

        public ProcessedItem ProcessItem(ItemId item, Recipe recipe) => ProcessedItem.CreateNew(Id, item, recipe);
    }
}