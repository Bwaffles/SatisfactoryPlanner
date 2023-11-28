using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ProcessedItems.Rules
{
    public class ItemMustBeIngredientOfRecipeRule(Item item, Recipe recipe) : IBusinessRule
    {
        public string Message => "Item must be an ingredient of the recipe.";

        public bool IsBroken() => !recipe.HasIngredient(item);
    }
}