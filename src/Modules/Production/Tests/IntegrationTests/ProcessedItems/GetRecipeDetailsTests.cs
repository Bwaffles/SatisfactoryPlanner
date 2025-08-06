using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems;
using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetRecipeDetails;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProcessedItems
{
    [TestFixture]
    public class GetRecipeDetailsTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task CanGetRecipeDetails()
        {
            var recipeDetails = await ProductionModule.ExecuteQueryAsync(new GetRecipeDetailsQuery("PureIronIngot"));

            AssertAll(() =>
            {
                recipeDetails.Should().BeEquivalentTo(new RecipeDetailsDto
                {
                    Id = "PureIronIngot",
                    Name = "Pure Iron Ingot",
                    Type = "Alternate",
                    Ingredients = [
                            new IngredientDto {
                                ItemId = "IronOre",
                                ItemName = "Iron Ore",
                                Amount = new AmountDto {
                                    AmountPerCycle = 7,
                                    AmountPerMinute = 35
                                }
                            },
                            new IngredientDto {
                                ItemId = "Water",
                                ItemName = "Water",
                                Amount = new AmountDto {
                                    AmountPerCycle = 4,
                                    AmountPerMinute = 20
                                }
                            }
                    ],
                    Products = [
                        new ProductDto {
                            ItemId = "IronIngot",
                            ItemName = "Iron Ingot",
                            Amount = new AmountDto {
                                AmountPerCycle = 13,
                                AmountPerMinute = 65
                            }
                        }
                    ],
                    ProducedIn = [
                        new BuildingDto {
                            Id = "Refinery",
                            Name = "Refinery",
                            ProductionMethod = "Automatic"
                        }
                    ]
                });
            });
        }

        [Test]
        public async Task CanGetCorrectTypeForStandardRecipe()
        {
            var recipeDetails = await ProductionModule.ExecuteQueryAsync(new GetRecipeDetailsQuery("IronIngot"));
            recipeDetails.Type.Should().Be("Standard");
        }

        [Test]
        public async Task CanOnlyGetAutomatedProductionBuildings()
        {
            var recipeDetails = await ProductionModule.ExecuteQueryAsync(new GetRecipeDetailsQuery("IronIngot"));
            recipeDetails.ProducedIn.Should().OnlyContain(building => building.ProductionMethod == "Automatic", "because we're processing items which implies we're automating the work. It doesn't matter if we can produce the item in a craft bench too.");
        }

        [Test]
        public async Task CanGetBuildingWithSpacesInName()
        { // I want this test to ensure that name and id are mapped right, since most buildings the id and name are the same
            var recipeDetails = await ProductionModule.ExecuteQueryAsync(new GetRecipeDetailsQuery("FicsoniumFuelRod"));

            var building = recipeDetails.ProducedIn.First();
            building.Id.Should().Be("QuantumEncoder");
            building.Name.Should().Be("Quantum Encoder");
        }

        [TestCase("RecipeThatDoesNotExist")]
        [TestCase("")]
        [TestCase(null)]
        public void WhenRecipeInvalid_ThrowsInvalidCommandException(string recipeId)
        {
            AssertInvalidCommand(async () =>
            {
                await ProductionModule.ExecuteQueryAsync(new GetRecipeDetailsQuery(recipeId));
            });
        }
    }
}
