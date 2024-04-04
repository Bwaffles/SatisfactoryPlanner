using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems.Rules
{
    public class ItemMustBeIngredientOfRecipeRule(ItemId itemId, Recipe recipe) : IBusinessRule
    {
        public string Message => "Item must be an ingredient of the recipe.";

        public bool IsBroken() => !recipe.HasIngredient(itemId);
    }
}