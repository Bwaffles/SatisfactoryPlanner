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
            { ClassType.Recipe, "Class'/Script/FactoryGame.FGRecipe'" },
            { ClassType.Resource, "Class'/Script/FactoryGame.FGResourceDescriptor'" },
            { ClassType.Consumable, "Class'/Script/FactoryGame.FGConsumableDescriptor'" },
            { ClassType.Biomass, "Class'/Script/FactoryGame.FGItemDescriptorBiomass'" },
            { ClassType.NuclearFuel, "Class'/Script/FactoryGame.FGItemDescriptorNuclearFuel'" },
            { ClassType.Equipment, "Class'/Script/FactoryGame.FGEquipmentDescriptor'" },
            { ClassType.Projectile, "Class'/Script/FactoryGame.FGItemDescAmmoTypeProjectile'" }
        };

        public SFGameData GetGameData()
        {
            var docFile = File.ReadAllText("E:/Projects/SatisfactoryPlanner/src/Services/SFGame/Docs.json");
            var rootData = JsonConvert.DeserializeObject<List<Root>>(docFile);

            var gameData = new SFGameData();

            AddItems(rootData, gameData);
            AddResourceItems(rootData, gameData);
            AddConsumableItems(rootData, gameData);
            AddBiomassItems(rootData, gameData);
            AddNuclearFuelItems(rootData, gameData);
            AddEquipmentItems(rootData, gameData);
            //AddProjectileItems(rootData, gameData);

            AddSchematics(rootData, gameData);

            gameData.Recipes = ParseRecipes(rootData, gameData.Items).ToList();

            //var ui = gameData.Items
            //    .Select(_ => new { Name = _.ClassName, Category = _.Category })
            //    .GroupBy(_ => _.Category)
            //    .ToList();

            //var missingItems = gameData.Recipes
            //    .Where(_ =>
            //        _.Ingredients.Any(ingredient => !gameData.Items.Select(_ => _.ClassName).Contains(ingredient.ItemClass)) ||
            //        _.Products.Any(product => !gameData.Items.Select(_ => _.ClassName).Contains(product.ItemClass))
            //        )
            //    .ToList();

            return gameData;
        }

        private void AddProjectileItems(List<Root> rootData, SFGameData gameData)
        {
            throw new System.NotImplementedException();
        }

        private void AddEquipmentItems(List<Root> rootData, SFGameData gameData)
        {
            /*
                XX"mAbbreviatedDisplayName": "",
                XX"mBuildMenuPriority": "0.000000"
                "mCanBeDiscarded": "True",
                "ClassName": "Desc_Chainsaw_C",
                "mDescription": "Slot: Hands\r\nFuel: Biofuel\r\n\r\nUsed to clear an area of flora that is too difficult to remove by hand.",
                "mDisplayName": "Chainsaw",
                "mEnergyValue": "0.000000",
                XX"mFluidColor": "(B=0,G=0,R=0,A=0)",
                "mForm": "RF_SOLID",
                XX"mGasColor": "(B=0,G=0,R=0,A=0)",
                XX"mMenuPriority": "0.000000",
                XX"mPersistentBigIcon": "Texture2D /Game/FactoryGame/Equipment/Chainsaw/UI/IconDesc_Chainsaw_256.IconDesc_Chainsaw_256",
                "mRadioactiveDecay": "0.000000",
                "mRememberPickUp": "False",
                "mResourceSinkPoints": "2760",
                XX"mSmallIcon": "Texture2D /Game/FactoryGame/Equipment/Chainsaw/UI/IconDesc_Chainsaw_64.IconDesc_Chainsaw_64",
                "mStackSize": "SS_ONE",
                XX"mSubCategories": "",
            */

            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Equipment]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = GetCategory(_),
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints ?? 0,
                StackSize = _.StackSize.Value,
            }));

            static ItemType GetCategory(Class _)
            {
                if (_.ClassName == "BP_EquipmentDescriptorCandyCane_C")
                    return ItemType.Ficsmas;

                return ItemType.Equipment;
            }
        }

        private void AddNuclearFuelItems(List<Root> rootData, SFGameData gameData)
        {
            /*
                XX"mAbbreviatedDisplayName": "",
                XX"mAmountOfWaste": "10", -> not storing this--can be retrieve from the nuclear power plant and isn't relevent as an item
                "mCanBeDiscarded": "True",
                "ClassName": "Desc_PlutoniumFuelRod_C",
                "mDescription": "Used as fuel for the Nuclear Power Plant.\r\n\r\nCaution: Produces radioactive Plutonium Waste when consumed.\r\nCaution: HIGHLY Radioactive.",
                "mDisplayName": "Plutonium Fuel Rod",
                "mEnergyValue": "1500000.000000",
                XX"mFluidColor": "(B=0,G=0,R=0,A=0)",
                "mForm": "RF_SOLID",
                XX"mGasColor": "(B=0,G=0,R=0,A=0)",
                XX"mPersistentBigIcon": "Texture2D /Game/FactoryGame/Resource/Parts/PlutoniumFuelRods/UI/IconDesc_PlutoniumFuelRod_256.IconDesc_PlutoniumFuelRod_256",
                "mRadioactiveDecay": "250.000000",
                "mRememberPickUp": "False",
                "mResourceSinkPoints": "153184"
                XX"mSmallIcon": "Texture2D /Game/FactoryGame/Resource/Parts/PlutoniumFuelRods/UI/IconDesc_PlutoniumFuelRod_64.IconDesc_PlutoniumFuelRod_64",
                XX"mSpentFuelClass": "BlueprintGeneratedClass'/Game/FactoryGame/Resource/Parts/NuclearWaste/Desc_PlutoniumWaste.Desc_PlutoniumWaste_C'",
                "mStackSize": "SS_SMALL",
            */

            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.NuclearFuel]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = ItemType.NuclearFuel,
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints.Value,
                StackSize = _.StackSize.Value,
            }));
        }

        private void AddSchematics(List<Root> rootData, SFGameData gameData)
        { // TODO not finished, don't think I'm going to need this
            gameData.Schematics.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Schematic]).Classes.Select(_ => new Schematic
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
                .ToList());
        }

        private void AddItems(List<Root> rootData, SFGameData gameData)
        {
            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Item]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = GetCategory(_),
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints.Value,
                StackSize = _.StackSize.Value,
            })
                .ToList());
        }

        private void AddBiomassItems(List<Root> rootData, SFGameData gameData)
        {
            /*
                XX"mAbbreviatedDisplayName": "",
                "mCanBeDiscarded": "True",
                "ClassName": "Desc_Biofuel_C",
                "mDescription": "The most energy-efficient form of solid biomass. Can be used as fuel for the Chainsaw.",
                "mDisplayName": "Solid Biofuel",
                "mEnergyValue": "450.000000",
                XX"mFluidColor": "(B=0,G=0,R=0,A=0)",
                "mForm": "RF_SOLID",
                XX"mGasColor": "(B=0,G=0,R=0,A=0)",
                XX"mPersistentBigIcon": "Texture2D /Game/FactoryGame/Resource/Parts/SolidBiofuel/UI/IconDesc_SolidBiofuel_256.IconDesc_SolidBiofuel_256",
                "mRadioactiveDecay": "0.000000",
                "mRememberPickUp": "False",
                "mResourceSinkPoints": "48",
                XX"mSmallIcon": "Texture2D /Game/FactoryGame/Resource/Parts/SolidBiofuel/UI/IconDesc_SolidBiofuel_64.IconDesc_SolidBiofuel_64",
                "mStackSize": "SS_BIG"
            */

            // TODO some of these things don't make sense as biomass tbh
            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Biomass]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = GetCategory(_),
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints.Value,
                StackSize = _.StackSize.Value,
            }));

            static ItemType GetCategory(Class _)
            {
                if (_.DisplayName.Contains("Packaged"))
                    return ItemType.Package;

                if (_.DisplayName.Contains("Liquid"))
                    return ItemType.Fluid;

                if (_.ClassName == "Desc_Fabric_C")
                    return ItemType.Component;

                return ItemType.Biomass;
            }
        }

        private void AddConsumableItems(List<Root> rootData, SFGameData gameData)
        {
            /*
                XX"mAbbreviatedDisplayName": "",
                "mCanBeDiscarded": "True",
                "ClassName": "Desc_Berry_C",
                XX"mCustomHandsMeshScale": "1.000000",
                XX"mCustomLocation": "(X=0.000000,Y=0.000000,Z=0.000000)",
                XX"mCustomRotation": "(Pitch=0.000000,Yaw=-90.000000,Roll=0.000000)",
                "mDescription": "Slot: Hands\r\nConsumable\r\n\r\nCan be eaten to restore one health segment.",
                "mDisplayName": "Paleberry",
                "mEnergyValue": "0.000000",
                XX"mFluidColor": "(B=0,G=0,R=0,A=0)",
                "mForm": "RF_SOLID",
                XX"mGasColor": "(B=0,G=0,R=0,A=0)",
                "mHealthGain": "10.000000",
                XX"mPersistentBigIcon": "Texture2D /Game/FactoryGame/Resource/Environment/Berry/UI/Berry_256.Berry_256",
                "mRadioactiveDecay": "0.000000",
                "mRememberPickUp": "True",
                "mResourceSinkPoints": "0",
                XX"mSmallIcon": "Texture2D /Game/FactoryGame/Resource/Environment/Berry/UI/Berry_64.Berry_64",
                "mStackSize": "SS_SMALL"
            */
            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Consumable]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = GetCategory(_),
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                HealthGain = _.HealthGain,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints.Value,
                StackSize = _.StackSize.Value,
            }));

            static ItemType GetCategory(Class _)
            {
                if (_.HealthGain > 0)
                    return ItemType.HealingItem;
                return ItemType.Equipment;
            }
        }

        private void AddResourceItems(List<Root> rootData, SFGameData gameData)
        {
            gameData.Items.AddRange(rootData.First(_ => _.NativeClass == ClassTypeMap[ClassType.Resource]).Classes.Select(_ => new Item
            {
                CanBeDiscarded = _.CanBeDiscarded.Value,
                Type = ItemType.Resource,
                ClassName = _.ClassName,
                Description = _.Description,
                DisplayName = _.DisplayName,
                EnergyValue = _.EnergyValue.Value,
                Form = _.Form.Value,
                RadioactiveDecay = _.RadioactiveDecay.Value,
                RememberPickUp = _.RememberPickUp.Value,
                ResourceSinkPoints = _.ResourceSinkPoints.Value,
                StackSize = _.StackSize.Value,
            }));
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

                var rawAmount = int.Parse(amount.Substring(amount.IndexOf("=") + 1));
                var adjustedAmount = producedItem.Form == ResourceForm.RF_LIQUID || producedItem.Form == ResourceForm.RF_GAS ? rawAmount / 1000 : rawAmount;

                products.Add(new Product
                {
                    Item = producedItem,
                    ItemClass = itemClass,
                    Amount = adjustedAmount,
                });
            }

            return products;
        }

        private ItemType GetCategory(Class c)
        {
            if (c.BigIcon.Contains("Christmas"))
                return ItemType.Ficsmas;

            if (c.ClassName.Contains("SpaceElevatorPart"))
                return ItemType.ProjectAssembly;

            if (c.ClassName.Contains("Waste"))
                return ItemType.NuclearWaste;

            if (c.ClassName.Contains("Canister") || c.DisplayName.Contains("Empty") || c.DisplayName.Contains("Packaged"))
                return ItemType.Package;

            if (c.DisplayName.Contains("Uranium") || c.DisplayName.Contains("Plutonium"))
                return ItemType.Nuclear;

            if (c.ClassName.Contains("Filter") || c.ClassName == "Desc_HUBParts_C")
                return ItemType.Equipment;

            if (c.BigIcon.Contains("Parts") || c.BigIcon.Contains("Resource/RawResources"))
            {
                if (c.BigIcon.Contains("Ingot"))
                    return ItemType.Ingot;
                else if (c.StackSize == StackSize.SS_FLUID)
                    return ItemType.Fluid;
                else
                    return ItemType.Component;
            }

            if (c.ClassName.Contains("Crystal"))
                return ItemType.PowerShard;

            throw new System.Exception("Can't get category for this item");
        }
    }
}
