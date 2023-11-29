using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Events;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines
{
    public class ProductionLine : Entity, IAggregateRoot
    {
        private ProductionLineName _name;

        public ProductionLineId Id { get; }

        private ProductionLine(ProductionLineName name)
        {
            Id = new ProductionLineId(Guid.NewGuid());
            _name = name;

            AddDomainEvent(new ProductionLineSetUpDomainEvent(Id, _name));
        }

        public static ProductionLine SetUp(ProductionLineName name) => new(name);

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