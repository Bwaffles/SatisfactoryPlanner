using Newtonsoft.Json;
using Services.SFGame.Models.DocExtraction;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Services.SFGame
{

    public class SFGameService
    {
        public Dictionary<ClassType, string> ClassTypeMap = new()
        {
            { ClassType.Item, "Class'/Script/FactoryGame.FGItemDescriptor'" },
            { ClassType.Schematic, "Class'/Script/FactoryGame.FGSchematic'" },
            { ClassType.Recipe, "Class'/Script/FactoryGame.FGRecipe'" }
        };

        public SFGameData GetGameData()
        {
            var docFile = File.ReadAllText("E:/Projects/SatisfactoryPlanner/SatisfactoryPlanner/Services/SFGame/Docs.json");
            var rootData = JsonConvert.DeserializeObject<List<Root>>(docFile);

            var gameData = new SFGameData
            {
                Items = rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Item]).Classes.Select(_ => new Item
                {
                    AbbreviatedDisplayName = _.AbbreviatedDisplayName,
                    BigIcon = _.BigIcon,
                    CanBeDiscarded = _.CanBeDiscarded.Value,
                    Category = GetCategory(_),
                    ClassName = _.ClassName,
                    Description = _.Description,
                    DisplayName = _.DisplayName,
                    EnergyValue = _.EnergyValue.Value,
                    FluidColor = _.FluidColor,
                    Form = _.Form.Value,
                    GasColor = _.GasColor,
                    RadioactiveDecay = _.RadioactiveDecay.Value,
                    RememberPickUp = _.RememberPickUp.Value,
                    ResourceSinkPoints = _.ResourceSinkPoints.Value,
                    SmallIcon = _.SmallIcon,
                    StackSize = _.StackSize.Value,
                }),
                Schematics = rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Schematic]).Classes.Select(_ => new Schematic
                {
                    ClassName = _.ClassName,
                    FullName = _.FullName,
                    Type = _.Type.Value,
                    DisplayName = _.DisplayName,
                    Description = _.Description,
                    SubCategories = _.SubCategories,
                    MenuPriority = _.MenuPriority.Value,
                    TechTier = _.TechTier.Value,
                    Cost = _.Cost,
                    TimeToComplete = _.TimeToComplete.Value,
                    RelevantShopSchematics = _.RelevantShopSchematics,
                    RelevantEvents = _.RelevantEvents,
                    SchematicIcon = _.SchematicIcon,
                    SmallSchematicIcon = _.SmallSchematicIcon,
                    //SchematicDependencies = _.SchematicDependencies
                    //Unlocks = _.Unlocks,
                    IncludeInBuilds = _.IncludeInBuilds
                })
            };

            gameData.Recipes = ParseRecipes(rootData, gameData.Items).ToList();

            var ui = gameData.Items
                .Select(_ => new { Name = _.ClassName, Category = _.Category })
                .GroupBy(_ => _.Category)
                .ToList();

            return gameData;
        }

        private IEnumerable<Recipe> ParseRecipes(List<Root> data, IEnumerable<Item> existingItems)
        {
            /*
            "ClassName": "Recipe_CircuitBoard_C",
            "FullName": "BlueprintGeneratedClass /Game/FactoryGame/Recipes/Assembler/Recipe_CircuitBoard.Recipe_CircuitBoard_C",
            "mDisplayName": "Circuit Board",
            "mIngredients": "(
                                (ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/CopperSheet/Desc_CopperSheet.Desc_CopperSheet_C\"',Amount=2),
                                (ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Plastic/Desc_Plastic.Desc_Plastic_C\"',Amount=4)
                            )",
            "mProduct": "(
                            (
                                ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/CircuitBoard/Desc_CircuitBoard.Desc_CircuitBoard_C\"',
                                Amount=1
                            )
                        )",
            "mManufacturingMenuPriority": "5.000000",
            "mManufactoringDuration": "8.000000",
            "mManualManufacturingMultiplier": "1.500000",
            "mProducedIn": "(/Game/FactoryGame/Buildable/Factory/AssemblerMk1/Build_AssemblerMk1.Build_AssemblerMk1_C,/Game/FactoryGame/Buildable/-Shared/WorkBench/BP_WorkBenchComponent.BP_WorkBenchComponent_C,/Script/FactoryGame.FGBuildableAutomatedWorkBench)",
            "mRelevantEvents": "",
            "mVariablePowerConsumptionConstant": "0.000000",
            "mVariablePowerConsumptionFactor": "1.000000"
             */
            foreach (var recipeRawData in data.First(_ => _.NativeClass == ClassTypeMap[ClassType.Recipe]).Classes)
            {
                var products = ParseItems(recipeRawData.Product, existingItems);
                if (!products.Any())
                    continue;

                var recipe = new Recipe
                {
                    ClassName = recipeRawData.ClassName,
                    FullName = recipeRawData.FullName,
                    DisplayName = recipeRawData.DisplayName,
                    Ingredients = ParseItems(recipeRawData.Ingredients, existingItems),
                    Products = products,
                    ManufacturingDuration = recipeRawData.ManufactoringDuration
                };
                yield return recipe;
            }
        }

        private List<Product> ParseItems(string product, IEnumerable<Item> existingItems)
        {
            var items = product.Substring(1, product.Length - 2).Split(",");
            var products = new List<Product>();

            for (int i = 0; i + 1 < items.Length; i += 2)
            {
                var item = items[i];
                var amount = items[i + 1].TrimEnd(')');
                var itemClass = item.Substring(item.IndexOf("=") + 1);
                var className = itemClass.Substring(itemClass.LastIndexOf('.') + 1).TrimEnd('\'').TrimEnd('"');

                var producedItem = existingItems.FirstOrDefault(i => i.ClassName == className);
                if (producedItem == null)
                    continue;

                products.Add(new Product
                {
                    Item = producedItem,
                    ItemClass = itemClass,
                    Amount = int.Parse(amount.Substring(amount.IndexOf("=") + 1)),
                });
            }

            return products;
        }

        private ItemCategory GetCategory(Class c)
        {
            if (c.BigIcon.Contains("Christmas"))
                return ItemCategory.Ficsmas;

            if (c.ClassName.Contains("SpaceElevatorPart"))
                return ItemCategory.ProjectAssembly;

            if (c.BigIcon.Contains("Parts") || c.BigIcon.Contains("Resource/RawResources"))
            {
                if (c.BigIcon.Contains("Ingot"))
                    return ItemCategory.Ingot;
                else if (c.StackSize == StackSize.SS_FLUID)
                    return ItemCategory.Fluid;
                else
                    return ItemCategory.Part;
            }

            if (c.ClassName.Contains("Crystal"))
                return ItemCategory.PowerShard;

            throw new System.Exception("Can't get category for this item");
        }
    }
}
