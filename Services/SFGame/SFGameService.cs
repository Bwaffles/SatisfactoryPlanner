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
            var t = JsonConvert.DeserializeObject<List<Root>>(docFile);

            var gameData = new SFGameData
            {
                Items = t.First(_ => _.NativeClass == ClassTypeMap[ClassType.Item]).Classes.Select(_ => new Item
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
                Schematics = t.First(_ => _.NativeClass == ClassTypeMap[ClassType.Schematic]).Classes.Select(_ => new Schematic
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
                }),
                Recipes = ParseRecipes(t)
            };

            var ui = gameData.Items
                .Select(_ => new { Name = _.ClassName, Category = _.Category })
                .ToList();

            return gameData;
        }

        private IEnumerable<Recipe> ParseRecipes(List<Root> t)
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

            //"((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Plastic/Desc_Plastic.Desc_Plastic_C\"',Amount=2),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/HeavyOilResidue/Desc_HeavyOilResidue.Desc_HeavyOilResidue_C\"',Amount=1000))",
            //"(
            //(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Plastic/Desc_Plastic.Desc_Plastic_C\"',Amount=2),(ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/HeavyOilResidue/Desc_HeavyOilResidue.Desc_HeavyOilResidue_C\"',Amount=1000))",
            return t.First(_ => _.NativeClass == ClassTypeMap[ClassType.Recipe]).Classes.Select(_ => new Recipe
            {
                Name = _.ClassName,
                FullName = _.FullName,
                DisplayName = _.DisplayName,
                //Ingredients = ParseIngredients(_.Ingredients),
                Product = ParseRecipeProduct(_.Product)
            });
        }

        private List<Product> ParseRecipeProduct(string product)
        {
            var items = product.Substring(1, product.Length - 2).Split(",");
            var products = new List<Product>();

            for (int i = 0; i + 1 < items.Length; i+=2)
            {
                var item = items[i];
                var amount = items[i + 1].TrimEnd(')');
                var itemClass = item.Substring(item.IndexOf("=") + 1);
                products.Add(new Product
                {
                    ItemClass = itemClass,
                    ClassName = itemClass.Substring(itemClass.LastIndexOf('.') + 1).TrimEnd('\'').TrimEnd('"'),
                    Amount = int.Parse(amount.Substring(amount.IndexOf("=") + 1)),
                });
            }

            return products;
        }

        //private object ParseIngredients(string ingredients)
        //{
        //    /*
        //     * "mIngredients": "( (ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronPlate/Desc_IronPlate.Desc_IronPlate_C\"',Amount=3)
        //     *                  , (ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/Rubber/Desc_Rubber.Desc_Rubber_C\"',Amount=1))",
        //       "mProduct":     "((ItemClass=BlueprintGeneratedClass'\"/Game/FactoryGame/Resource/Parts/IronPlateReinforced/Desc_IronPlateReinforced.Desc_IronPlateReinforced_C\"',Amount=1))",
        //     */
        //    var t = ingredients.Split(",");
        //}

        private ItemCategory GetCategory(Class c)
        {
            if (c.BigIcon.Contains("Christmas"))
                return ItemCategory.Christmas;

            if (c.ClassName.Contains("SpaceElevatorPart"))
                return ItemCategory.ProjectAssembly;

            if (c.BigIcon.Contains("Parts"))
                return ItemCategory.Part;

            return ItemCategory.Unknown;
        }
    }
}
