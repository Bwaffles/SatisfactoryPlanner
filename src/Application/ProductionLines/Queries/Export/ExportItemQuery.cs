using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application.ProductionLines.Queries.Export
{
    public class ExportItemQuery
    {
        public IEnumerable<ExportItemModel> Execute()
        {
            var allLines = GetDummyProductionLine();

            foreach (var line in allLines)
            {
                foreach (var pod in line.Pods)
                {
                    foreach (var input in pod.Item.Inputs)
                    {
                        yield return new ExportItemModel
                        {
                            Id = input.Id,
                            Name = $"{line.Name} - Pod {pod.Number}",
                            ItemId = input.Ingredient.Item.Id,
                            Amount = input.Amount
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Very obviously this can be a reference table of all the items in the game.
        /// </summary>
        public static class Items
        {
            public static Item CircuitBoard => new Item
            {
                Id = "Desc_CircuitBoard_C",
                Name = "Circuit Board"
            };

            public static Item Plastic => new Item
            {
                Id = "Desc_Plastic_C",
                Name = "Plastic"
            };

            public static Item Quickwire => new Item
            {
                Id = "Desc_HighSpeedWire_C",
                Name = "Quickwire"
            };
        }

        /// <summary>
        /// Should be from reference tables and does not change.
        /// </summary>
        public static class Recipes
        {
            public static Recipe CateriumCircuitBoard => new Recipe
            {
                Id = "Recipe_Alternate_CircuitBoard_2_C",
                Name = "Alternate: Caterium Circuit Board",
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Item = Items.CircuitBoard,
                        ItemsPerMinute = 8.75m
                    }
                },
                Ingredients = new List<Ingredient>
                {
                    new Ingredient
                    {
                        Id = 1,
                        Item = Items.Plastic,
                        ItemsPerMinute = 12.5m
                    },
                    new Ingredient
                    {
                        Id = 2,
                        Item = Items.Quickwire,
                        ItemsPerMinute = 37.5m
                    }
                }
            };
        }

        /// <summary>
        /// Get hardcoded data for testing. Needs to come from the database.
        /// </summary>
        private List<ProductionLine> GetDummyProductionLine()
        {
            var circuitBoardProduct = Recipes.CateriumCircuitBoard.Products.First(p => p.Id == 1);
            var plasticIngredient = Recipes.CateriumCircuitBoard.Ingredients.First(p => p.Id == 1);
            var quickwireIngredient = Recipes.CateriumCircuitBoard.Ingredients.First(p => p.Id == 2);

            return new List<ProductionLine>
            {
                new ProductionLine {
                    Name = "Circuit Board West",
                    Pods = new List<Pod>
                    {
                        new Pod {
                            Number = 1,
                            Item = new PodItem
                            {
                                Recipe = Recipes.CateriumCircuitBoard,
                                Outputs = new List<Output>()
                                {
                                    new Output
                                    {
                                        Id = 1,
                                        Product = circuitBoardProduct,
                                        Amount = 336
                                    }
                                },
                                Inputs = new List<Input>()
                                {
                                    new Input
                                    {
                                        Id = 1,
                                        Ingredient = plasticIngredient,
                                        Amount = 480
                                    },
                                    new Input
                                    {
                                        Id = 2,
                                        Ingredient = quickwireIngredient,
                                        Amount = 1440
                                    }
                                }
                            }
                        },
                        new Pod {
                            Number = 2,
                            Item = new PodItem
                            {
                                Recipe = Recipes.CateriumCircuitBoard,
                                Outputs = new List<Output>()
                                {
                                    new Output
                                    {
                                        Id = 2,
                                        Product = circuitBoardProduct,
                                        Amount = 336
                                    }
                                },
                                Inputs = new List<Input>()
                                {
                                    new Input
                                    {
                                        Id = 3,
                                        Ingredient =plasticIngredient,
                                        Amount = 480
                                    },
                                    new Input
                                    {
                                        Id = 4,
                                        Ingredient = quickwireIngredient,
                                        Amount = 1440
                                    }
                                }
                            }
                        },
                        new Pod {
                            Number = 3,
                            Item = new PodItem
                            {
                                Recipe = Recipes.CateriumCircuitBoard,
                                Outputs = new List<Output>()
                                {
                                    new Output
                                    {
                                        Id = 3,
                                        Product = circuitBoardProduct,
                                        Amount = 336
                                    }
                                },
                                Inputs = new List<Input>()
                                {
                                    new Input
                                    {
                                        Id = 5,
                                        Ingredient = plasticIngredient,
                                        Amount = 480
                                    },
                                    new Input
                                    {
                                        Id = 6,
                                        Ingredient = quickwireIngredient,
                                        Amount = 1440
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
