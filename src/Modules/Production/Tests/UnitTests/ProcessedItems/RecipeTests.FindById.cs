using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;

namespace SatisfactoryPlanner.Modules.Production.UnitTests.ProcessedItems
{
    [TestFixture]
    public partial class RecipeTests
    {
        [TestFixture]
        public class FindByIdTests
        {
            [Test]
            public void WhenRecipeExists_ShouldReturnFoundRecipe()
            {
                Recipe.FindById("PureIronIngot").Should().Be(Recipe.PureIronIngot);
            }

            [Test]
            public void WhenRecipeDoesNotExists_ShouldReturnNull()
            {
                Recipe.FindById("RecipeThatDoesNotExist").Should().BeNull();
            }
        }
    }
}
