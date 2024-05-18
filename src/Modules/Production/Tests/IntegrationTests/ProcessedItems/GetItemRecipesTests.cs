using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess;
using SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.ProcessedItems
{
    [TestFixture]
    public class GetItemRecipesTests : IntegrationTest
    {
        // Happy path tests
        [Test]
        public async Task CanGetRecipe()
        {
            // Main test to verify all properties are set as expected

            var itemRecipes = await ProductionModule.ExecuteQueryAsync(new GetItemRecipesQuery("IronIngot"));

            AssertAll(() =>
            {
                var productRecipes = itemRecipes.ProductRecipes;
                productRecipes.Should().HaveCount(3);
                productRecipes.Should().OnlyHaveUniqueItems();
                productRecipes.Should().ContainEquivalentOf(new RecipeDto
                {
                    Id = "IronIngot",
                    Name = "Iron Ingot",
                    Type = "Standard",
                    Ingredients = [
                        new IngredientDto
                        {
                            ItemId = "IronOre",
                            ItemName = "Iron Ore",
                            Amount = new AmountDto {
                                AmountPerCycle = 1,
                                AmountPerMinute = 30
                            }
                        }
                    ],
                    Products = [
                        new ProductDto {
                            ItemId = "IronIngot",
                            ItemName = "Iron Ingot",
                            Amount = new AmountDto {
                                AmountPerCycle = 1,
                                AmountPerMinute = 30
                            }
                        }
                    ]
                });
                productRecipes.Should().ContainEquivalentOf(new RecipeDto
                {
                    Id = "PureIronIngot",
                    Name = "Pure Iron Ingot",
                    Type = "Alternate",
                    Ingredients = [
                        new IngredientDto
                        {
                            ItemId = "IronOre",
                            ItemName = "Iron Ore",
                            Amount = new AmountDto {
                                AmountPerCycle = 7,
                                AmountPerMinute = 35
                            }
                        },
                        new IngredientDto
                        {
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
                    ]
                });

                var ingredientRecipes = itemRecipes.IngredientRecipes;
                ingredientRecipes.Should().HaveCount(6);
                ingredientRecipes.Should().OnlyHaveUniqueItems();
                ingredientRecipes.Should().ContainEquivalentOf(new RecipeDto
                {
                    Id = "IronPlate",
                    Name = "Iron Plate",
                    Type = "Standard",
                    Ingredients = [new IngredientDto
                    {
                        ItemId = "IronIngot",
                        ItemName = "Iron Ingot",
                        Amount = new AmountDto {
                            AmountPerCycle = 3,
                            AmountPerMinute = 30
                        }
                    }],
                    Products = [
                        new ProductDto {
                            ItemId = "IronPlate",
                            ItemName = "Iron Plate",
                            Amount = new AmountDto {
                                AmountPerCycle = 2,
                                AmountPerMinute = 20
                            }
                        }
                    ]
                });
            });
        }

        [Test]
        public async Task CanGetRecipesForItemOnlyUsedAsAnIngredient()
        {
            var itemRecipes = await ProductionModule.ExecuteQueryAsync(new GetItemRecipesQuery("BluePowerSlug"));

            AssertAll(() =>
            {
                itemRecipes.ProductRecipes.Should().BeEmpty("because you can only get slugs by picking them up");
                itemRecipes.IngredientRecipes.Should().NotBeEmpty("because you can use power slugs to produce power shards");
            });
        }

        [Test]
        public async Task CanGetRecipesForItemNeverUsedAsAnIngredient()
        {
            var itemRecipes = await ProductionModule.ExecuteQueryAsync(new GetItemRecipesQuery("PlutoniumFuelRod"));

            AssertAll(() =>
            {
                itemRecipes.ProductRecipes.Should().NotBeEmpty("because you can produce plutonium fuel rods");
                itemRecipes.IngredientRecipes.Should().BeEmpty("because plutonium fuel rods are the end of their production chain");
            });
        }

        [Test]
        public async Task WhenItemNotUsedInAnyRecipes_ResultsShouldBeEmpty()
        {
            // Can't automate production of Xeno-Zapper so it won't have any recipes returned
            var itemRecipes = await ProductionModule.ExecuteQueryAsync(new GetItemRecipesQuery("XenoZapper"));

            AssertAll(() =>
            {
                itemRecipes.ProductRecipes.Should().BeEmpty();
                itemRecipes.IngredientRecipes.Should().BeEmpty();
            });
        }

        [TestCase("ItemThatDoesNotExist")]
        [TestCase("")]
        [TestCase(null)]
        public void WhenItemInvalid_ThrowsInvalidCommandException(string itemId)
        {
            AssertInvalidCommand(async () =>
            {
                await ProductionModule.ExecuteQueryAsync(new GetItemRecipesQuery(itemId));
            });
        }
    }
}
