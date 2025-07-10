using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameData.Tests
{
    public class Tests
    {
        private static Dictionary<Building, string> BuildingMap = new Dictionary<Building, string>()
        {
            { Building.CraftBench, "WorkBench/BP_WorkBenchComponent.BP_WorkBenchComponent_C" },
            { Building.EquipmentWorkshop, "WorkBench/BP_WorkshopComponent.BP_WorkshopComponent_C" },
            { Building.Smelter, "SmelterMk1/Build_SmelterMk1.Build_SmelterMk1_C" },
            { Building.Foundry, "FoundryMk1/Build_FoundryMk1.Build_FoundryMk1_C" },
            { Building.Constructor, "ConstructorMk1/Build_ConstructorMk1.Build_ConstructorMk1_C" },
            { Building.Assembler, "AssemblerMk1/Build_AssemblerMk1.Build_AssemblerMk1_C" },
            { Building.Refinery, "OilRefinery/Build_OilRefinery.Build_OilRefinery_C" },
            { Building.Packager, "Packager/Build_Packager.Build_Packager_C" },
            { Building.Manufacturer, "Build_ManufacturerMk1.Build_ManufacturerMk1_C" },
            { Building.Blender, "Blender/Build_Blender.Build_Blender_C" },
            { Building.ParticleAccelerator, "HadronCollider/Build_HadronCollider.Build_HadronCollider_C" },
            { Building.Converter, "Converter/Build_Converter.Build_Converter_C" },
            { Building.QuantumEncoder, "QuantumEncoder/Build_QuantumEncoder.Build_QuantumEncoder_C" }
        };

        [Test]
        public void Test1()
        {
            var docFile = File.ReadAllText(@"C:\Users\Thana\source\repos\SatisfactoryPlanner\src\Database\DatabaseMigrator\GameData\Docs_update_1.1.json");
            var root = JsonConvert.DeserializeObject<List<Root>>(docFile)!;
            var allClasses = root.SelectMany(_ => _.Classes);
            var existingItems = Item.All;
            var allChanges = new List<string>();

            CheckForItemChanges(allClasses, existingItems, allChanges);
            CheckForNewItems(allClasses, existingItems, allChanges);

            // Check recipes
            var updatedRecipes = allClasses
                            .Where(_ => !string.IsNullOrEmpty(_.MIngredients) || !string.IsNullOrEmpty(_.MProduct))
                            .Where(_ => !string.IsNullOrWhiteSpace(_.MProducedIn) && _.MProducedIn != "(\"/Game/FactoryGame/Equipment/BuildGun/BP_BuildGun.BP_BuildGun_C\")" && _.MProducedIn != "(\"/Script/FactoryGame.FGBuildGun\")")
                            .ToList();
            var existingRecipes = Recipe.All;

            CheckForRecipeChanges(allClasses, allChanges, updatedRecipes, existingRecipes);
            CheckForNewRecipes(allChanges, updatedRecipes, existingRecipes);

            File.WriteAllLines(@"C:\Users\Thana\source\repos\SatisfactoryPlanner\src\Database\DatabaseMigrator\GameData\changes.txt", allChanges);
        }

        private static void CheckForItemChanges(IEnumerable<Class> allClasses, List<Item> existingItems, List<string> allChanges)
        {
            foreach (var item in existingItems)
            {
                var matchedItems = allClasses.Where(_ => _.MDisplayName == item.Name && !string.IsNullOrWhiteSpace(_.MForm));

                if (matchedItems.Count() > 1)
                    throw new Exception($"More than 1 {item.Name} found.");

                var itemChanges = new List<string>();

                if (matchedItems.Count() == 0)
                {
                    itemChanges.Add($"{item.Name} Changes");
                    itemChanges.Add($"=======================");
                    itemChanges.Add($"Cannot find item in docs. Removed, or name was changed.");
                    itemChanges.Add($"");
                    allChanges.AddRange(itemChanges);
                    continue;
                }

                var updatedResource = matchedItems.Single();
                if ((item.StackSize == StackSize.One && updatedResource.MStackSize != "SS_ONE") ||
                    (item.StackSize == StackSize.Small && updatedResource.MStackSize != "SS_SMALL") ||
                    (item.StackSize == StackSize.Medium && updatedResource.MStackSize != "SS_MEDIUM") ||
                    (item.StackSize == StackSize.Big && updatedResource.MStackSize != "SS_BIG") ||
                    (item.StackSize == StackSize.Huge && updatedResource.MStackSize != "SS_HUGE") ||
                    (item.StackSize == StackSize.Fluid && updatedResource.MStackSize != "SS_FLUID"))
                    itemChanges.Add($"StackSize -> '{updatedResource.MStackSize}'");

                if ((item.Form == ResourceForm.Solid && updatedResource.MForm != "RF_SOLID") ||
                    (item.Form == ResourceForm.Liquid && updatedResource.MForm != "RF_LIQUID") ||
                    (item.Form == ResourceForm.Gas && updatedResource.MForm != "RF_GAS"))
                    itemChanges.Add($"Form -> '{updatedResource.MForm}'");

                if (item.ResourceSinkPoints != updatedResource.MResourceSinkPoints)
                    itemChanges.Add($"ResourceSinkPoints -> '{updatedResource.MResourceSinkPoints}'");

                if (item.EnergyValue != updatedResource.MEnergyValue)
                    itemChanges.Add($"EnergyValue -> '{updatedResource.MEnergyValue}'");

                if (item.RadioactiveDecay != updatedResource.MRadioactiveDecay)
                    itemChanges.Add($"RadioactiveDecay -> '{updatedResource.MRadioactiveDecay}'");

                if (item.HealthGain != updatedResource.MHealthGain.GetValueOrDefault(0))
                    itemChanges.Add($"HealthGain -> '{updatedResource.MHealthGain}'");

                if (item.Description != updatedResource.MDescription)
                    itemChanges.Add($"Description -> '{updatedResource.MDescription}'");

                if (itemChanges.Any())
                {
                    allChanges.Add($"{item.Name} Changes");
                    allChanges.Add($"=======================");
                    allChanges.AddRange(itemChanges);
                    allChanges.Add($"");
                }
            }
        }

        private static void CheckForNewItems(IEnumerable<Class> allClasses, List<Item> existingItems, List<string> allChanges)
        {
            var newItems = allClasses
                            .Where(_ => !string.IsNullOrWhiteSpace(_.MDisplayName) && existingItems.All(ei => ei.Name != _.MDisplayName) && _.MForm != null && _.MForm != "RF_INVALID")
                            .Select(_ => new
                            {
                                Name = _.MDisplayName,
                                StackSize = _.MStackSize,
                                Form = _.MForm,
                                ResourceSinkPoints = _.MResourceSinkPoints,
                                EnergyValue = _.MEnergyValue,
                                RadioactiveDecay = _.MRadioactiveDecay,
                                HealthGain = _.MHealthGain,
                                Description = _.MDescription,
                            })
                            .ToList();

            allChanges.Add($"New Items");
            allChanges.Add($"=======================");
            allChanges.AddRange(newItems.Select(newItem => newItem.ToString()));
            allChanges.Add($"");
        }

        private static void CheckForNewRecipes(List<string> allChanges, List<Class> updatedRecipes, List<Recipe> existingRecipes)
        {
            var newRecipes = updatedRecipes
                            .Where(_ => existingRecipes.All(ei =>
                            {
                                var displayName = _.MDisplayName.Replace("Alternate: ", "");

                                return displayName != ei.Name;
                            }))
                            .Select(_ => new
                            {
                                Name = _.MDisplayName,
                                Ingredients = _.MIngredients,
                                Products = _.MProduct,
                                ProductedIn = _.MProducedIn,
                                ManufactoringDuration = _.MManufactoringDuration,
                                ManualManufacturingMultiplier = _.MManualManufacturingMultiplier,
                                VariablePowerConsumptionConstant = _.MVariablePowerConsumptionConstant,
                                VariablePowerConsumptionFactor = _.MVariablePowerConsumptionFactor

                            })
                            .ToList();

            if (!newRecipes.Any())
                return;

            allChanges.Add($"New Recipes");
            allChanges.Add($"=======================");
            allChanges.AddRange(newRecipes.Select(newRecipes => newRecipes.ToString()));
            allChanges.Add($"");
        }

        private static void CheckForRecipeChanges(IEnumerable<Class> allClasses, List<string> allChanges, List<Class> updatedRecipes, List<Recipe> existingRecipes)
        {
            foreach (var recipe in existingRecipes)
            {
                var matchedRecipes = updatedRecipes.Where(_ =>
                {
                    var displayName = _.MDisplayName.Replace("Alternate: ", "");

                    return displayName == recipe.Name;
                });

                var recipeChanges = new List<string>();
                if (matchedRecipes.Count() > 1)
                {
                    recipeChanges.Add($"{recipe.Name} Changes");
                    recipeChanges.Add($"=======================");
                    recipeChanges.Add($"More than 1 {recipe.Name} found.");
                    recipeChanges.Add($"");
                    allChanges.AddRange(recipeChanges);
                    continue;
                }

                if (matchedRecipes.Count() == 0)
                {
                    recipeChanges.Add($"{recipe.Name} Changes");
                    recipeChanges.Add($"=======================");
                    recipeChanges.Add($"Cannot find item in docs. Removed, or name was changed.");
                    recipeChanges.Add($"");
                    allChanges.AddRange(recipeChanges);
                    continue;
                }

                var updatedRecipe = matchedRecipes.Single();

                if (recipe.Type == RecipeType.Alternate && !updatedRecipe.MDisplayName.StartsWith("Alternate: "))
                    recipeChanges.Add($"Type -> 'Standard'");

                if (recipe.Type == RecipeType.Standard && updatedRecipe.MDisplayName.StartsWith("Alternate: "))
                    recipeChanges.Add($"Type -> 'Alternate'");

                if (recipe.ManufacturingTime.Duration != updatedRecipe.MManufactoringDuration)
                    recipeChanges.Add($"ManufacturingTime.Duration -> '{updatedRecipe.MManufactoringDuration}'");

                if (recipe.ManufacturingTime.ManualMultiplier != updatedRecipe.MManualManufacturingMultiplier)
                    recipeChanges.Add($"ManufacturingTime.ManualMultiplier -> '{updatedRecipe.MManualManufacturingMultiplier}'");

                if (recipe.VariablePowerConsumption.Constant != updatedRecipe.MVariablePowerConsumptionConstant)
                    recipeChanges.Add($"VariablePowerConsumption.Constant -> '{updatedRecipe.MVariablePowerConsumptionConstant}'");

                if (recipe.VariablePowerConsumption.Factor != updatedRecipe.MVariablePowerConsumptionFactor)
                    recipeChanges.Add($"VariablePowerConsumption.Factor -> '{updatedRecipe.MVariablePowerConsumptionFactor}'");

                var updatedProducedIn = updatedRecipe.MProducedIn.Split(",").ToList();

                // Check all existing buildings to make sure they are still in the updated list
                foreach (var existingProductionBuilding in recipe.ProducedIn)
                {
                    if (updatedProducedIn.All(str => !str.Contains(BuildingMap[existingProductionBuilding])))
                        recipeChanges.Add($"ProducedIn -> Remove '{existingProductionBuilding.Name}'");
                }

                // Check all updated buildings to check if they are in existing
                foreach (var updatedProductionBuilding in updatedProducedIn)
                {
                    var buildingExists = false;
                    foreach (var buildingMap in BuildingMap)
                    {
                        buildingExists = true;
                        if (updatedProductionBuilding.Contains(buildingMap.Value) && !recipe.ProducedIn.Contains(buildingMap.Key))
                            recipeChanges.Add($"ProducedIn -> Add '{buildingMap.Key.Name}'");
                    }

                    // Check for buildings that don't exist in map
                    if (!buildingExists)
                        recipeChanges.Add($"ProducedIn -> Missing Building for '{updatedProductionBuilding}'");
                }

                // Check for ingredients
                var updatedIngredients = updatedRecipe.MIngredients.Split("),(").Select(a => a.Trim('(').Trim('"').Trim(')'))
                    .Where(ingredient => !string.IsNullOrWhiteSpace(ingredient))
                    .Select(ingredient =>
                    {
                        var a = ingredient.Split(",");

                        return new
                        {
                            ItemClass = a[0].Split("=")[1],
                            Amount = Convert.ToDecimal(a[1].Split("=")[1])
                        };
                    })
                    .ToList();

                // Check existing ingredients are still used and use the same amount
                foreach (var existingIngredient in recipe.Ingredients)
                {
                    var matchedItem = allClasses.Single(_ => _.MDisplayName == existingIngredient.Item.Name && !string.IsNullOrWhiteSpace(_.MForm)).ClassName; // find classname
                    var matchingIngredient = updatedIngredients.SingleOrDefault(ingredient => ingredient.ItemClass.Contains(matchedItem));
                    if (matchingIngredient == null)
                        recipeChanges.Add($"Ingredient '{existingIngredient.Item.Name}' -> Removed");
                    else if (matchingIngredient.Amount != (existingIngredient.Item.Form == ResourceForm.Solid ? existingIngredient.Amount : existingIngredient.Amount * 1000))
                        recipeChanges.Add($"Ingredient '{existingIngredient.Item.Name}' -> Amount Changed to '{(existingIngredient.Item.Form == ResourceForm.Solid ? matchingIngredient.Amount : matchingIngredient.Amount / 1000)}'");
                }

                // Check all updated ingredients are in existing list of ingredients
                foreach (var updatedIngredient in updatedIngredients) // List of ingredients using ClassName
                {
                    var matchingIngredients = recipe.Ingredients.Any(ingredient =>
                    {
                        var matchedItem = allClasses.Single(_ => _.MDisplayName == ingredient.Item.Name && !string.IsNullOrWhiteSpace(_.MForm)).ClassName; // find classname
                        return updatedIngredient.ItemClass.Contains(matchedItem);
                    });

                    if (!matchingIngredients)
                        recipeChanges.Add($"Ingredients -> Add {updatedIngredient}");
                }

                // Check for products
                var updatedProducts = updatedRecipe.MProduct.Split("),(").Select(a => a.Trim('(').Trim('"').Trim(')'))
                    .Select(product =>
                    {
                        var a = product.Split(",");

                        return new
                        {
                            ItemClass = a[0].Split("=")[1],
                            Amount = Convert.ToDecimal(a[1].Split("=")[1])
                        };
                    })
                    .ToList();

                // Check existing products are still used and use the same amount
                foreach (var existingProduct in recipe.Products)
                {
                    var matchedItem = allClasses.Single(_ => _.MDisplayName == existingProduct.Item.Name && !string.IsNullOrWhiteSpace(_.MForm)).ClassName; // find classname
                    var matchingProduct = updatedProducts.SingleOrDefault(product => product.ItemClass.Contains(matchedItem));
                    if (matchingProduct == null)
                        recipeChanges.Add($"Products '{existingProduct.Item.Name}' -> Removed");
                    else if (matchingProduct.Amount != (existingProduct.Item.Form == ResourceForm.Solid ? existingProduct.Amount : existingProduct.Amount * 1000))
                        recipeChanges.Add($"Products '{existingProduct.Item.Name}' -> Amount Changed to '{(existingProduct.Item.Form == ResourceForm.Solid ? matchingProduct.Amount : matchingProduct.Amount / 1000)}'");
                }

                // Check all updated products are in existing list of products
                foreach (var updatedProduct in updatedProducts) // List of products using ClassName
                {
                    var matchingProducts = recipe.Products.Any(product =>
                    {
                        var matchedItem = allClasses.Single(_ => _.MDisplayName == product.Item.Name && !string.IsNullOrWhiteSpace(_.MForm)).ClassName; // find classname
                        return updatedProduct.ItemClass.Contains(matchedItem);
                    });

                    if (!matchingProducts)
                        recipeChanges.Add($"Products -> Add {updatedProduct}");
                }

                if (recipeChanges.Any())
                {
                    allChanges.Add($"{recipe.Name} Changes");
                    allChanges.Add($"=======================");
                    allChanges.AddRange(recipeChanges);
                    allChanges.Add($"");
                }
            }
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Class
    {
        [JsonProperty("ClassName")]
        public string ClassName { get; set; }

        [JsonProperty("mMeshLength")]
        public string MMeshLength { get; set; }

        [JsonProperty("mConnections")]
        public string MConnections { get; set; }

        [JsonProperty("mIsOwnedByPlatform")]
        public string MIsOwnedByPlatform { get; set; }

        [JsonProperty("mTrackGraphID")]
        public string MTrackGraphID { get; set; }

        [JsonProperty("mOverlappingTracks")]
        public string MOverlappingTracks { get; set; }

        [JsonProperty("mVehicles")]
        public string MVehicles { get; set; }

        [JsonProperty("mSignalBlockID")]
        public string MSignalBlockID { get; set; }

        [JsonProperty("mBlockVisualizationColorDataStartIndex")]
        public string MBlockVisualizationColorDataStartIndex { get; set; }

        [JsonProperty("mDisplayName")]
        public string MDisplayName { get; set; }

        [JsonProperty("mDescription")]
        public string MDescription { get; set; }

        [JsonProperty("MaxRenderDistance")]
        public string MaxRenderDistance { get; set; }

        [JsonProperty("mAlternativeMaterialRecipes")]
        public string MAlternativeMaterialRecipes { get; set; }

        [JsonProperty("mContainsComponents")]
        public string MContainsComponents { get; set; }

        [JsonProperty("mIsConsideredForBaseWeightValue")]
        public string MIsConsideredForBaseWeightValue { get; set; }

        [JsonProperty("mOnBuildableReturnedToLightweightPool")]
        public string MOnBuildableReturnedToLightweightPool { get; set; }

        [JsonProperty("bForceLegacyBuildEffect")]
        public string BForceLegacyBuildEffect { get; set; }

        [JsonProperty("bForceBuildEffectSolo")]
        public string BForceBuildEffectSolo { get; set; }

        [JsonProperty("mBuildEffectSpeed")]
        public string MBuildEffectSpeed { get; set; }

        [JsonProperty("mAllowColoring")]
        public string MAllowColoring { get; set; }

        [JsonProperty("mAllowPatterning")]
        public string MAllowPatterning { get; set; }

        [JsonProperty("mInteractionRegisterPlayerWithCircuit")]
        public string MInteractionRegisterPlayerWithCircuit { get; set; }

        [JsonProperty("mSkipBuildEffect")]
        public string MSkipBuildEffect { get; set; }

        [JsonProperty("mForceNetUpdateOnRegisterPlayer")]
        public string MForceNetUpdateOnRegisterPlayer { get; set; }

        [JsonProperty("mToggleDormancyOnInteraction")]
        public string MToggleDormancyOnInteraction { get; set; }

        [JsonProperty("mIsMultiSpawnedBuildable")]
        public string MIsMultiSpawnedBuildable { get; set; }

        [JsonProperty("mShouldShowAttachmentPointVisuals")]
        public string MShouldShowAttachmentPointVisuals { get; set; }

        [JsonProperty("mCanContainLightweightInstances")]
        public string MCanContainLightweightInstances { get; set; }

        [JsonProperty("mManagedByLightweightBuildableSubsystem")]
        public string MManagedByLightweightBuildableSubsystem { get; set; }

        [JsonProperty("mRemoveBuildableFromSubsystemOnDismantle")]
        public string MRemoveBuildableFromSubsystemOnDismantle { get; set; }

        [JsonProperty("mHasBeenRemovedFromSubsystem")]
        public string MHasBeenRemovedFromSubsystem { get; set; }

        [JsonProperty("mAffectsOcclusion")]
        public string MAffectsOcclusion { get; set; }

        [JsonProperty("mOcclusionShape")]
        public string MOcclusionShape { get; set; }

        [JsonProperty("mScaleCustomOffset")]
        public string MScaleCustomOffset { get; set; }

        [JsonProperty("mCustomScaleType")]
        public string MCustomScaleType { get; set; }

        [JsonProperty("mOcclusionBoxInfo")]
        public string MOcclusionBoxInfo { get; set; }

        [JsonProperty("mAttachmentPoints")]
        public string MAttachmentPoints { get; set; }

        [JsonProperty("mReplicatedBuiltInsideBlueprintDesigner")]
        public string MReplicatedBuiltInsideBlueprintDesigner { get; set; }

        [JsonProperty("mInteractWidgetSoftClass")]
        public string MInteractWidgetSoftClass { get; set; }

        [JsonProperty("mInteractingPlayers")]
        public string MInteractingPlayers { get; set; }

        [JsonProperty("mIsUseable")]
        public string MIsUseable { get; set; }

        [JsonProperty("mClearanceData")]
        public string MClearanceData { get; set; }

        [JsonProperty("mHideOnBuildEffectStart")]
        public string MHideOnBuildEffectStart { get; set; }

        [JsonProperty("mShouldModifyWorldGrid")]
        public string MShouldModifyWorldGrid { get; set; }

        [JsonProperty("mTimelapseBucketId")]
        public string MTimelapseBucketId { get; set; }

        [JsonProperty("mTimelapseDelay")]
        public string MTimelapseDelay { get; set; }

        [JsonProperty("mAlienOverClockingZOffset")]
        public string MAlienOverClockingZOffset { get; set; }

        [JsonProperty("mAlienOverClockingAttenuationScalingFactor")]
        public string MAlienOverClockingAttenuationScalingFactor { get; set; }

        [JsonProperty("mAlienOverClockingVolumeDB_RTPC")]
        public string MAlienOverClockingVolumeDBRTPC { get; set; }

        [JsonProperty("mAlienOverClockingHighpass_RTPC")]
        public string MAlienOverClockingHighpassRTPC { get; set; }

        [JsonProperty("mAlienOverClockingPitch_RTPC")]
        public string MAlienOverClockingPitchRTPC { get; set; }

        [JsonProperty("mBlueprintBuildEffectID")]
        public string MBlueprintBuildEffectID { get; set; }

        [JsonProperty("mSize")]
        public string MSize { get; set; }

        [JsonProperty("mDefaultLength")]
        public string MDefaultLength { get; set; }

        [JsonProperty("mMaxLength")]
        public string MMaxLength { get; set; }

        [JsonProperty("mLength")]
        public string MLength { get; set; }

        [JsonProperty("bTiledMesh")]
        public string BTiledMesh { get; set; }

        [JsonProperty("mUsesDistanceForZooping")]
        public string MUsesDistanceForZooping { get; set; }

        [JsonProperty("mAbbreviatedDisplayName")]
        public string MAbbreviatedDisplayName { get; set; }

        [JsonProperty("mStackSize")]
        public string MStackSize { get; set; }

        [JsonProperty("mCanBeDiscarded")]
        public string MCanBeDiscarded { get; set; }

        [JsonProperty("mRememberPickUp")]
        public string MRememberPickUp { get; set; }

        [JsonProperty("mEnergyValue")]
        public decimal? MEnergyValue { get; set; }

        [JsonProperty("mRadioactiveDecay")]
        public decimal? MRadioactiveDecay { get; set; }

        [JsonProperty("mForm")]
        public string MForm { get; set; }

        [JsonProperty("mGasType")]
        public string MGasType { get; set; }

        [JsonProperty("mSmallIcon")]
        public string MSmallIcon { get; set; }

        [JsonProperty("mPersistentBigIcon")]
        public string MPersistentBigIcon { get; set; }

        [JsonProperty("mCrosshairMaterial")]
        public string MCrosshairMaterial { get; set; }

        [JsonProperty("mDescriptorStatBars")]
        public string MDescriptorStatBars { get; set; }

        [JsonProperty("mIsAlienItem")]
        public string MIsAlienItem { get; set; }

        [JsonProperty("mSubCategories")]
        public string MSubCategories { get; set; }

        [JsonProperty("mMenuPriority")]
        public string MMenuPriority { get; set; }

        [JsonProperty("mFluidColor")]
        public string MFluidColor { get; set; }

        [JsonProperty("mGasColor")]
        public string MGasColor { get; set; }

        [JsonProperty("mCompatibleItemDescriptors")]
        public string MCompatibleItemDescriptors { get; set; }

        [JsonProperty("mClassToScanFor")]
        public string MClassToScanFor { get; set; }

        [JsonProperty("mScannableType")]
        public string MScannableType { get; set; }

        [JsonProperty("mShouldOverrideScannerDisplayText")]
        public string MShouldOverrideScannerDisplayText { get; set; }

        [JsonProperty("mScannerDisplayText")]
        public string MScannerDisplayText { get; set; }

        [JsonProperty("mScannerLightColor")]
        public string MScannerLightColor { get; set; }

        [JsonProperty("mNeedsPickUpMarker")]
        public string MNeedsPickUpMarker { get; set; }

        [JsonProperty("mResourceSinkPoints")]
        public long? MResourceSinkPoints { get; set; }

        [JsonProperty("mInitialProjectileSpeedOverride")]
        public string MInitialProjectileSpeedOverride { get; set; }

        [JsonProperty("mProjectileMaxSpeedOverride")]
        public string MProjectileMaxSpeedOverride { get; set; }

        [JsonProperty("mProjectileHealthOverride")]
        public string MProjectileHealthOverride { get; set; }

        [JsonProperty("mProjectileLifespan")]
        public string MProjectileLifespan { get; set; }

        [JsonProperty("mProjectileStickspan")]
        public string MProjectileStickspan { get; set; }

        [JsonProperty("mCanTakeDamageBySameProjectileOrChild")]
        public string MCanTakeDamageBySameProjectileOrChild { get; set; }

        [JsonProperty("mDamageTypesAtEndOfLife")]
        public string MDamageTypesAtEndOfLife { get; set; }

        [JsonProperty("mGravityScaleOverLifespan")]
        public string MGravityScaleOverLifespan { get; set; }

        [JsonProperty("mHomingProjectile")]
        public string MHomingProjectile { get; set; }

        [JsonProperty("mHomingNeedsValidTarget")]
        public string MHomingNeedsValidTarget { get; set; }

        [JsonProperty("mMaxHomingAccelerationMagnitudeOverride")]
        public string MMaxHomingAccelerationMagnitudeOverride { get; set; }

        [JsonProperty("mHomingMagnitudeMultiplierOverLifespan")]
        public string MHomingMagnitudeMultiplierOverLifespan { get; set; }

        [JsonProperty("mHomingMagnitudeMultiplierOverDistanceToTarget")]
        public string MHomingMagnitudeMultiplierOverDistanceToTarget { get; set; }

        [JsonProperty("mHomingOverlapSize")]
        public string MHomingOverlapSize { get; set; }

        [JsonProperty("mHomingAngleLimit")]
        public string MHomingAngleLimit { get; set; }

        [JsonProperty("mHomingOverrideTargets")]
        public string MHomingOverrideTargets { get; set; }

        [JsonProperty("AmmoFiredDelegate")]
        public string AmmoFiredDelegate { get; set; }

        [JsonProperty("mFiringTransform")]
        public string MFiringTransform { get; set; }

        [JsonProperty("mFiringDirection")]
        public string MFiringDirection { get; set; }

        [JsonProperty("mMagazineSize")]
        public string MMagazineSize { get; set; }

        [JsonProperty("mMaxAmmoEffectiveRange")]
        public string MMaxAmmoEffectiveRange { get; set; }

        [JsonProperty("mReloadTimeMultiplier")]
        public string MReloadTimeMultiplier { get; set; }

        [JsonProperty("mFireRate")]
        public string MFireRate { get; set; }

        [JsonProperty("mFiringTransformIgnoresDispersion")]
        public string MFiringTransformIgnoresDispersion { get; set; }

        [JsonProperty("mDispersionFireRateMultiplier")]
        public string MDispersionFireRateMultiplier { get; set; }

        [JsonProperty("mDispersionPerShot")]
        public string MDispersionPerShot { get; set; }

        [JsonProperty("mRestingDispersion")]
        public string MRestingDispersion { get; set; }

        [JsonProperty("mFiringDispersion")]
        public string MFiringDispersion { get; set; }

        [JsonProperty("mDispersionRecoveryTime")]
        public string MDispersionRecoveryTime { get; set; }

        [JsonProperty("mHasBeenInitialized")]
        public string MHasBeenInitialized { get; set; }

        [JsonProperty("mWeaponDamageMultiplier")]
        public string MWeaponDamageMultiplier { get; set; }

        [JsonProperty("mMagazineMeshMaterials")]
        public string MMagazineMeshMaterials { get; set; }

        [JsonProperty("mMagazineMeshMaterials1p")]
        public string MMagazineMeshMaterials1p { get; set; }

        [JsonProperty("mDamageTypesOnImpact")]
        public string MDamageTypesOnImpact { get; set; }

        [JsonProperty("mAmmoDamageFalloff")]
        public string MAmmoDamageFalloff { get; set; }

        [JsonProperty("mMuzzleFlashScale")]
        public string MMuzzleFlashScale { get; set; }

        [JsonProperty("mFiringSounds")]
        public string MFiringSounds { get; set; }

        [JsonProperty("mFiringSounds1P")]
        public string MFiringSounds1P { get; set; }

        [JsonProperty("mAudioEventsCache")]
        public string MAudioEventsCache { get; set; }

        [JsonProperty("mAmmoColor")]
        public string MAmmoColor { get; set; }

        [JsonProperty("mAmmoScale")]
        public string MAmmoScale { get; set; }

        [JsonProperty("mAmmoTickFunction")]
        public string MAmmoTickFunction { get; set; }

        [JsonProperty("SpreadTrail_Velocity")]
        public string SpreadTrailVelocity { get; set; }

        [JsonProperty("mNumShots")]
        public string MNumShots { get; set; }

        [JsonProperty("mSpreadAngleDegrees")]
        public string MSpreadAngleDegrees { get; set; }

        [JsonProperty("mFireMontage")]
        public string MFireMontage { get; set; }

        [JsonProperty("mReloadMontageList")]
        public string MReloadMontageList { get; set; }

        [JsonProperty("mAmmoSwapMontageList")]
        public string MAmmoSwapMontageList { get; set; }

        [JsonProperty("mFailedToFireMontageList")]
        public string MFailedToFireMontageList { get; set; }

        [JsonProperty("mSupressDryFireMontage")]
        public string MSupressDryFireMontage { get; set; }

        [JsonProperty("mOnWeaponStateChanged")]
        public string MOnWeaponStateChanged { get; set; }

        [JsonProperty("mWeaponState")]
        public string MWeaponState { get; set; }

        [JsonProperty("mAutomaticallyReload")]
        public string MAutomaticallyReload { get; set; }

        [JsonProperty("mAutoReloadDelay")]
        public string MAutoReloadDelay { get; set; }

        [JsonProperty("mAutoReloadTimerHandle")]
        public string MAutoReloadTimerHandle { get; set; }

        [JsonProperty("mCurrentAmmoCount")]
        public string MCurrentAmmoCount { get; set; }

        [JsonProperty("mAllowedAmmoClasses")]
        public string MAllowedAmmoClasses { get; set; }

        [JsonProperty("mAttachMagazineToPlayer")]
        public string MAttachMagazineToPlayer { get; set; }

        [JsonProperty("mMuzzleSocketName")]
        public string MMuzzleSocketName { get; set; }

        [JsonProperty("mCurrentMagazineBoneName")]
        public string MCurrentMagazineBoneName { get; set; }

        [JsonProperty("mEjectMagazineBoneName")]
        public string MEjectMagazineBoneName { get; set; }

        [JsonProperty("mDispersionOnNoMagazine")]
        public string MDispersionOnNoMagazine { get; set; }

        [JsonProperty("mFiringBlocksDispersionReduction")]
        public string MFiringBlocksDispersionReduction { get; set; }

        [JsonProperty("mCurrentDispersion")]
        public string MCurrentDispersion { get; set; }

        [JsonProperty("mReloadTime")]
        public string MReloadTime { get; set; }

        [JsonProperty("mAmmoSwitchUsedRadialMenu")]
        public string MAmmoSwitchUsedRadialMenu { get; set; }

        [JsonProperty("mOnAmmoCyclingPressed")]
        public string MOnAmmoCyclingPressed { get; set; }

        [JsonProperty("mOnAmmoCyclingReleased")]
        public string MOnAmmoCyclingReleased { get; set; }

        [JsonProperty("mBlockSprintWhenFiring")]
        public string MBlockSprintWhenFiring { get; set; }

        [JsonProperty("mEquipmentSlot")]
        public string MEquipmentSlot { get; set; }

        [JsonProperty("mEquipMontage")]
        public string MEquipMontage { get; set; }

        [JsonProperty("mHasStingerMontage")]
        public string MHasStingerMontage { get; set; }

        [JsonProperty("mStingerMontage")]
        public string MStingerMontage { get; set; }

        [JsonProperty("mUnEquipMontage")]
        public string MUnEquipMontage { get; set; }

        [JsonProperty("mMontageBlendOutTime")]
        public string MMontageBlendOutTime { get; set; }

        [JsonProperty("mAttachSocket")]
        public string MAttachSocket { get; set; }

        [JsonProperty("mComponentNameToFirstPersonMaterials")]
        public string MComponentNameToFirstPersonMaterials { get; set; }

        [JsonProperty("mNeedsDefaultEquipmentMappingContext")]
        public string MNeedsDefaultEquipmentMappingContext { get; set; }

        [JsonProperty("mCostToUse")]
        public string MCostToUse { get; set; }

        [JsonProperty("mArmAnimation")]
        public string MArmAnimation { get; set; }

        [JsonProperty("mBackAnimation")]
        public string MBackAnimation { get; set; }

        [JsonProperty("mDefaultEquipmentActions")]
        public string MDefaultEquipmentActions { get; set; }

        [JsonProperty("mMagnetismStrength")]
        public string MMagnetismStrength { get; set; }

        [JsonProperty("mMagnetismZeroInputStrength")]
        public string MMagnetismZeroInputStrength { get; set; }

        [JsonProperty("bMagnetismActive")]
        public string BMagnetismActive { get; set; }

        [JsonProperty("mReceivedDamageModifiers")]
        public string MReceivedDamageModifiers { get; set; }

        [JsonProperty("mSwappedOutThirdPersonMaterials")]
        public string MSwappedOutThirdPersonMaterials { get; set; }

        [JsonProperty("mEquipmentLookAtDescOverride")]
        public string MEquipmentLookAtDescOverride { get; set; }

        [JsonProperty("Fire")]
        public string Fire { get; set; }

        [JsonProperty("mHasReloadedOnce")]
        public string MHasReloadedOnce { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Trail_Velocity")]
        public string TrailVelocity { get; set; }

        [JsonProperty("mPlayFireEffects")]
        public string MPlayFireEffects { get; set; }

        [JsonProperty("mWidth")]
        public string MWidth { get; set; }

        [JsonProperty("mHeight")]
        public string MHeight { get; set; }

        [JsonProperty("mElevation")]
        public string MElevation { get; set; }

        [JsonProperty("mAngularDepth")]
        public string MAngularDepth { get; set; }

        [JsonProperty("mWallType")]
        public string MWallType { get; set; }

        [JsonProperty("mAngledVariants")]
        public string MAngledVariants { get; set; }

        [JsonProperty("mImmunity")]
        public string MImmunity { get; set; }

        [JsonProperty("mIsWorking")]
        public string MIsWorking { get; set; }

        [JsonProperty("mHasNegatedDamage")]
        public string MHasNegatedDamage { get; set; }

        [JsonProperty("mDamageNegated")]
        public string MDamageNegated { get; set; }

        [JsonProperty("mFilterDuration")]
        public string MFilterDuration { get; set; }

        [JsonProperty("mCountdown")]
        public string MCountdown { get; set; }

        [JsonProperty("mDisableEffectTimer")]
        public string MDisableEffectTimer { get; set; }

        [JsonProperty("mIsBurningFuel")]
        public string MIsBurningFuel { get; set; }

        [JsonProperty("mSuitMeshMaterials")]
        public string MSuitMeshMaterials { get; set; }

        [JsonProperty("mRandomAnim")]
        public string MRandomAnim { get; set; }

        [JsonProperty("mEatMontage")]
        public string MEatMontage { get; set; }

        [JsonProperty("mReEquipAfterEatMontage")]
        public string MReEquipAfterEatMontage { get; set; }

        [JsonProperty("mMedkitUseMontages")]
        public string MMedkitUseMontages { get; set; }

        [JsonProperty("mCurrentMedkitUseMontage")]
        public string MCurrentMedkitUseMontage { get; set; }

        [JsonProperty("AmmoTypeToAkEvent")]
        public string AmmoTypeToAkEvent { get; set; }

        [JsonProperty("mShowCycleAmmoRadialMenuTimer")]
        public string MShowCycleAmmoRadialMenuTimer { get; set; }

        [JsonProperty("mRadialMenuShowUpTime")]
        public string MRadialMenuShowUpTime { get; set; }

        [JsonProperty("mPrimaryFireStartMontageList")]
        public string MPrimaryFireStartMontageList { get; set; }

        [JsonProperty("mSecondaryFireMontageList")]
        public string MSecondaryFireMontageList { get; set; }

        [JsonProperty("mPrimaryFireEndMontageList")]
        public string MPrimaryFireEndMontageList { get; set; }

        [JsonProperty("bHasDispensedProjectiles")]
        public string BHasDispensedProjectiles { get; set; }

        [JsonProperty("mDispensedProjectiles")]
        public string MDispensedProjectiles { get; set; }

        [JsonProperty("mMaxChargeTime")]
        public string MMaxChargeTime { get; set; }

        [JsonProperty("mReleaseCooldown")]
        public string MReleaseCooldown { get; set; }

        [JsonProperty("mMaxThrowForce")]
        public string MMaxThrowForce { get; set; }

        [JsonProperty("mMinThrowForce")]
        public string MMinThrowForce { get; set; }

        [JsonProperty("mDelayBetweenSecondaryTriggers")]
        public string MDelayBetweenSecondaryTriggers { get; set; }

        [JsonProperty("mDecalSize")]
        public string MDecalSize { get; set; }

        [JsonProperty("mPingColor")]
        public string MPingColor { get; set; }

        [JsonProperty("mCollectSpeedMultiplier")]
        public string MCollectSpeedMultiplier { get; set; }

        [JsonProperty("mManualMiningAudioName")]
        public string MManualMiningAudioName { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("mIngredients")]
        public string MIngredients { get; set; }

        [JsonProperty("mProduct")]
        public string MProduct { get; set; }

        [JsonProperty("mManufacturingMenuPriority")]
        public string MManufacturingMenuPriority { get; set; }

        [JsonProperty("mManufactoringDuration")]
        public decimal? MManufactoringDuration { get; set; }

        [JsonProperty("mManualManufacturingMultiplier")]
        public decimal? MManualManufacturingMultiplier { get; set; }

        [JsonProperty("mProducedIn")]
        public string MProducedIn { get; set; }

        [JsonProperty("mRelevantEvents")]
        public string MRelevantEvents { get; set; }

        [JsonProperty("mVariablePowerConsumptionConstant")]
        public decimal? MVariablePowerConsumptionConstant { get; set; }

        [JsonProperty("mVariablePowerConsumptionFactor")]
        public decimal? MVariablePowerConsumptionFactor { get; set; }

        [JsonProperty("mWorkBenchOccupied")]
        public string MWorkBenchOccupied { get; set; }

        [JsonProperty("mWorkBenchFree")]
        public string MWorkBenchFree { get; set; }

        [JsonProperty("Meshes")]
        public string Meshes { get; set; }

        [JsonProperty("mShipUpgradeLevel")]
        public string MShipUpgradeLevel { get; set; }

        [JsonProperty("mStorageText")]
        public string MStorageText { get; set; }

        [JsonProperty("mMamFreeText")]
        public string MMamFreeText { get; set; }

        [JsonProperty("mMamOccupiedText")]
        public string MMamOccupiedText { get; set; }

        [JsonProperty("mMeshes")]
        public string MMeshes { get; set; }

        [JsonProperty("ABClass")]
        public string ABClass { get; set; }

        [JsonProperty("mSkeletalMeshSoftPtr")]
        public string MSkeletalMeshSoftPtr { get; set; }

        [JsonProperty("mStages")]
        public string MStages { get; set; }

        [JsonProperty("mLadderVisibilityLevel")]
        public string MLadderVisibilityLevel { get; set; }

        [JsonProperty("mGenerators")]
        public string MGenerators { get; set; }

        [JsonProperty("mStorageInventorySize")]
        public string MStorageInventorySize { get; set; }

        [JsonProperty("mStorageVisibilityLevel")]
        public string MStorageVisibilityLevel { get; set; }

        [JsonProperty("mLockerVisibilityLevel")]
        public string MLockerVisibilityLevel { get; set; }

        [JsonProperty("mMiniGameAndCalendarVisibilityLevel")]
        public string MMiniGameAndCalendarVisibilityLevel { get; set; }

        [JsonProperty("mSpawningGroundZOffset")]
        public string MSpawningGroundZOffset { get; set; }

        [JsonProperty("mGroundSearchZDistance")]
        public string MGroundSearchZDistance { get; set; }

        [JsonProperty("mDefaultResources")]
        public string MDefaultResources { get; set; }

        [JsonProperty("mRepresentationText")]
        public string MRepresentationText { get; set; }

        [JsonProperty("mPowerConsumption")]
        public string MPowerConsumption { get; set; }

        [JsonProperty("mPowerConsumptionExponent")]
        public string MPowerConsumptionExponent { get; set; }

        [JsonProperty("mProductionBoostPowerConsumptionExponent")]
        public string MProductionBoostPowerConsumptionExponent { get; set; }

        [JsonProperty("mDoesHaveShutdownAnimation")]
        public string MDoesHaveShutdownAnimation { get; set; }

        [JsonProperty("mOnHasPowerChanged")]
        public string MOnHasPowerChanged { get; set; }

        [JsonProperty("mOnHasProductionChanged")]
        public string MOnHasProductionChanged { get; set; }

        [JsonProperty("mOnHasStandbyChanged")]
        public string MOnHasStandbyChanged { get; set; }

        [JsonProperty("mOnPendingPotentialChanged")]
        public string MOnPendingPotentialChanged { get; set; }

        [JsonProperty("mOnPendingProductionBoostChanged")]
        public string MOnPendingProductionBoostChanged { get; set; }

        [JsonProperty("mOnCurrentProductivityChanged")]
        public string MOnCurrentProductivityChanged { get; set; }

        [JsonProperty("mMinimumProducingTime")]
        public string MMinimumProducingTime { get; set; }

        [JsonProperty("mMinimumStoppedTime")]
        public string MMinimumStoppedTime { get; set; }

        [JsonProperty("mCanEverMonitorProductivity")]
        public string MCanEverMonitorProductivity { get; set; }

        [JsonProperty("mCanChangePotential")]
        public string MCanChangePotential { get; set; }

        [JsonProperty("mCanChangeProductionBoost")]
        public string MCanChangeProductionBoost { get; set; }

        [JsonProperty("mMinPotential")]
        public string MMinPotential { get; set; }

        [JsonProperty("mMaxPotential")]
        public string MMaxPotential { get; set; }

        [JsonProperty("mBaseProductionBoost")]
        public string MBaseProductionBoost { get; set; }

        [JsonProperty("mPotentialShardSlots")]
        public string MPotentialShardSlots { get; set; }

        [JsonProperty("mProductionShardSlotSize")]
        public string MProductionShardSlotSize { get; set; }

        [JsonProperty("mProductionShardBoostMultiplier")]
        public string MProductionShardBoostMultiplier { get; set; }

        [JsonProperty("mFluidStackSizeDefault")]
        public string MFluidStackSizeDefault { get; set; }

        [JsonProperty("mFluidStackSizeMultiplier")]
        public string MFluidStackSizeMultiplier { get; set; }

        [JsonProperty("mHasInventoryPotential")]
        public string MHasInventoryPotential { get; set; }

        [JsonProperty("mIsTickRateManaged")]
        public string MIsTickRateManaged { get; set; }

        [JsonProperty("mEffectUpdateInterval")]
        public string MEffectUpdateInterval { get; set; }

        [JsonProperty("mDefaultProductivityMeasurementDuration")]
        public string MDefaultProductivityMeasurementDuration { get; set; }

        [JsonProperty("mLastProductivityMeasurementProduceDuration")]
        public string MLastProductivityMeasurementProduceDuration { get; set; }

        [JsonProperty("mLastProductivityMeasurementDuration")]
        public string MLastProductivityMeasurementDuration { get; set; }

        [JsonProperty("mCurrentProductivityMeasurementProduceDuration")]
        public string MCurrentProductivityMeasurementProduceDuration { get; set; }

        [JsonProperty("mCurrentProductivityMeasurementDuration")]
        public string MCurrentProductivityMeasurementDuration { get; set; }

        [JsonProperty("mProductivityMonitorEnabled")]
        public string MProductivityMonitorEnabled { get; set; }

        [JsonProperty("mOverridePotentialShardSlots")]
        public string MOverridePotentialShardSlots { get; set; }

        [JsonProperty("mOverrideProductionShardSlotSize")]
        public string MOverrideProductionShardSlotSize { get; set; }

        [JsonProperty("mAddToSignificanceManager")]
        public string MAddToSignificanceManager { get; set; }

        [JsonProperty("mAlienOverClockingParticleEffects")]
        public string MAlienOverClockingParticleEffects { get; set; }

        [JsonProperty("mCachedSkeletalMeshes")]
        public string MCachedSkeletalMeshes { get; set; }

        [JsonProperty("mSignificanceRange")]
        public string MSignificanceRange { get; set; }

        [JsonProperty("mTickExponent")]
        public string MTickExponent { get; set; }

        [JsonProperty("mOccupiedText")]
        public string MOccupiedText { get; set; }

        [JsonProperty("Tier")]
        public string Tier { get; set; }

        [JsonProperty("mFirstSwingMontageList")]
        public string MFirstSwingMontageList { get; set; }

        [JsonProperty("mSecondSwingMontageList")]
        public string MSecondSwingMontageList { get; set; }

        [JsonProperty("mDamageTypes")]
        public string MDamageTypes { get; set; }

        [JsonProperty("mSecondSwingMinDelay")]
        public string MSecondSwingMinDelay { get; set; }

        [JsonProperty("mSecondSwingMaxDelay")]
        public string MSecondSwingMaxDelay { get; set; }

        [JsonProperty("mSecondSwingUseCoolDown")]
        public string MSecondSwingUseCoolDown { get; set; }

        [JsonProperty("mAttackDistance")]
        public string MAttackDistance { get; set; }

        [JsonProperty("mAttackSweepRadius")]
        public string MAttackSweepRadius { get; set; }

        [JsonProperty("mSelectedPoleVersion")]
        public string MSelectedPoleVersion { get; set; }

        [JsonProperty("mPoleVariations")]
        public string MPoleVariations { get; set; }

        [JsonProperty("mCustomSkins")]
        public string MCustomSkins { get; set; }

        [JsonProperty("mItemMeshMap")]
        public string MItemMeshMap { get; set; }

        [JsonProperty("mSplineData")]
        public string MSplineData { get; set; }

        [JsonProperty("mSpeed")]
        public string MSpeed { get; set; }

        [JsonProperty("mItems")]
        public string MItems { get; set; }

        [JsonProperty("mConveyorChainFlags")]
        public string MConveyorChainFlags { get; set; }

        [JsonProperty("mChainSegmentIndex")]
        public string MChainSegmentIndex { get; set; }

        [JsonProperty("mAttachedThroughputMonitors")]
        public string MAttachedThroughputMonitors { get; set; }

        [JsonProperty("mMaxPowerTowerLength")]
        public string MMaxPowerTowerLength { get; set; }

        [JsonProperty("mLengthPerCost")]
        public string MLengthPerCost { get; set; }

        [JsonProperty("mConnectionLocations")]
        public string MConnectionLocations { get; set; }

        [JsonProperty("mWireInstances")]
        public string MWireInstances { get; set; }

        [JsonProperty("mCachedLength")]
        public string MCachedLength { get; set; }

        [JsonProperty("mPowerConnections")]
        public string MPowerConnections { get; set; }

        [JsonProperty("mPowerPoleType")]
        public string MPowerPoleType { get; set; }

        [JsonProperty("mPowerTowerWireMaxLength")]
        public string MPowerTowerWireMaxLength { get; set; }

        [JsonProperty("mHasPower")]
        public string MHasPower { get; set; }

        [JsonProperty("mType")]
        public string MType { get; set; }

        [JsonProperty("mStatisticGameplayTag")]
        public string MStatisticGameplayTag { get; set; }

        [JsonProperty("mTechTier")]
        public string MTechTier { get; set; }

        [JsonProperty("mCost")]
        public string MCost { get; set; }

        [JsonProperty("mTimeToComplete")]
        public string MTimeToComplete { get; set; }

        [JsonProperty("mRelevantShopSchematics")]
        public string MRelevantShopSchematics { get; set; }

        [JsonProperty("mIsPlayerSpecific")]
        public string MIsPlayerSpecific { get; set; }

        [JsonProperty("mUnlocks")]
        public List<MUnlock> MUnlocks { get; set; }

        [JsonProperty("mSchematicIcon")]
        public string MSchematicIcon { get; set; }

        [JsonProperty("mSmallSchematicIcon")]
        public string MSmallSchematicIcon { get; set; }

        [JsonProperty("mSchematicDependencies")]
        public List<MSchematicDependency> MSchematicDependencies { get; set; }

        [JsonProperty("mDependenciesBlocksSchematicAccess")]
        public string MDependenciesBlocksSchematicAccess { get; set; }

        [JsonProperty("mHiddenUntilDependenciesMet")]
        public string MHiddenUntilDependenciesMet { get; set; }

        [JsonProperty("mSchematicUnlockTag")]
        public string MSchematicUnlockTag { get; set; }

        [JsonProperty("mIncludeInBuilds")]
        public string MIncludeInBuilds { get; set; }

        [JsonProperty("mUnlockName")]
        public string MUnlockName { get; set; }

        [JsonProperty("mUnlockDescription")]
        public string MUnlockDescription { get; set; }

        [JsonProperty("mUnlockIconBig")]
        public string MUnlockIconBig { get; set; }

        [JsonProperty("mUnlockIconSmall")]
        public string MUnlockIconSmall { get; set; }

        [JsonProperty("mUnlockIconCategory")]
        public string MUnlockIconCategory { get; set; }

        [JsonProperty("mIsSupport")]
        public string MIsSupport { get; set; }

        [JsonProperty("JumpForceCharacter")]
        public string JumpForceCharacter { get; set; }

        [JsonProperty("JumpForcePhysics")]
        public string JumpForcePhysics { get; set; }

        [JsonProperty("mDampeningFactor")]
        public string MDampeningFactor { get; set; }

        [JsonProperty("mPlayerList")]
        public string MPlayerList { get; set; }

        [JsonProperty("mDisableSnapOn")]
        public string MDisableSnapOn { get; set; }

        [JsonProperty("m_SFXSockets")]
        public string MSFXSockets { get; set; }

        [JsonProperty("m_CurrentPotential")]
        public string MCurrentPotential { get; set; }

        [JsonProperty("mFuelClasses")]
        public string MFuelClasses { get; set; }

        [JsonProperty("mDefaultFuelClasses")]
        public string MDefaultFuelClasses { get; set; }

        [JsonProperty("mFuel")]
        public List<MFuel> MFuel { get; set; }

        [JsonProperty("mAvailableFuelClasses")]
        public string MAvailableFuelClasses { get; set; }

        [JsonProperty("mFuelClassesInInventory")]
        public string MFuelClassesInInventory { get; set; }

        [JsonProperty("mFuelLoadAmount")]
        public string MFuelLoadAmount { get; set; }

        [JsonProperty("mRequiresSupplementalResource")]
        public string MRequiresSupplementalResource { get; set; }

        [JsonProperty("mSupplementalLoadAmount")]
        public string MSupplementalLoadAmount { get; set; }

        [JsonProperty("mSupplementalToPowerRatio")]
        public string MSupplementalToPowerRatio { get; set; }

        [JsonProperty("mIsFullBlast")]
        public string MIsFullBlast { get; set; }

        [JsonProperty("mCachedInputConnections")]
        public string MCachedInputConnections { get; set; }

        [JsonProperty("mCachedPipeInputConnections")]
        public string MCachedPipeInputConnections { get; set; }

        [JsonProperty("mPowerProduction")]
        public string MPowerProduction { get; set; }

        [JsonProperty("mLoadPercentage")]
        public string MLoadPercentage { get; set; }

        [JsonProperty("mRTPCInterval")]
        public string MRTPCInterval { get; set; }

        [JsonProperty("mCachedLoadPercentage")]
        public string MCachedLoadPercentage { get; set; }

        [JsonProperty("mVerticalAngle")]
        public string MVerticalAngle { get; set; }

        [JsonProperty("mSupportMeshInstanceData")]
        public string MSupportMeshInstanceData { get; set; }

        [JsonProperty("mRadius")]
        public string MRadius { get; set; }

        [JsonProperty("mFlowLimit")]
        public string MFlowLimit { get; set; }

        [JsonProperty("mFlowIndicatorMinimumPipeLength")]
        public string MFlowIndicatorMinimumPipeLength { get; set; }

        [JsonProperty("mSoundSplineComponentEmitterInterval")]
        public string MSoundSplineComponentEmitterInterval { get; set; }

        [JsonProperty("mPipeConnections")]
        public string MPipeConnections { get; set; }

        [JsonProperty("mFluidBox")]
        public string MFluidBox { get; set; }

        [JsonProperty("mMaxIndicatorTurnAngle")]
        public string MMaxIndicatorTurnAngle { get; set; }

        [JsonProperty("mIgnoreActorsForIndicator")]
        public string MIgnoreActorsForIndicator { get; set; }

        [JsonProperty("mFluidNames")]
        public string MFluidNames { get; set; }

        [JsonProperty("mCurrentFluid")]
        public string MCurrentFluid { get; set; }

        [JsonProperty("mLastContentForSound")]
        public string MLastContentForSound { get; set; }

        [JsonProperty("mLastFlowForSound")]
        public string MLastFlowForSound { get; set; }

        [JsonProperty("mLastElapsedTime")]
        public string MLastElapsedTime { get; set; }

        [JsonProperty("mLastFlowForSoundUpdateThreshold")]
        public string MLastFlowForSoundUpdateThreshold { get; set; }

        [JsonProperty("mRattleLimit")]
        public string MRattleLimit { get; set; }

        [JsonProperty("mIsRattling")]
        public string MIsRattling { get; set; }

        [JsonProperty("mUpdateSoundsHandle")]
        public string MUpdateSoundsHandle { get; set; }

        [JsonProperty("mUpdateSoundsTimerInterval")]
        public string MUpdateSoundsTimerInterval { get; set; }

        [JsonProperty("mSnappedPassthroughs")]
        public string MSnappedPassthroughs { get; set; }

        [JsonProperty("mFluidBoxVolume")]
        public string MFluidBoxVolume { get; set; }

        [JsonProperty("mLastFlowUpdate")]
        public string MLastFlowUpdate { get; set; }

        [JsonProperty("mUpdateFlowTime")]
        public string MUpdateFlowTime { get; set; }

        [JsonProperty("mAnimSpeed")]
        public string MAnimSpeed { get; set; }

        [JsonProperty("mLastFlowValue")]
        public string MLastFlowValue { get; set; }

        [JsonProperty("mTimeScaleOffset")]
        public string MTimeScaleOffset { get; set; }

        [JsonProperty("mIsPipePumpPlaying")]
        public string MIsPipePumpPlaying { get; set; }

        [JsonProperty("mIsExceedingHeadLift")]
        public string MIsExceedingHeadLift { get; set; }

        [JsonProperty("mCurrentAudioHeadLift")]
        public string MCurrentAudioHeadLift { get; set; }

        [JsonProperty("mMaxPressure")]
        public string MMaxPressure { get; set; }

        [JsonProperty("mDesignPressure")]
        public string MDesignPressure { get; set; }

        [JsonProperty("mDefaultFlowLimit")]
        public string MDefaultFlowLimit { get; set; }

        [JsonProperty("mUserFlowLimit")]
        public string MUserFlowLimit { get; set; }

        [JsonProperty("mMinimumFlowPercentForStandby")]
        public string MMinimumFlowPercentForStandby { get; set; }

        [JsonProperty("mIndicatorData")]
        public string MIndicatorData { get; set; }

        [JsonProperty("mUpdateAudioFlowTime")]
        public string MUpdateAudioFlowTime { get; set; }

        [JsonProperty("mPistonAudioTimer")]
        public string MPistonAudioTimer { get; set; }

        [JsonProperty("mStackingHeight")]
        public string MStackingHeight { get; set; }

        [JsonProperty("mStorageCapacity")]
        public string MStorageCapacity { get; set; }

        [JsonProperty("mWaterpumpTimeline_RTPC_B8FA6F944E717E3B7A286E84901F620E")]
        public string MWaterpumpTimelineRTPCB8FA6F944E717E3B7A286E84901F620E { get; set; }

        [JsonProperty("mWaterpumpTimeline__Direction_B8FA6F944E717E3B7A286E84901F620E")]
        public string MWaterpumpTimelineDirectionB8FA6F944E717E3B7A286E84901F620E { get; set; }

        [JsonProperty("HasLostSignificance")]
        public string HasLostSignificance { get; set; }

        [JsonProperty("mMinimumDepthForPlacement")]
        public string MMinimumDepthForPlacement { get; set; }

        [JsonProperty("mDepthTraceOriginOffset")]
        public string MDepthTraceOriginOffset { get; set; }

        [JsonProperty("mExtractStartupTime")]
        public string MExtractStartupTime { get; set; }

        [JsonProperty("mExtractStartupTimer")]
        public string MExtractStartupTimer { get; set; }

        [JsonProperty("mExtractCycleTime")]
        public string MExtractCycleTime { get; set; }

        [JsonProperty("mItemsPerCycle")]
        public string MItemsPerCycle { get; set; }

        [JsonProperty("mPipeOutputConnections")]
        public string MPipeOutputConnections { get; set; }

        [JsonProperty("mAllowedResourceForms")]
        public string MAllowedResourceForms { get; set; }

        [JsonProperty("mOnlyAllowCertainResources")]
        public string MOnlyAllowCertainResources { get; set; }

        [JsonProperty("mAllowedResources")]
        public string MAllowedResources { get; set; }

        [JsonProperty("mExtractorTypeName")]
        public string MExtractorTypeName { get; set; }

        [JsonProperty("mTryFindMissingResource")]
        public string MTryFindMissingResource { get; set; }

        [JsonProperty("IsAnimationProducing")]
        public string IsAnimationProducing { get; set; }

        [JsonProperty("EnableTickGrinder")]
        public string EnableTickGrinder { get; set; }

        [JsonProperty("EnableTickEngine")]
        public string EnableTickEngine { get; set; }

        [JsonProperty("mGrinderInterpDuration")]
        public string MGrinderInterpDuration { get; set; }

        [JsonProperty("mEngineInterpDuration")]
        public string MEngineInterpDuration { get; set; }

        [JsonProperty("mProcessingTime")]
        public string MProcessingTime { get; set; }

        [JsonProperty("mProducingTimer")]
        public string MProducingTimer { get; set; }

        [JsonProperty("mShopInventoryDefaultSize")]
        public string MShopInventoryDefaultSize { get; set; }

        [JsonProperty("mScannerCycleLeftMontage")]
        public string MScannerCycleLeftMontage { get; set; }

        [JsonProperty("mScannerCycleRightMontage")]
        public string MScannerCycleRightMontage { get; set; }

        [JsonProperty("mBeepDelayMax")]
        public string MBeepDelayMax { get; set; }

        [JsonProperty("mBeepDelayMin")]
        public string MBeepDelayMin { get; set; }

        [JsonProperty("mDetectionRange")]
        public string MDetectionRange { get; set; }

        [JsonProperty("mUpdateClosestObjectTime")]
        public string MUpdateClosestObjectTime { get; set; }

        [JsonProperty("mClosestObject")]
        public string MClosestObject { get; set; }

        [JsonProperty("mClosestObjectInScanRange")]
        public string MClosestObjectInScanRange { get; set; }

        [JsonProperty("mNormalizedDistanceToClosestObject")]
        public string MNormalizedDistanceToClosestObject { get; set; }

        [JsonProperty("mAngleToClosestObject")]
        public string MAngleToClosestObject { get; set; }

        [JsonProperty("IsPowered")]
        public string IsPowered { get; set; }

        [JsonProperty("IsAnimProducing")]
        public string IsAnimProducing { get; set; }

        [JsonProperty("mEstimatedMininumPowerConsumption")]
        public string MEstimatedMininumPowerConsumption { get; set; }

        [JsonProperty("mEstimatedMaximumPowerConsumption")]
        public string MEstimatedMaximumPowerConsumption { get; set; }

        [JsonProperty("mCurrentRecipeChanged")]
        public string MCurrentRecipeChanged { get; set; }

        [JsonProperty("mManufacturingSpeed")]
        public string MManufacturingSpeed { get; set; }

        [JsonProperty("mFactoryInputConnections")]
        public string MFactoryInputConnections { get; set; }

        [JsonProperty("mPipeInputConnections")]
        public string MPipeInputConnections { get; set; }

        [JsonProperty("mFactoryOutputConnections")]
        public string MFactoryOutputConnections { get; set; }

        [JsonProperty("mSequenceDuration")]
        public string MSequenceDuration { get; set; }

        [JsonProperty("mLightningTimer")]
        public string MLightningTimer { get; set; }

        [JsonProperty("mGameTimeAtProducing")]
        public string MGameTimeAtProducing { get; set; }

        [JsonProperty("mCurrentProducingSeekTime")]
        public string MCurrentProducingSeekTime { get; set; }

        [JsonProperty("mStartVector_VFX_Small_Start")]
        public string MStartVectorVFXSmallStart { get; set; }

        [JsonProperty("mStartVector_VFX_Small_End")]
        public string MStartVectorVFXSmallEnd { get; set; }

        [JsonProperty("mStartVector_VFX_Medium_Start")]
        public string MStartVectorVFXMediumStart { get; set; }

        [JsonProperty("mStartVector_VFX_Medium_End")]
        public string MStartVectorVFXMediumEnd { get; set; }

        [JsonProperty("mStartVector_VFX_Large_Start")]
        public string MStartVectorVFXLargeStart { get; set; }

        [JsonProperty("mStartVector_VFX_Large_End")]
        public string MStartVectorVFXLargeEnd { get; set; }

        [JsonProperty("mPowerShardType")]
        public string MPowerShardType { get; set; }

        [JsonProperty("mExtraPotential")]
        public string MExtraPotential { get; set; }

        [JsonProperty("mExtraProductionBoost")]
        public string MExtraProductionBoost { get; set; }

        [JsonProperty("mParticleMap")]
        public string MParticleMap { get; set; }

        [JsonProperty("mCanPlayAfterStartUpStopped")]
        public string MCanPlayAfterStartUpStopped { get; set; }

        [JsonProperty("SAMReference")]
        public string SAMReference { get; set; }

        [JsonProperty("CanPlayAfterStartUpStopped")]
        public string CanPlayAfterStartUpStopped { get; set; }

        [JsonProperty("InternalStartUpTimer")]
        public string InternalStartUpTimer { get; set; }

        [JsonProperty("mInternalMiningState_0")]
        public string MInternalMiningState0 { get; set; }

        [JsonProperty("mToggleMiningStateHandle_0")]
        public string MToggleMiningStateHandle0 { get; set; }

        [JsonProperty("mMinimumDrillTime_0")]
        public string MMinimumDrillTime0 { get; set; }

        [JsonProperty("mMaximumDrillTime_0")]
        public string MMaximumDrillTime0 { get; set; }

        [JsonProperty("mProductionEffectsRunning")]
        public string MProductionEffectsRunning { get; set; }

        [JsonProperty("bIsPendingToKillVfx")]
        public string BIsPendingToKillVfx { get; set; }

        //[JsonProperty("mCurrentColor_VFX")]
        //public string MCurrentColorVFX { get; set; }

        [JsonProperty("CurrentPackagingMode")]
        public string CurrentPackagingMode { get; set; }

        [JsonProperty("mStoppedProducingAnimationSounds")]
        public string MStoppedProducingAnimationSounds { get; set; }

        [JsonProperty("mStoppedAkComponents")]
        public string MStoppedAkComponents { get; set; }

        [JsonProperty("mSocketStoppedAkComponents")]
        public string MSocketStoppedAkComponents { get; set; }

        //[JsonProperty("mCurrentColorVFX")]
        //public string MCurrentColorVFX { get; set; }

        [JsonProperty("m_NotifyNameREferences")]
        public string MNotifyNameREferences { get; set; }

        [JsonProperty("mColor")]
        public string MColor { get; set; }

        [JsonProperty("mIsRadioActive")]
        public string MIsRadioActive { get; set; }

        [JsonProperty("mAOAttenuationScalingFactor")]
        public string MAOAttenuationScalingFactor { get; set; }

        [JsonProperty("mAOLayerZOffset")]
        public string MAOLayerZOffset { get; set; }

        [JsonProperty("RTPC_AO_VolumeDB")]
        public string RTPCAOVolumeDB { get; set; }

        [JsonProperty("RTPC_AO_HighpassValue")]
        public string RTPCAOHighpassValue { get; set; }

        [JsonProperty("RTPC_AO_Pitch")]
        public string RTPCAOPitch { get; set; }

        [JsonProperty("mIsPendingToKillVFX")]
        public string MIsPendingToKillVFX { get; set; }

        [JsonProperty("mCachedCurrentPotential")]
        public string MCachedCurrentPotential { get; set; }

        [JsonProperty("mSpentFuelClass")]
        public string MSpentFuelClass { get; set; }

        [JsonProperty("mAmountOfWaste")]
        public string MAmountOfWaste { get; set; }

        [JsonProperty("mWasteLeftFromCurrentFuel")]
        public string MWasteLeftFromCurrentFuel { get; set; }

        [JsonProperty("mCurrentGeneratorNuclearWarning")]
        public string MCurrentGeneratorNuclearWarning { get; set; }

        [JsonProperty("CurrentPotentialChangedDelegate")]
        public string CurrentPotentialChangedDelegate { get; set; }

        [JsonProperty("ConnectedExtractorCountChangedDelegate")]
        public string ConnectedExtractorCountChangedDelegate { get; set; }

        [JsonProperty("mActivationStartupTime")]
        public string MActivationStartupTime { get; set; }

        [JsonProperty("mActivationStartupTimer")]
        public string MActivationStartupTimer { get; set; }

        [JsonProperty("mSatelliteActivationComplete")]
        public string MSatelliteActivationComplete { get; set; }

        [JsonProperty("mSatelliteNodeCount")]
        public string MSatelliteNodeCount { get; set; }

        [JsonProperty("mConnectedExtractorCount")]
        public string MConnectedExtractorCount { get; set; }

        [JsonProperty("mDefaultPotentialExtractionPerMinute")]
        public string MDefaultPotentialExtractionPerMinute { get; set; }

        [JsonProperty("m_DockingStates")]
        public string MDockingStates { get; set; }

        [JsonProperty("m_OffsetTime")]
        public string MOffsetTime { get; set; }

        [JsonProperty("mDroneDockingStartLocationLocal")]
        public string MDroneDockingStartLocationLocal { get; set; }

        [JsonProperty("mDroneDockingLocationLocal")]
        public string MDroneDockingLocationLocal { get; set; }

        [JsonProperty("mDroneDockingQueue")]
        public string MDroneDockingQueue { get; set; }

        [JsonProperty("mStationHasDronesInQueue")]
        public string MStationHasDronesInQueue { get; set; }

        [JsonProperty("mItemTransferringStage")]
        public string MItemTransferringStage { get; set; }

        [JsonProperty("mTransferProgress")]
        public string MTransferProgress { get; set; }

        [JsonProperty("mTransferSpeed")]
        public string MTransferSpeed { get; set; }

        [JsonProperty("mStackTransferSize")]
        public string MStackTransferSize { get; set; }

        [JsonProperty("mDroneQueueRadius")]
        public string MDroneQueueRadius { get; set; }

        [JsonProperty("mDroneQueueSeparationRadius")]
        public string MDroneQueueSeparationRadius { get; set; }

        [JsonProperty("mDroneQueueVerticalSeparation")]
        public string MDroneQueueVerticalSeparation { get; set; }

        [JsonProperty("mTripPowerCost")]
        public string MTripPowerCost { get; set; }

        [JsonProperty("mTripPowerPerMeterCost")]
        public string MTripPowerPerMeterCost { get; set; }

        [JsonProperty("mTripInformationSampleCount")]
        public string MTripInformationSampleCount { get; set; }

        [JsonProperty("mStorageSizeX")]
        public string MStorageSizeX { get; set; }

        [JsonProperty("mStorageSizeY")]
        public string MStorageSizeY { get; set; }

        [JsonProperty("mFuelStorageSizeX")]
        public string MFuelStorageSizeX { get; set; }

        [JsonProperty("mFuelStorageSizeY")]
        public string MFuelStorageSizeY { get; set; }

        [JsonProperty("mMapText")]
        public string MMapText { get; set; }

        [JsonProperty("mInventorySize")]
        public string MInventorySize { get; set; }

        [JsonProperty("mFuelConsumption")]
        public string MFuelConsumption { get; set; }

        [JsonProperty("mMeshHeight")]
        public string MMeshHeight { get; set; }

        [JsonProperty("mOutputMeshDisplayMode")]
        public string MOutputMeshDisplayMode { get; set; }

        [JsonProperty("mInputMeshDisplayMode")]
        public string MInputMeshDisplayMode { get; set; }

        [JsonProperty("mTopTransform")]
        public string MTopTransform { get; set; }

        [JsonProperty("mIsReversed")]
        public string MIsReversed { get; set; }

        [JsonProperty("mIsBeltUsingInputRotation")]
        public string MIsBeltUsingInputRotation { get; set; }

        [JsonProperty("mFlipMeshOnReverse")]
        public string MFlipMeshOnReverse { get; set; }

        [JsonProperty("mOpposingConnectionClearance")]
        public string MOpposingConnectionClearance { get; set; }

        [JsonProperty("OffsetFromPortalSurface")]
        public string OffsetFromPortalSurface { get; set; }

        [JsonProperty("HeatingUpPlaying ID")]
        public string HeatingUpPlayingID { get; set; }

        [JsonProperty("LightningEndLoc")]
        public string LightningEndLoc { get; set; }

        [JsonProperty("LightningStartLoc")]
        public string LightningStartLoc { get; set; }

        [JsonProperty("LightningStartVector")]
        public string LightningStartVector { get; set; }

        [JsonProperty("LightningEndVector")]
        public string LightningEndVector { get; set; }

        //[JsonProperty("LightningStartSocket")]
        //public string LightningStartSocket { get; set; }

        //[JsonProperty("LightningEndSocket")]
        //public string LightningEndSocket { get; set; }

        [JsonProperty("mTeleportationCompleteRepNotify")]
        public string MTeleportationCompleteRepNotify { get; set; }

        [JsonProperty("mTeleportationBeginRepNotify")]
        public string MTeleportationBeginRepNotify { get; set; }

        [JsonProperty("In Value")]
        public string InValue { get; set; }

        [JsonProperty("PortalActive")]
        public string PortalActive { get; set; }

        [JsonProperty("PortalFluctuation")]
        public string PortalFluctuation { get; set; }

        [JsonProperty("mFuelSlotSize")]
        public string MFuelSlotSize { get; set; }

        [JsonProperty("mMinFuelToStartProducing")]
        public string MMinFuelToStartProducing { get; set; }

        [JsonProperty("mMinFuelToStartProducingAfterAbruptStop")]
        public string MMinFuelToStartProducingAfterAbruptStop { get; set; }

        [JsonProperty("mPortalDisconnectedCooldownTime")]
        public string MPortalDisconnectedCooldownTime { get; set; }

        [JsonProperty("mHeatUpComplete")]
        public string MHeatUpComplete { get; set; }

        [JsonProperty("mHeatUpCycleTime")]
        public string MHeatUpCycleTime { get; set; }

        [JsonProperty("mCurrentHeatUpProgress")]
        public string MCurrentHeatUpProgress { get; set; }

        [JsonProperty("mCurrentProductionProgress")]
        public string MCurrentProductionProgress { get; set; }

        [JsonProperty("mLinkedPortalDisconnectCooldownTimeLeft")]
        public string MLinkedPortalDisconnectCooldownTimeLeft { get; set; }

        [JsonProperty("mCurrentProductionCycleTime")]
        public string MCurrentProductionCycleTime { get; set; }

        [JsonProperty("mTrippedProductionStop")]
        public string MTrippedProductionStop { get; set; }

        [JsonProperty("mCachedHasEnoughFuelForProduce")]
        public string MCachedHasEnoughFuelForProduce { get; set; }

        [JsonProperty("mOnLinkedPortalChanged")]
        public string MOnLinkedPortalChanged { get; set; }

        [JsonProperty("mOnPortalTraversableChanged")]
        public string MOnPortalTraversableChanged { get; set; }

        [JsonProperty("mOnHeatUpStateChanged")]
        public string MOnHeatUpStateChanged { get; set; }

        [JsonProperty("mMaxPortalTravelTime")]
        public string MMaxPortalTravelTime { get; set; }

        [JsonProperty("mIsPortalTraversable")]
        public string MIsPortalTraversable { get; set; }

        [JsonProperty("mActorRepresentationColor")]
        public string MActorRepresentationColor { get; set; }

        [JsonProperty("mActorRepresentationViewDistance")]
        public string MActorRepresentationViewDistance { get; set; }

        [JsonProperty("mActorRepresentationFogOfWarRevealRadius")]
        public string MActorRepresentationFogOfWarRevealRadius { get; set; }

        [JsonProperty("mPortalName")]
        public string MPortalName { get; set; }

        [JsonProperty("mTeleportPowerConsumptionTimeLeft")]
        public string MTeleportPowerConsumptionTimeLeft { get; set; }

        [JsonProperty("mTeleportPowerConsumption")]
        public string MTeleportPowerConsumption { get; set; }

        [JsonProperty("mCachedFactoryTickData")]
        public string MCachedFactoryTickData { get; set; }

        [JsonProperty("mLastEditedBy")]
        public string MLastEditedBy { get; set; }

        //[JsonProperty("Lightning End Socket")]
        //public string LightningEndSocket { get; set; }

        //[JsonProperty("Lightning Start Socket")]
        //public string LightningStartSocket { get; set; }

        [JsonProperty("mWasHeatingUpLastTick")]
        public string MWasHeatingUpLastTick { get; set; }

        [JsonProperty("mTerminalDistanceFromEdge")]
        public string MTerminalDistanceFromEdge { get; set; }

        [JsonProperty("mTerminalHalfDepth")]
        public string MTerminalHalfDepth { get; set; }

        [JsonProperty("mDimensions")]
        public string MDimensions { get; set; }

        [JsonProperty("OnRecordDataChanged")]
        public string OnRecordDataChanged { get; set; }

        [JsonProperty("OnBlueprintCostChanged")]
        public string OnBlueprintCostChanged { get; set; }

        [JsonProperty("mCurrentCost")]
        public string MCurrentCost { get; set; }

        [JsonProperty("mBuildables")]
        public string MBuildables { get; set; }

        [JsonProperty("mCurrentRecordData")]
        public string MCurrentRecordData { get; set; }

        [JsonProperty("mIsDismantlingAll")]
        public string MIsDismantlingAll { get; set; }

        [JsonProperty("mCurrentPlayerVelocity_SFX")]
        public string MCurrentPlayerVelocitySFX { get; set; }

        [JsonProperty("mCurrentMouseDelta_SFX")]
        public string MCurrentMouseDeltaSFX { get; set; }

        [JsonProperty("mLowBatteryWarningActive_SFX")]
        public string MLowBatteryWarningActiveSFX { get; set; }

        [JsonProperty("mPropellerVFX")]
        public string MPropellerVFX { get; set; }

        [JsonProperty("ConnectionLocationUpdatedDelegate")]
        public string ConnectionLocationUpdatedDelegate { get; set; }

        [JsonProperty("ConnectionStatusUpdatedDelegate")]
        public string ConnectionStatusUpdatedDelegate { get; set; }

        [JsonProperty("HoverModeChangedDelegate")]
        public string HoverModeChangedDelegate { get; set; }

        [JsonProperty("RangeWarningToggleDelegate")]
        public string RangeWarningToggleDelegate { get; set; }

        [JsonProperty("mHoverSpeed")]
        public string MHoverSpeed { get; set; }

        [JsonProperty("mHoverAccelerationSpeed")]
        public string MHoverAccelerationSpeed { get; set; }

        [JsonProperty("mHoverSprintMultiplier")]
        public string MHoverSprintMultiplier { get; set; }

        [JsonProperty("mHoverFriction")]
        public string MHoverFriction { get; set; }

        [JsonProperty("mJumpKeyHoldActivationTime")]
        public string MJumpKeyHoldActivationTime { get; set; }

        [JsonProperty("mFallSpeedLimitWhenPowered")]
        public string MFallSpeedLimitWhenPowered { get; set; }

        [JsonProperty("mPowerConnectionSearchRadius")]
        public string MPowerConnectionSearchRadius { get; set; }

        [JsonProperty("mPowerConnectionSearchTickRate")]
        public string MPowerConnectionSearchTickRate { get; set; }

        [JsonProperty("mPowerConnectionDisconnectionTime")]
        public string MPowerConnectionDisconnectionTime { get; set; }

        [JsonProperty("mPowerCapacity")]
        public string MPowerCapacity { get; set; }

        [JsonProperty("mPowerDrainRate")]
        public string MPowerDrainRate { get; set; }

        [JsonProperty("mCurrentPowerLevel")]
        public string MCurrentPowerLevel { get; set; }

        [JsonProperty("mRangeWarningNormalizedDistanceThreshold")]
        public string MRangeWarningNormalizedDistanceThreshold { get; set; }

        [JsonProperty("mDisplayRangeWarning")]
        public string MDisplayRangeWarning { get; set; }

        [JsonProperty("mCurrentHoverMode")]
        public string MCurrentHoverMode { get; set; }

        [JsonProperty("mHasConnection")]
        public string MHasConnection { get; set; }

        [JsonProperty("mShouldAutomaticallyHoverWhenConnected")]
        public string MShouldAutomaticallyHoverWhenConnected { get; set; }

        [JsonProperty("mCrouchHoverCancelTime")]
        public string MCrouchHoverCancelTime { get; set; }

        [JsonProperty("mCharacterUseDistanceWhenActive")]
        public string MCharacterUseDistanceWhenActive { get; set; }

        [JsonProperty("mActiveNoiseFrequency")]
        public string MActiveNoiseFrequency { get; set; }

        [JsonProperty("mCurrentConnectionLocation")]
        public string MCurrentConnectionLocation { get; set; }

        [JsonProperty("mPreviousAspect")]
        public string MPreviousAspect { get; set; }

        [JsonProperty("mOnAspectChangedDelegate")]
        public string MOnAspectChangedDelegate { get; set; }

        [JsonProperty("mOnBlockValidationChangedDelegate")]
        public string MOnBlockValidationChangedDelegate { get; set; }

        [JsonProperty("mDrawDebugVisualState")]
        public string MDrawDebugVisualState { get; set; }

        [JsonProperty("mGuardedConnections")]
        public string MGuardedConnections { get; set; }

        [JsonProperty("mObservedConnections")]
        public string MObservedConnections { get; set; }

        [JsonProperty("mAspect")]
        public string MAspect { get; set; }

        [JsonProperty("mBlockValidation")]
        public string MBlockValidation { get; set; }

        [JsonProperty("mIsPathSignal")]
        public string MIsPathSignal { get; set; }

        [JsonProperty("mIsBiDirectional")]
        public string MIsBiDirectional { get; set; }

        [JsonProperty("mIsLeftHanded")]
        public string MIsLeftHanded { get; set; }

        [JsonProperty("mVisualState")]
        public string MVisualState { get; set; }

        [JsonProperty("mIsEndStop")]
        public string MIsEndStop { get; set; }

        [JsonProperty("mOnTransferRateUpdated")]
        public string MOnTransferRateUpdated { get; set; }

        [JsonProperty("mFreightCargoType")]
        public string MFreightCargoType { get; set; }

        [JsonProperty("mCanUnloadAny")]
        public string MCanUnloadAny { get; set; }

        [JsonProperty("mIsFullUnload")]
        public string MIsFullUnload { get; set; }

        [JsonProperty("mCanLoadAny")]
        public string MCanLoadAny { get; set; }

        [JsonProperty("mIsFullLoad")]
        public string MIsFullLoad { get; set; }

        [JsonProperty("mTimeToCompleteLoad")]
        public string MTimeToCompleteLoad { get; set; }

        [JsonProperty("mTimeToSwapLoadVisibility")]
        public string MTimeToSwapLoadVisibility { get; set; }

        [JsonProperty("mTimeToCompleteUnload")]
        public string MTimeToCompleteUnload { get; set; }

        [JsonProperty("mTimeToSwapUnloadVisibility")]
        public string MTimeToSwapUnloadVisibility { get; set; }

        [JsonProperty("mWaitForConditionUpdatePeriod")]
        public string MWaitForConditionUpdatePeriod { get; set; }

        [JsonProperty("mStorageInputConnections")]
        public string MStorageInputConnections { get; set; }

        [JsonProperty("mDockingRuleSet")]
        public string MDockingRuleSet { get; set; }

        [JsonProperty("mLoadItemFilter")]
        public string MLoadItemFilter { get; set; }

        [JsonProperty("mUnloadItemFilter")]
        public string MUnloadItemFilter { get; set; }

        [JsonProperty("mHasFullyLoadUnloadRule")]
        public string MHasFullyLoadUnloadRule { get; set; }

        [JsonProperty("mDockForDuration")]
        public string MDockForDuration { get; set; }

        [JsonProperty("mMustDockForDuration")]
        public string MMustDockForDuration { get; set; }

        [JsonProperty("mCurrentDockForDuration")]
        public string MCurrentDockForDuration { get; set; }

        [JsonProperty("mHasAnyRelevantStacksToMove")]
        public string MHasAnyRelevantStacksToMove { get; set; }

        [JsonProperty("mAllowDepartureNoValidItemsToTransfer")]
        public string MAllowDepartureNoValidItemsToTransfer { get; set; }

        [JsonProperty("mShouldExecuteLoadOrUnload")]
        public string MShouldExecuteLoadOrUnload { get; set; }

        [JsonProperty("mRanCompleteBeforeNone")]
        public string MRanCompleteBeforeNone { get; set; }

        [JsonProperty("mSwapCargoVisibilityTimerHandle")]
        public string MSwapCargoVisibilityTimerHandle { get; set; }

        [JsonProperty("mTimeSinceLastLoadTransferUpdate")]
        public string MTimeSinceLastLoadTransferUpdate { get; set; }

        [JsonProperty("mTimeSinceLastUnloadTransferUpdate")]
        public string MTimeSinceLastUnloadTransferUpdate { get; set; }

        [JsonProperty("mSmoothedLoadRate")]
        public string MSmoothedLoadRate { get; set; }

        [JsonProperty("mSmoothedUnloadRate")]
        public string MSmoothedUnloadRate { get; set; }

        [JsonProperty("mReplicatedOutflowRate")]
        public string MReplicatedOutflowRate { get; set; }

        [JsonProperty("mReplicatedInflowRate")]
        public string MReplicatedInflowRate { get; set; }

        [JsonProperty("mPlatformConnections")]
        public string MPlatformConnections { get; set; }

        [JsonProperty("mIsOrientationReversed")]
        public string MIsOrientationReversed { get; set; }

        [JsonProperty("mPlatformDockingStatus")]
        public string MPlatformDockingStatus { get; set; }

        [JsonProperty("mSavedDockingStatus")]
        public string MSavedDockingStatus { get; set; }

        [JsonProperty("mDockingSequenceTimerHandle")]
        public string MDockingSequenceTimerHandle { get; set; }

        [JsonProperty("mIdleUpdateTimerHandle")]
        public string MIdleUpdateTimerHandle { get; set; }

        [JsonProperty("mDockWasCancelled")]
        public string MDockWasCancelled { get; set; }

        [JsonProperty("mStationName")]
        public string MStationName { get; set; }

        [JsonProperty("mShouldTeleportHere")]
        public string MShouldTeleportHere { get; set; }

        [JsonProperty("mDockedPlatformList")]
        public string MDockedPlatformList { get; set; }

        [JsonProperty("mCurrentDockedWithRuleSet")]
        public string MCurrentDockedWithRuleSet { get; set; }

        [JsonProperty("FuelTypeDescriptos")]
        public string FuelTypeDescriptos { get; set; }

        [JsonProperty("mOnFuelAmountChanged")]
        public string MOnFuelAmountChanged { get; set; }

        [JsonProperty("mOnBurnPercentChanged")]
        public string MOnBurnPercentChanged { get; set; }

        [JsonProperty("mOnFuelTypeChanged")]
        public string MOnFuelTypeChanged { get; set; }

        [JsonProperty("mDefaultAirControl")]
        public string MDefaultAirControl { get; set; }

        [JsonProperty("mCurrentFuel")]
        public string MCurrentFuel { get; set; }

        [JsonProperty("mIsThrusting")]
        public string MIsThrusting { get; set; }

        [JsonProperty("mAllowedFuelTypes")]
        public string MAllowedFuelTypes { get; set; }

        [JsonProperty("mSelectedFuelType")]
        public string MSelectedFuelType { get; set; }

        [JsonProperty("mCurrentFuelType")]
        public string MCurrentFuelType { get; set; }

        [JsonProperty("mUnlockedFuelTypes")]
        public string MUnlockedFuelTypes { get; set; }

        [JsonProperty("mAvailableFuelTypes")]
        public string MAvailableFuelTypes { get; set; }

        [JsonProperty("mStackHeight")]
        public string MStackHeight { get; set; }

        [JsonProperty("mInventorySizeX")]
        public string MInventorySizeX { get; set; }

        [JsonProperty("mInventorySizeY")]
        public string MInventorySizeY { get; set; }

        [JsonProperty("mWindDirectionFromTurbine")]
        public string MWindDirectionFromTurbine { get; set; }

        [JsonProperty("mIsWindSoundPlaying?")]
        public string MIsWindSoundPlaying { get; set; }

        [JsonProperty("mAudioTimerCounter")]
        public string MAudioTimerCounter { get; set; }

        [JsonProperty("AudioCounterTimer")]
        public string AudioCounterTimer { get; set; }

        [JsonProperty("IsEnginePlaying")]
        public string IsEnginePlaying { get; set; }

        [JsonProperty("mOpeningOffset")]
        public string MOpeningOffset { get; set; }

        [JsonProperty("mInitialMinSpeedFactor")]
        public string MInitialMinSpeedFactor { get; set; }

        [JsonProperty("JunctionPaths")]
        public string JunctionPaths { get; set; }

        [JsonProperty("DefaultPaths")]
        public string DefaultPaths { get; set; }

        [JsonProperty("mBuiltWithPipelineCostMultiplier")]
        public string MBuiltWithPipelineCostMultiplier { get; set; }

        [JsonProperty("m_PreviousBatteryStatus")]
        public string MPreviousBatteryStatus { get; set; }

        [JsonProperty("mCurrentGameTimeSinceStateChange")]
        public string MCurrentGameTimeSinceStateChange { get; set; }

        [JsonProperty("mActivationEventID")]
        public string MActivationEventID { get; set; }

        [JsonProperty("mStatusPrimitiveID")]
        public string MStatusPrimitiveID { get; set; }

        [JsonProperty("mChargePrimitiveID")]
        public string MChargePrimitiveID { get; set; }

        [JsonProperty("mNumExtraCustomizationData")]
        public string MNumExtraCustomizationData { get; set; }

        [JsonProperty("mBatteryStatus")]
        public string MBatteryStatus { get; set; }

        [JsonProperty("mPowerStore")]
        public string MPowerStore { get; set; }

        [JsonProperty("mPowerStoreCapacity")]
        public string MPowerStoreCapacity { get; set; }

        [JsonProperty("mPowerInput")]
        public string MPowerInput { get; set; }

        [JsonProperty("mPowerInputCapacity")]
        public string MPowerInputCapacity { get; set; }

        [JsonProperty("mIndicatorLevelMax")]
        public string MIndicatorLevelMax { get; set; }

        [JsonProperty("mIndicatorLevel")]
        public string MIndicatorLevel { get; set; }

        [JsonProperty("mMaxRealDataDriftTime")]
        public string MMaxRealDataDriftTime { get; set; }

        [JsonProperty("VehicleFuelConsumptionRateChangedDelegate")]
        public string VehicleFuelConsumptionRateChangedDelegate { get; set; }

        [JsonProperty("ItemTransferRateChangedDelegate")]
        public string ItemTransferRateChangedDelegate { get; set; }

        [JsonProperty("MaximumStackTransferRateChangedDelegate")]
        public string MaximumStackTransferRateChangedDelegate { get; set; }

        [JsonProperty("mDockPosition")]
        public string MDockPosition { get; set; }

        [JsonProperty("mMinimumDockingTime")]
        public string MMinimumDockingTime { get; set; }

        [JsonProperty("mFuelInventorySizeX")]
        public string MFuelInventorySizeX { get; set; }

        [JsonProperty("mFuelInventorySizeY")]
        public string MFuelInventorySizeY { get; set; }

        [JsonProperty("mFuelTransferSpeed")]
        public string MFuelTransferSpeed { get; set; }

        [JsonProperty("mForceSignificance")]
        public string MForceSignificance { get; set; }

        [JsonProperty("mVehicleFuelConsumptionRate")]
        public string MVehicleFuelConsumptionRate { get; set; }

        [JsonProperty("mItemTransferRate")]
        public string MItemTransferRate { get; set; }

        [JsonProperty("mMaximumStackTransferRate")]
        public string MMaximumStackTransferRate { get; set; }

        [JsonProperty("mDockingVehicleStatistics")]
        public string MDockingVehicleStatistics { get; set; }

        [JsonProperty("mPowerBankCapacity")]
        public string MPowerBankCapacity { get; set; }

        [JsonProperty("mLaunchPowerCost")]
        public string MLaunchPowerCost { get; set; }

        [JsonProperty("mChargeRateMultiplier")]
        public string MChargeRateMultiplier { get; set; }

        [JsonProperty("mLaunchVelocity")]
        public string MLaunchVelocity { get; set; }

        [JsonProperty("mLaunchAngle")]
        public string MLaunchAngle { get; set; }

        [JsonProperty("mPlayerChainJumpResetTime")]
        public string MPlayerChainJumpResetTime { get; set; }

        [JsonProperty("mHasPowerForLaunch")]
        public string MHasPowerForLaunch { get; set; }

        [JsonProperty("ComponentsToLaunch")]
        public string ComponentsToLaunch { get; set; }

        [JsonProperty("CharactersToLaunch")]
        public string CharactersToLaunch { get; set; }

        [JsonProperty("VehiclesToLaunch")]
        public string VehiclesToLaunch { get; set; }

        [JsonProperty("mTrajectoryData")]
        public string MTrajectoryData { get; set; }

        [JsonProperty("mTrajectoryMeshScale")]
        public string MTrajectoryMeshScale { get; set; }

        [JsonProperty("mTrajectoryMeshRotation")]
        public string MTrajectoryMeshRotation { get; set; }

        [JsonProperty("mDestinationMeshHeightOffset")]
        public string MDestinationMeshHeightOffset { get; set; }

        [JsonProperty("mTrajectorySplineMeshNumPrimitiveDataFloats")]
        public string MTrajectorySplineMeshNumPrimitiveDataFloats { get; set; }

        [JsonProperty("mTrajectorySplineMeshSplineDataSettings")]
        public string MTrajectorySplineMeshSplineDataSettings { get; set; }

        [JsonProperty("mNumArrows")]
        public string MNumArrows { get; set; }

        [JsonProperty("mKillTimer")]
        public string MKillTimer { get; set; }

        [JsonProperty("mTrajectoryGravityMultiplier")]
        public string MTrajectoryGravityMultiplier { get; set; }

        [JsonProperty("mShowTrajectoryCounter")]
        public string MShowTrajectoryCounter { get; set; }

        [JsonProperty("mInterpSawProgress")]
        public string MInterpSawProgress { get; set; }

        [JsonProperty("mCurrentOutputDataSFX")]
        public string MCurrentOutputDataSFX { get; set; }

        [JsonProperty("StartUpToIdleID")]
        public string StartUpToIdleID { get; set; }

        [JsonProperty("mCurrentHasFuel")]
        public string MCurrentHasFuel { get; set; }

        [JsonProperty("mPreviousState")]
        public string MPreviousState { get; set; }

        [JsonProperty("mChainsawEngageMontage")]
        public string MChainsawEngageMontage { get; set; }

        [JsonProperty("mChainsawSawingMontage")]
        public string MChainsawSawingMontage { get; set; }

        [JsonProperty("mChainsawEquipFuelMontage")]
        public string MChainsawEquipFuelMontage { get; set; }

        [JsonProperty("mChainsawEquipNoFuelMontage")]
        public string MChainsawEquipNoFuelMontage { get; set; }

        [JsonProperty("mChainsawEquipStingerMontage")]
        public string MChainsawEquipStingerMontage { get; set; }

        [JsonProperty("mShowAOESelectorUITimer")]
        public string MShowAOESelectorUITimer { get; set; }

        [JsonProperty("EngagePlayingID")]
        public string EngagePlayingID { get; set; }

        [JsonProperty("IdlePlaying ID")]
        public string IdlePlayingID { get; set; }

        [JsonProperty("Playing ID")]
        public string PlayingID { get; set; }

        [JsonProperty("SawingPlayingID")]
        public string SawingPlayingID { get; set; }

        [JsonProperty("CurrentState")]
        public string CurrentState { get; set; }

        [JsonProperty("StateChanged")]
        public string StateChanged { get; set; }

        [JsonProperty("mEnergyConsumption")]
        public string MEnergyConsumption { get; set; }

        [JsonProperty("mSawDownTreeTime")]
        public string MSawDownTreeTime { get; set; }

        [JsonProperty("mCollateralPickupRadius")]
        public string MCollateralPickupRadius { get; set; }

        [JsonProperty("mIsAOEOn")]
        public string MIsAOEOn { get; set; }

        [JsonProperty("mEnergyStored")]
        public string MEnergyStored { get; set; }

        [JsonProperty("mSawingProgress")]
        public string MSawingProgress { get; set; }

        [JsonProperty("mChainsawState")]
        public string MChainsawState { get; set; }

        [JsonProperty("mHealthGain")]
        public decimal? MHealthGain { get; set; }

        [JsonProperty("mCustomHandsMeshScale")]
        public string MCustomHandsMeshScale { get; set; }

        [JsonProperty("mCustomRotation")]
        public string MCustomRotation { get; set; }

        [JsonProperty("mCustomLocation")]
        public string MCustomLocation { get; set; }

        [JsonProperty("Centrifuge_NewTrack_1_BB49BD99478F0FC67F8D7E9A54C7E849")]
        public string CentrifugeNewTrack1BB49BD99478F0FC67F8D7E9A54C7E849 { get; set; }

        [JsonProperty("Centrifuge_NewTrack_0_BB49BD99478F0FC67F8D7E9A54C7E849")]
        public string CentrifugeNewTrack0BB49BD99478F0FC67F8D7E9A54C7E849 { get; set; }

        [JsonProperty("Centrifuge__Direction_BB49BD99478F0FC67F8D7E9A54C7E849")]
        public string CentrifugeDirectionBB49BD99478F0FC67F8D7E9A54C7E849 { get; set; }

        [JsonProperty("mCurrentResearchState")]
        public string MCurrentResearchState { get; set; }

        [JsonProperty("Centrifuge Duration")]
        public string CentrifugeDuration { get; set; }

        [JsonProperty("mCurrentInputIndex")]
        public string MCurrentInputIndex { get; set; }

        [JsonProperty("mSavedDirections")]
        public string MSavedDirections { get; set; }

        [JsonProperty("mHologramOverrides")]
        public string MHologramOverrides { get; set; }

        [JsonProperty("mCurrentOutputIndex")]
        public string MCurrentOutputIndex { get; set; }

        [JsonProperty("mDepth")]
        public string MDepth { get; set; }

        [JsonProperty("mIsFrame")]
        public string MIsFrame { get; set; }

        [JsonProperty("mDisableAttachmentSnapOn")]
        public string MDisableAttachmentSnapOn { get; set; }

        [JsonProperty("mIsDoubleRamp")]
        public string MIsDoubleRamp { get; set; }

        [JsonProperty("mIsRoof")]
        public string MIsRoof { get; set; }

        [JsonProperty("mIsInverted")]
        public string MIsInverted { get; set; }

        [JsonProperty("bigOverlapList")]
        public string BigOverlapList { get; set; }

        [JsonProperty("mCanBeLocked")]
        public string MCanBeLocked { get; set; }

        [JsonProperty("mAnimationRate")]
        public string MAnimationRate { get; set; }

        [JsonProperty("mMovementRate")]
        public string MMovementRate { get; set; }

        [JsonProperty("EasingFunction")]
        public string EasingFunction { get; set; }

        [JsonProperty("BlendExp")]
        public string BlendExp { get; set; }

        [JsonProperty("Steps")]
        public string Steps { get; set; }

        [JsonProperty("bigOverlapList_0")]
        public string BigOverlapList0 { get; set; }

        [JsonProperty("IsDoorOpen")]
        public string IsDoorOpen { get; set; }

        [JsonProperty("IsInProducingAnimState")]
        public string IsInProducingAnimState { get; set; }

        [JsonProperty("LaserSocketNames")]
        public string LaserSocketNames { get; set; }

        [JsonProperty("mVFX_LaserSubtract")]
        public string MVFXLaserSubtract { get; set; }

        [JsonProperty("mVFX_SummerSloop_Loc_Offset")]
        public string MVFXSummerSloopLocOffset { get; set; }

        [JsonProperty("mVFX_SubVector")]
        public string MVFXSubVector { get; set; }

        [JsonProperty("m_Beam_TargetLoc")]
        public string MBeamTargetLoc { get; set; }

        [JsonProperty("mShrine_location")]
        public string MShrineLocation { get; set; }

        [JsonProperty("bFirstRun")]
        public string BFirstRun { get; set; }

        [JsonProperty("mBasePowerProduction")]
        public string MBasePowerProduction { get; set; }

        [JsonProperty("mBaseBoostPercentage")]
        public string MBaseBoostPercentage { get; set; }

        [JsonProperty("mCurrentFuelBoostPercentage")]
        public string MCurrentFuelBoostPercentage { get; set; }

        [JsonProperty("mCurrentFuelDuration")]
        public string MCurrentFuelDuration { get; set; }

        [JsonProperty("mCurrentFuelDurationLeft")]
        public string MCurrentFuelDurationLeft { get; set; }

        [JsonProperty("TimeToExecuteCheckAfterItemAdded")]
        public string TimeToExecuteCheckAfterItemAdded { get; set; }

        [JsonProperty("mUploadTimer")]
        public string MUploadTimer { get; set; }

        [JsonProperty("mTimeToUpload")]
        public string MTimeToUpload { get; set; }

        [JsonProperty("mBoostPercentage")]
        public string MBoostPercentage { get; set; }

        [JsonProperty("mBoostDuration")]
        public string MBoostDuration { get; set; }

        [JsonProperty("mVariablePowerProductionConstant")]
        public string MVariablePowerProductionConstant { get; set; }

        [JsonProperty("mVariablePowerProductionFactor")]
        public string MVariablePowerProductionFactor { get; set; }

        [JsonProperty("mVariablePowerProductionCycleLength")]
        public string MVariablePowerProductionCycleLength { get; set; }

        [JsonProperty("mMinPowerProduction")]
        public string MMinPowerProduction { get; set; }

        [JsonProperty("mMaxPowerProduction")]
        public string MMaxPowerProduction { get; set; }

        [JsonProperty("mVariablePowerProductionCycleOffset")]
        public string MVariablePowerProductionCycleOffset { get; set; }

        [JsonProperty("OnSortRulesChangedDelegate")]
        public string OnSortRulesChangedDelegate { get; set; }

        [JsonProperty("mMaxNumSortRules")]
        public string MMaxNumSortRules { get; set; }

        [JsonProperty("mLastItem")]
        public string MLastItem { get; set; }

        [JsonProperty("mItemToLastOutputMap")]
        public string MItemToLastOutputMap { get; set; }

        [JsonProperty("mLastOutputIndex")]
        public string MLastOutputIndex { get; set; }

        [JsonProperty("mCurrentInventoryIndex")]
        public string MCurrentInventoryIndex { get; set; }

        [JsonProperty("mDistributionTable")]
        public string MDistributionTable { get; set; }

        [JsonProperty("mTextRenderers")]
        public string MTextRenderers { get; set; }

        [JsonProperty("bIsSignificant")]
        public string BIsSignificant { get; set; }

        [JsonProperty("mMaxCharacters")]
        public string MMaxCharacters { get; set; }

        [JsonProperty("mOnPriorityChanged")]
        public string MOnPriorityChanged { get; set; }

        [JsonProperty("mPriority")]
        public string MPriority { get; set; }

        [JsonProperty("mOnIsSwitchOnChanged")]
        public string MOnIsSwitchOnChanged { get; set; }

        [JsonProperty("mOnIsConnectedChanged")]
        public string MOnIsConnectedChanged { get; set; }

        [JsonProperty("mOnBuildingTagChanged")]
        public string MOnBuildingTagChanged { get; set; }

        [JsonProperty("mOnLastEditedByChanged")]
        public string MOnLastEditedByChanged { get; set; }

        [JsonProperty("mIsSwitchOn")]
        public string MIsSwitchOn { get; set; }

        [JsonProperty("mHasBuildingTag")]
        public string MHasBuildingTag { get; set; }

        [JsonProperty("mBuildingTag")]
        public string MBuildingTag { get; set; }

        [JsonProperty("mOnCircuitsChanged")]
        public string MOnCircuitsChanged { get; set; }

        [JsonProperty("mIsBridgeConnected")]
        public string MIsBridgeConnected { get; set; }

        [JsonProperty("mSprintSpeedFactor")]
        public string MSprintSpeedFactor { get; set; }

        [JsonProperty("mJumpSpeedFactor")]
        public string MJumpSpeedFactor { get; set; }

        [JsonProperty("mFGTextRenderers")]
        public string MFGTextRenderers { get; set; }

        [JsonProperty("mZiplineAttachMontage")]
        public string MZiplineAttachMontage { get; set; }

        [JsonProperty("mZiplineDetachMontage")]
        public string MZiplineDetachMontage { get; set; }

        [JsonProperty("mZiplineTryAttachMontage")]
        public string MZiplineTryAttachMontage { get; set; }

        [JsonProperty("mWantToGrab")]
        public string MWantToGrab { get; set; }

        [JsonProperty("mZiplineJumpLaunchVelocity")]
        public string MZiplineJumpLaunchVelocity { get; set; }

        [JsonProperty("mMaxZiplineAngle")]
        public string MMaxZiplineAngle { get; set; }

        [JsonProperty("mTraceDistance")]
        public string MTraceDistance { get; set; }

        [JsonProperty("mTraceStartOffset")]
        public string MTraceStartOffset { get; set; }

        [JsonProperty("mTraceRadius")]
        public string MTraceRadius { get; set; }

        [JsonProperty("mVisualizeTraceDistance")]
        public string MVisualizeTraceDistance { get; set; }

        [JsonProperty("mZiplineReattachCooldown")]
        public string MZiplineReattachCooldown { get; set; }

        [JsonProperty("mDamageTypesToProtectAgainst")]
        public string MDamageTypesToProtectAgainst { get; set; }

        [JsonProperty("mPostProcessEnabled")]
        public string MPostProcessEnabled { get; set; }

        [JsonProperty("mFilterCountdown")]
        public string MFilterCountdown { get; set; }

        [JsonProperty("mIsInPoisonGas")]
        public string MIsInPoisonGas { get; set; }

        [JsonProperty("DeployedVFXComponents")]
        public string DeployedVFXComponents { get; set; }

        [JsonProperty("mParachuteDeployMontageList")]
        public string MParachuteDeployMontageList { get; set; }

        [JsonProperty("mParachuteDetachMontageList")]
        public string MParachuteDetachMontageList { get; set; }

        [JsonProperty("mUseDistanceOverride")]
        public string MUseDistanceOverride { get; set; }

        [JsonProperty("mIsDeployed")]
        public string MIsDeployed { get; set; }

        [JsonProperty("mLocalCustomizationData")]
        public string MLocalCustomizationData { get; set; }

        [JsonProperty("OnInputPrioritiesChanged")]
        public string OnInputPrioritiesChanged { get; set; }

        [JsonProperty("mInputPriorities")]
        public string MInputPriorities { get; set; }

        [JsonProperty("mCurrentInputIndices")]
        public string MCurrentInputIndices { get; set; }

        [JsonProperty("mCurrentInputPriorityGroupIndex")]
        public string MCurrentInputPriorityGroupIndex { get; set; }

        [JsonProperty("mRevealRadius")]
        public string MRevealRadius { get; set; }

        [JsonProperty("mScannableDescriptors")]
        public string MScannableDescriptors { get; set; }

        [JsonProperty("SFXLocation")]
        public string SFXLocation { get; set; }

        [JsonProperty("mAngleLimit")]
        public string MAngleLimit { get; set; }

        [JsonProperty("mAngleOffset")]
        public string MAngleOffset { get; set; }

        [JsonProperty("mCannonAngle")]
        public string MCannonAngle { get; set; }

        [JsonProperty("mTimeToProduceItem")]
        public string MTimeToProduceItem { get; set; }

        [JsonProperty("mEventType")]
        public string MEventType { get; set; }

        [JsonProperty("mMaxSegmentCount")]
        public string MMaxSegmentCount { get; set; }

        [JsonProperty("mNumSegments")]
        public string MNumSegments { get; set; }

        [JsonProperty("mLadderMeshes")]
        public string MLadderMeshes { get; set; }

        [JsonProperty("mBuildDisqualifierText")]
        public string MBuildDisqualifierText { get; set; }

        [JsonProperty("canDisplayDisqualifier")]
        public string CanDisplayDisqualifier { get; set; }

        [JsonProperty("mChristmasMaterial")]
        public string MChristmasMaterial { get; set; }

        [JsonProperty("mChristmasMaterial1P")]
        public string MChristmasMaterial1P { get; set; }

        [JsonProperty("mCartPlacementClearance")]
        public string MCartPlacementClearance { get; set; }

        [JsonProperty("mCartPlacementDistance")]
        public string MCartPlacementDistance { get; set; }

        [JsonProperty("mSnappedBuildingThickness")]
        public string MSnappedBuildingThickness { get; set; }

        [JsonProperty("mMidMeshLength")]
        public string MMidMeshLength { get; set; }

        [JsonProperty("mGenerateTunnelCollision")]
        public string MGenerateTunnelCollision { get; set; }

        [JsonProperty("mEndCapRotation")]
        public string MEndCapRotation { get; set; }

        [JsonProperty("mMidMeshRotation")]
        public string MMidMeshRotation { get; set; }

        [JsonProperty("mEndCapTranslation")]
        public string MEndCapTranslation { get; set; }

        [JsonProperty("mClearanceHeightMin")]
        public string MClearanceHeightMin { get; set; }

        [JsonProperty("mClearanceThickness")]
        public string MClearanceThickness { get; set; }

        [JsonProperty("mCostSegmentLength")]
        public string MCostSegmentLength { get; set; }

        [JsonProperty("mGeneratedMeshComponents")]
        public string MGeneratedMeshComponents { get; set; }

        [JsonProperty("newCustomizationData")]
        public string NewCustomizationData { get; set; }

        [JsonProperty("OnBuildableLightSourceStateChanged")]
        public string OnBuildableLightSourceStateChanged { get; set; }

        [JsonProperty("mIsEnabled")]
        public string MIsEnabled { get; set; }

        [JsonProperty("mLightControlData")]
        public string MLightControlData { get; set; }

        [JsonProperty("mIsDay")]
        public string MIsDay { get; set; }

        [JsonProperty("mFixtureAngle")]
        public string MFixtureAngle { get; set; }

        [JsonProperty("Timeline_Offset_942DA00C47315AC741095991E04356D7")]
        public string TimelineOffset942DA00C47315AC741095991E04356D7 { get; set; }

        [JsonProperty("Timeline__Direction_942DA00C47315AC741095991E04356D7")]
        public string TimelineDirection942DA00C47315AC741095991E04356D7 { get; set; }

        [JsonProperty("DoorStartTime")]
        public string DoorStartTime { get; set; }

        [JsonProperty("mElevatorDataStruct")]
        public string MElevatorDataStruct { get; set; }

        [JsonProperty("mFloorRelevantElevatorState")]
        public string MFloorRelevantElevatorState { get; set; }

        [JsonProperty("mCachedQueuedStatus")]
        public string MCachedQueuedStatus { get; set; }

        [JsonProperty("mCachedFloorStopInfo")]
        public string MCachedFloorStopInfo { get; set; }

        [JsonProperty("mStopAtFloorTimerHandle")]
        public string MStopAtFloorTimerHandle { get; set; }

        [JsonProperty("mPowerOutPauseTimerHandle")]
        public string MPowerOutPauseTimerHandle { get; set; }

        [JsonProperty("mLockedMovementDirection")]
        public string MLockedMovementDirection { get; set; }

        [JsonProperty("mFloorStopInfos")]
        public string MFloorStopInfos { get; set; }

        [JsonProperty("mQueuedStops")]
        public string MQueuedStops { get; set; }

        [JsonProperty("mQueuedStopIndexes")]
        public string MQueuedStopIndexes { get; set; }

        [JsonProperty("mCharactersInElevator")]
        public string MCharactersInElevator { get; set; }

        [JsonProperty("mOccupyingCharacters")]
        public string MOccupyingCharacters { get; set; }

        [JsonProperty("mAllPawnsInElevator")]
        public string MAllPawnsInElevator { get; set; }

        [JsonProperty("mHeightOfCabin")]
        public string MHeightOfCabin { get; set; }

        [JsonProperty("mSongID")]
        public string MSongID { get; set; }

        [JsonProperty("mDurationToOpenDoors")]
        public string MDurationToOpenDoors { get; set; }

        [JsonProperty("mDurationToWaitAtStop")]
        public string MDurationToWaitAtStop { get; set; }

        [JsonProperty("mDurationToCloseDoors")]
        public string MDurationToCloseDoors { get; set; }

        [JsonProperty("mDurationPauseBeforeMove")]
        public string MDurationPauseBeforeMove { get; set; }

        [JsonProperty("mDurationPausePowerOutage")]
        public string MDurationPausePowerOutage { get; set; }

        [JsonProperty("mDurationPauseBeforeDoorOpen")]
        public string MDurationPauseBeforeDoorOpen { get; set; }

        [JsonProperty("mElevatorState")]
        public string MElevatorState { get; set; }

        [JsonProperty("OnLightControlPanelStateChanged")]
        public string OnLightControlPanelStateChanged { get; set; }

        [JsonProperty("mOnControlledBuildablesChanged")]
        public string MOnControlledBuildablesChanged { get; set; }

        [JsonProperty("mControlledBuildables")]
        public string MControlledBuildables { get; set; }

        [JsonProperty("mGainSignificanceDistance")]
        public string MGainSignificanceDistance { get; set; }

        [JsonProperty("mTextElementToDataMap")]
        public string MTextElementToDataMap { get; set; }

        [JsonProperty("mTextElementToLocDataMap")]
        public string MTextElementToLocDataMap { get; set; }

        [JsonProperty("mIconElementToDataMap")]
        public string MIconElementToDataMap { get; set; }

        [JsonProperty("mSignDrawSize")]
        public string MSignDrawSize { get; set; }

        [JsonProperty("mSoftActivePrefabLayout")]
        public string MSoftActivePrefabLayout { get; set; }

        [JsonProperty("mActivePrefabLayout")]
        public string MActivePrefabLayout { get; set; }

        [JsonProperty("mPrefabTextElementSaveData")]
        public string MPrefabTextElementSaveData { get; set; }

        [JsonProperty("mPrefabIconElementSaveData")]
        public string MPrefabIconElementSaveData { get; set; }

        [JsonProperty("mGlobalPrefabIconElementSaveData")]
        public string MGlobalPrefabIconElementSaveData { get; set; }

        [JsonProperty("mForegroundColor")]
        public string MForegroundColor { get; set; }

        [JsonProperty("mBackgroundColor")]
        public string MBackgroundColor { get; set; }

        [JsonProperty("mAuxilaryColor")]
        public string MAuxilaryColor { get; set; }

        [JsonProperty("mEmissive")]
        public string MEmissive { get; set; }

        [JsonProperty("mGlossiness")]
        public string MGlossiness { get; set; }

        [JsonProperty("mDataVersion")]
        public string MDataVersion { get; set; }

        [JsonProperty("mSignPoles")]
        public string MSignPoles { get; set; }

        [JsonProperty("mWorldDimensions")]
        public string MWorldDimensions { get; set; }

        [JsonProperty("mPoleOffset")]
        public string MPoleOffset { get; set; }

        [JsonProperty("mPoleScale")]
        public string MPoleScale { get; set; }

        [JsonProperty("mSignToSignOffset")]
        public string MSignToSignOffset { get; set; }

        [JsonProperty("WidgetClass")]
        public string WidgetClass { get; set; }

        [JsonProperty("OnAverageDataUpdatedDelegate")]
        public string OnAverageDataUpdatedDelegate { get; set; }

        [JsonProperty("mIsSignificant")]
        public string MIsSignificant { get; set; }

        [JsonProperty("mOffsetAlongConveyor")]
        public string MOffsetAlongConveyor { get; set; }

        [JsonProperty("mCalculatedItemsPerMinute")]
        public string MCalculatedItemsPerMinute { get; set; }

        [JsonProperty("mTotalItems")]
        public string MTotalItems { get; set; }

        [JsonProperty("mTotalTime")]
        public string MTotalTime { get; set; }

        [JsonProperty("mConfidence")]
        public string MConfidence { get; set; }

        [JsonProperty("mReplicatedCoreData")]
        public string MReplicatedCoreData { get; set; }

        [JsonProperty("mOnSpaceElevatorStateUpdated")]
        public string MOnSpaceElevatorStateUpdated { get; set; }

        [JsonProperty("mSpaceElevatorState")]
        public string MSpaceElevatorState { get; set; }
    }

    public class MFuel
    {
        [JsonProperty("mFuelClass")]
        public string MFuelClass { get; set; }

        [JsonProperty("mSupplementalResourceClass")]
        public string MSupplementalResourceClass { get; set; }

        [JsonProperty("mByproduct")]
        public string MByproduct { get; set; }

        [JsonProperty("mByproductAmount")]
        public string MByproductAmount { get; set; }
    }

    public class MSchematicDependency
    {
        [JsonProperty("Class")]
        public string Class { get; set; }

        [JsonProperty("mSchematics")]
        public string MSchematics { get; set; }

        [JsonProperty("mRequireAllSchematicsToBePurchased")]
        public string MRequireAllSchematicsToBePurchased { get; set; }
    }

    public class MUnlock
    {
        [JsonProperty("Class")]
        public string Class { get; set; }

        [JsonProperty("mRecipes")]
        public string MRecipes { get; set; }

        [JsonProperty("mResourcesToAddToScanner")]
        public string MResourcesToAddToScanner { get; set; }

        [JsonProperty("mResourcePairsToAddToScanner")]
        public string MResourcePairsToAddToScanner { get; set; }

        [JsonProperty("mEmotes")]
        public string MEmotes { get; set; }

        [JsonProperty("mSchematics")]
        public string MSchematics { get; set; }

        [JsonProperty("mScannableObjects")]
        public string MScannableObjects { get; set; }
    }

    public class Root
    {
        [JsonProperty("NativeClass")]
        public string NativeClass { get; set; }

        [JsonProperty("Classes")]
        public List<Class> Classes { get; set; }
    }


}