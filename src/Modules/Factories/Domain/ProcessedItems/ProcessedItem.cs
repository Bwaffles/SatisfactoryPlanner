using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Rules;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class ProcessedItem : Entity, IAggregateRoot
    {
        private readonly ItemId _itemId;
        private readonly ProductionLineId _productionLineId;
        private readonly Recipe _recipe;

        public ProcessedItemId Id { get; }

        private ProcessedItem(ProductionLineId productionLineId, ItemId itemId, Recipe recipe)
        {
            CheckRule(new ItemMustBeIngredientOfRecipeRule(itemId, recipe));

            Id = new ProcessedItemId(Guid.NewGuid());
            _productionLineId = productionLineId;
            _itemId = itemId;
            _recipe = recipe;

            AddDomainEvent(new ItemProcessedDomainEvent(Id, _productionLineId, _itemId, _recipe));
        }

        internal static ProcessedItem CreateNew(ProductionLineId productionLineId, ItemId item, Recipe recipe) =>
            new(productionLineId, item, recipe);

        public void Dismantle()
        {
            AddDomainEvent(new ProcessedItemDismantledDomainEvent(Id));
        }
    }
}