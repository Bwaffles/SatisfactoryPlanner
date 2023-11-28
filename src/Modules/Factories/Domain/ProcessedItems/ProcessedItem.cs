using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Events;
using SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Rules;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems
{
    public class ProcessedItem : Entity, IAggregateRoot
    {
        private readonly Item _item;
        private readonly ProductionLineId _productionLineId;
        private readonly Recipe _recipe;

        public ProcessedItemId Id { get; }

        private ProcessedItem(ProductionLineId productionLineId, Item item, Recipe recipe)
        {
            CheckRule(new ItemMustBeIngredientOfRecipeRule(item, recipe));

            Id = new ProcessedItemId(Guid.NewGuid());
            _productionLineId = productionLineId;
            _item = item;
            _recipe = recipe;

            AddDomainEvent(new ItemProcessedDomainEvent(Id, _productionLineId, _item, _recipe));
        }

        internal static ProcessedItem CreateNew(ProductionLineId productionLineId, Item item, Recipe recipe) =>
            new(productionLineId, item, recipe);
    }
}