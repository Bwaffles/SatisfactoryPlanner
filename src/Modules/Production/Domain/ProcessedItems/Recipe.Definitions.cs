using SatisfactoryPlanner.Modules.GameData.GameData;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public partial class Recipe
    {
        #region Resources

        // Bauxite
        public static readonly Recipe BauxiteCaterium = new(RecipeType.Standard, "BauxiteCaterium", "Bauxite (Caterium)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(15, Item.CateriumOre)],
            [Product.As(12, Item.Bauxite)],
            [Building.Converter]);

        public static readonly Recipe BauxiteCopper = new(RecipeType.Standard, "BauxiteCopper", "Bauxite (Copper)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(18, Item.CopperOre)],
            [Product.As(12, Item.Bauxite)],
            [Building.Converter]);

        // CateriumOre
        public static readonly Recipe CateriumOreCopper = new(RecipeType.Standard, "CateriumOreCopper", "Caterium Ore (Copper)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(15, Item.CopperOre)],
            [Product.As(12, Item.CateriumOre)],
            [Building.Converter]);

        public static readonly Recipe CateriumOreQuartz = new(RecipeType.Standard, "CateriumOreQuartz", "Caterium Ore (Quartz)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(12, Item.RawQuartz)],
            [Product.As(12, Item.CateriumOre)],
            [Building.Converter]);

        // Coal
        public static readonly Recipe CoalIron = new(RecipeType.Standard, "CoalIron", "Coal (Iron)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(18, Item.IronOre)],
            [Product.As(12, Item.Coal)],
            [Building.Converter]);

        public static readonly Recipe CoalLimestone = new(RecipeType.Standard, "CoalLimestone", "Coal (Limestone)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(36, Item.Limestone)],
            [Product.As(12, Item.Coal)],
            [Building.Converter]);

        public static readonly Recipe Charcoal = new(RecipeType.Alternate, "Charcoal", "Charcoal", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Wood)],
            [Product.As(10, Item.Coal)],
            [Building.Constructor]);

        public static readonly Recipe Biocoal = new(RecipeType.Alternate, "Biocoal", "Biocoal", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Biomass)],
            [Product.As(6, Item.Coal)],
            [Building.Constructor]);

        // CopperOre
        public static readonly Recipe CopperOreQuartz = new(RecipeType.Standard, "CopperOreQuartz", "Copper Ore (Quartz)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(10, Item.RawQuartz)],
            [Product.As(12, Item.CopperOre)],
            [Building.Converter]);

        public static readonly Recipe CopperOreSulfur = new(RecipeType.Standard, "CopperOreSulfur", "Copper Ore (Sulfur)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(12, Item.Sulfur)],
            [Product.As(12, Item.CopperOre)],
            [Building.Converter]);

        // IronOre
        public static readonly Recipe IronOreLimestone = new(RecipeType.Standard, "IronOreLimestone", "Iron Ore (Limestone)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(24, Item.Limestone)],
            [Product.As(12, Item.IronOre)],
            [Building.Converter]);

        // Limestone
        public static readonly Recipe LimestoneSulfur = new(RecipeType.Standard, "LimestoneSulfur", "Limestone (Sulfur)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(2, Item.Sulfur)],
            [Product.As(12, Item.Limestone)],
            [Building.Converter]);

        // Water
        public static readonly Recipe UnpackageWater = new(RecipeType.Standard, "UnpackageWater", "Unpackage Water", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedWater)],
            [Product.As(2, Item.Water), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        // CrudeOil
        public static readonly Recipe UnpackageOil = new(RecipeType.Standard, "UnpackageOil", "Unpackage Oil", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedOil)],
            [Product.As(2, Item.CrudeOil), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        // NitrogenGas
        public static readonly Recipe NitrogenGasBauxite = new(RecipeType.Standard, "NitrogenGasBauxite", "Nitrogen Gas (Bauxite)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(10, Item.Bauxite)],
            [Product.As(12, Item.NitrogenGas)],
            [Building.Converter]);

        public static readonly Recipe NitrogenGasCaterium = new(RecipeType.Standard, "NitrogenGasCaterium", "Nitrogen Gas (Caterium)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(12, Item.CateriumOre)],
            [Product.As(12, Item.NitrogenGas)],
            [Building.Converter]);

        public static readonly Recipe UnpackageNitrogenGas = new(RecipeType.Standard, "UnpackageNitrogenGas", "Unpackage Nitrogen Gas", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PackagedNitrogenGas)],
            [Product.As(4, Item.NitrogenGas), Product.As(1, Item.EmptyFluidTank)],
            [Building.Packager]);

        // RawQuartz
        public static readonly Recipe RawQuartzBauxite = new(RecipeType.Standard, "RawQuartzBauxite", "Raw Quartz (Bauxite)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(10, Item.Bauxite)],
            [Product.As(12, Item.RawQuartz)],
            [Building.Converter]);

        public static readonly Recipe RawQuartzCoal = new(RecipeType.Standard, "RawQuartzCoal", "Raw Quartz (Coal)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(24, Item.Coal)],
            [Product.As(12, Item.RawQuartz)],
            [Building.Converter]);

        // Sulfur
        public static readonly Recipe SulfurCoal = new(RecipeType.Standard, "SulfurCoal", "Sulfur (Coal)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(20, Item.Coal)],
            [Product.As(12, Item.Sulfur)],
            [Building.Converter]);

        public static readonly Recipe SulfurIron = new(RecipeType.Standard, "SulfurIron", "Sulfur (Iron)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(30, Item.IronOre)],
            [Product.As(12, Item.Sulfur)],
            [Building.Converter]);

        // Uranium
        public static readonly Recipe UraniumOreBauxite = new(RecipeType.Standard, "UraniumOreBauxite", "Uranium Ore (Bauxite)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(1, Item.ReanimatedSAM), Ingredient.As(48, Item.Bauxite)],
            [Product.As(12, Item.Uranium)],
            [Building.Converter]);

        #endregion

        #region Ingots

        // IronIngot
        public static readonly Recipe IronIngot = new(RecipeType.Standard, "IronIngot", "Iron Ingot", ManufacturingTime.Of(2, 3), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronOre)],
            [Product.As(1, Item.IronIngot)],
            [Building.Smelter, Building.CraftBench]);

        public static readonly Recipe BasicIronIngot = new(RecipeType.Alternate, "BasicIronIngot", "Basic Iron Ingot", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronOre), Ingredient.As(8, Item.Limestone)],
            [Product.As(10, Item.IronIngot)],
            [Building.Foundry]);

        public static readonly Recipe IronAlloyIngot = new(RecipeType.Alternate, "IronAlloyIngot", "Iron Alloy Ingot", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(8, Item.IronOre), Ingredient.As(2, Item.CopperOre)],
            [Product.As(15, Item.IronIngot)],
            [Building.Foundry]);

        public static readonly Recipe PureIronIngot = new(RecipeType.Alternate, "PureIronIngot", "Pure Iron Ingot", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(7, Item.IronOre), Ingredient.As(4, Item.Water)],
            [Product.As(13, Item.IronIngot)],
            [Building.Refinery]);

        public static readonly Recipe LeachedIronIngot = new(RecipeType.Alternate, "LeachedIronIngot", "Leached Iron Ingot", ManufacturingTime.Of(6, 99), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronOre), Ingredient.As(1, Item.SulfuricAcid)],
            [Product.As(10, Item.IronIngot)],
            [Building.Refinery]);

        // CopperIngot
        public static readonly Recipe CopperIngot = new(RecipeType.Standard, "CopperIngot", "Copper Ingot", ManufacturingTime.Of(2, 3), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CopperOre)],
            [Product.As(1, Item.CopperIngot)],
            [Building.Smelter, Building.CraftBench]);

        public static readonly Recipe TemperedCopperIngot = new(RecipeType.Alternate, "TemperedCopperIngot", "Tempered Copper Ingot", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.CopperOre), Ingredient.As(8, Item.PetroleumCoke)],
            [Product.As(12, Item.CopperIngot)],
            [Building.Foundry]);

        public static readonly Recipe CopperAlloyIngot = new(RecipeType.Alternate, "CopperAlloyIngot", "Copper Alloy Ingot", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.CopperOre), Ingredient.As(5, Item.IronOre)],
            [Product.As(10, Item.CopperIngot)],
            [Building.Foundry]);

        public static readonly Recipe PureCopperIngot = new(RecipeType.Alternate, "PureCopperIngot", "Pure Copper Ingot", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.CopperOre), Ingredient.As(4, Item.Water)],
            [Product.As(15, Item.CopperIngot)],
            [Building.Refinery]);

        public static readonly Recipe LeachedCopperIngot = new(RecipeType.Alternate, "LeachedCopperIngot", "Leached Copper Ingot", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(9, Item.CopperOre), Ingredient.As(5, Item.SulfuricAcid)],
            [Product.As(22, Item.CopperIngot)],
            [Building.Refinery]);

        // SteelIngot
        public static readonly Recipe SteelIngot = new(RecipeType.Standard, "SteelIngot", "Steel Ingot", ManufacturingTime.Of(4, 3), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.IronOre), Ingredient.As(3, Item.Coal)],
            [Product.As(3, Item.SteelIngot)],
            [Building.Foundry, Building.CraftBench]);

        public static readonly Recipe CokeSteelIngot = new(RecipeType.Alternate, "CokeSteelIngot", "Coke Steel Ingot", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.IronOre), Ingredient.As(15, Item.PetroleumCoke)],
            [Product.As(20, Item.SteelIngot)],
            [Building.Foundry]);

        public static readonly Recipe CompactedSteelIngot = new(RecipeType.Alternate, "CompactedSteelIngot", "Compacted Steel Ingot", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronOre), Ingredient.As(1, Item.CompactedCoal)],
            [Product.As(4, Item.SteelIngot)],
            [Building.Foundry]);

        public static readonly Recipe SolidSteelIngot = new(RecipeType.Alternate, "SolidSteelIngot", "Solid Steel Ingot", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronIngot), Ingredient.As(2, Item.Coal)],
            [Product.As(3, Item.SteelIngot)],
            [Building.Foundry]);

        // CateriumIngot
        public static readonly Recipe CateriumIngot = new(RecipeType.Standard, "CateriumIngot", "Caterium Ingot", ManufacturingTime.Of(4, 2), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CateriumOre)],
            [Product.As(1, Item.CateriumIngot)],
            [Building.Smelter, Building.CraftBench]);

        public static readonly Recipe TemperedCateriumIngot = new(RecipeType.Alternate, "TemperedCateriumIngot", "Tempered Caterium Ingot", ManufacturingTime.Of(8, 99), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.CateriumOre), Ingredient.As(2, Item.PetroleumCoke)],
            [Product.As(3, Item.CateriumIngot)],
            [Building.Foundry]);

        public static readonly Recipe PureCateriumIngot = new(RecipeType.Alternate, "PureCateriumIngot", "Pure Caterium Ingot", ManufacturingTime.Of(5, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.CateriumOre), Ingredient.As(2, Item.Water)],
            [Product.As(1, Item.CateriumIngot)],
            [Building.Refinery]);

        public static readonly Recipe LeachedCateriumIngot = new(RecipeType.Alternate, "LeachedCateriumIngot", "Leached Caterium Ingot", ManufacturingTime.Of(10, 99), VariablePowerConsumption.None(),
            [Ingredient.As(9, Item.CateriumOre), Ingredient.As(5, Item.SulfuricAcid)],
            [Product.As(6, Item.CateriumIngot)],
            [Building.Refinery]);

        // AluminumIngot
        public static readonly Recipe AluminumIngot = new(RecipeType.Standard, "AluminumIngot", "Aluminum Ingot", ManufacturingTime.Of(4, 3), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.AluminumScrap), Ingredient.As(5, Item.Silica)],
            [Product.As(4, Item.AluminumIngot)],
            [Building.Foundry, Building.CraftBench]);

        public static readonly Recipe PureAluminumIngot = new(RecipeType.Alternate, "PureAluminumIngot", "Pure Aluminum Ingot", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.AluminumScrap)],
            [Product.As(1, Item.AluminumIngot)],
            [Building.Smelter]);

        // FicsiteIngot
        public static readonly Recipe FicsiteIngotIron = new(RecipeType.Standard, "FicsiteIngotIron", "Ficsite Ingot (Iron)", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(4, Item.ReanimatedSAM), Ingredient.As(24, Item.IronIngot)],
            [Product.As(1, Item.FicsiteIngot)],
            [Building.Converter]);

        public static readonly Recipe FicsiteIngotCaterium = new(RecipeType.Standard, "FicsiteIngotCaterium", "Ficsite Ingot (Caterium)", ManufacturingTime.Of(4, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(3, Item.ReanimatedSAM), Ingredient.As(4, Item.CateriumIngot)],
            [Product.As(1, Item.FicsiteIngot)],
            [Building.Converter]);

        public static readonly Recipe FicsiteIngotAluminum = new(RecipeType.Standard, "FicsiteIngotAluminum", "Ficsite Ingot (Aluminum)", ManufacturingTime.Of(2, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(2, Item.ReanimatedSAM), Ingredient.As(4, Item.AluminumIngot)],
            [Product.As(1, Item.FicsiteIngot)],
            [Building.Converter]);

        #endregion

        #region Standard Parts

        // Iron Rods
        public static readonly Recipe IronRod = new(RecipeType.Standard, "IronRod", "Iron Rod", ManufacturingTime.Of(4, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronIngot)],
            [Product.As(1, Item.IronRod)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe AluminumRod = new(RecipeType.Alternate, "AluminumRod", "Aluminum Rod", ManufacturingTime.Of(8, 99), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.AluminumIngot)],
            [Product.As(7, Item.IronRod)],
            [Building.Constructor]);

        public static readonly Recipe SteelRod = new(RecipeType.Alternate, "SteelRod", "Steel Rod", ManufacturingTime.Of(5, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.SteelIngot)],
            [Product.As(4, Item.IronRod)],
            [Building.Constructor]);

        // Iron Plates
        public static readonly Recipe IronPlate = new(RecipeType.Standard, "IronPlate", "Iron Plate", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.IronIngot)],
            [Product.As(2, Item.IronPlate)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CoatedIronPlate = new(RecipeType.Alternate, "CoatedIronPlate", "Coated Iron Plate", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronIngot), Ingredient.As(1, Item.Plastic)],
            [Product.As(10, Item.IronPlate)],
            [Building.Assembler]);

        public static readonly Recipe SteelCastPlate = new(RecipeType.Alternate, "SteelCastPlate", "Steel Cast Plate", ManufacturingTime.Of(4, 99), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronIngot), Ingredient.As(1, Item.SteelIngot)],
            [Product.As(3, Item.IronPlate)],
            [Building.Foundry]);

        // ReinforcedIronPlate
        public static readonly Recipe ReinforcedIronPlate = new(RecipeType.Standard, "ReinforcedIronPlate", "Reinforced Iron Plate", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.IronPlate), Ingredient.As(12, Item.Screws)],
            [Product.As(1, Item.ReinforcedIronPlate)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe AdheredIronPlate = new(RecipeType.Alternate, "AdheredIronPlate", "Adhered Iron Plate", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.IronPlate), Ingredient.As(1, Item.Rubber)],
            [Product.As(1, Item.ReinforcedIronPlate)],
            [Building.Assembler]);

        public static readonly Recipe BoltedIronPlate = new(RecipeType.Alternate, "BoltedIronPlate", "Bolted Iron Plate", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(18, Item.IronPlate), Ingredient.As(50, Item.Screws)],
            [Product.As(3, Item.ReinforcedIronPlate)],
            [Building.Assembler]);

        public static readonly Recipe StitchedIronPlate = new(RecipeType.Alternate, "StitchedIronPlate", "Stitched Iron Plate", ManufacturingTime.Of(32, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.IronPlate), Ingredient.As(20, Item.Wire)],
            [Product.As(3, Item.ReinforcedIronPlate)],
            [Building.Assembler]);

        // Screws
        public static readonly Recipe Screws = new(RecipeType.Standard, "Screws", "Screws", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronRod)],
            [Product.As(4, Item.Screws)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CastScrews = new(RecipeType.Alternate, "CastScrews", "Cast Screws", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronIngot)],
            [Product.As(20, Item.Screws)],
            [Building.Constructor]);

        public static readonly Recipe SteelScrews = new(RecipeType.Alternate, "SteelScrews", "Steel Screws", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.SteelBeam)],
            [Product.As(52, Item.Screws)],
            [Building.Constructor]);

        // ModularFrame
        public static readonly Recipe ModularFrame = new(RecipeType.Standard, "ModularFrame", "Modular Frame", ManufacturingTime.Of(60, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.ReinforcedIronPlate), Ingredient.As(12, Item.IronRod)],
            [Product.As(2, Item.ModularFrame)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe BoltedFrame = new(RecipeType.Alternate, "BoltedFrame", "Bolted Frame", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.ReinforcedIronPlate), Ingredient.As(56, Item.Screws)],
            [Product.As(2, Item.ModularFrame)],
            [Building.Assembler]);

        public static readonly Recipe SteeledFrame = new(RecipeType.Alternate, "SteeledFrame", "Steeled Frame", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.ReinforcedIronPlate), Ingredient.As(10, Item.SteelPipe)],
            [Product.As(3, Item.ModularFrame)],
            [Building.Assembler]);

        // HeavyModularFrame
        public static readonly Recipe HeavyModularFrame = new(RecipeType.Standard, "HeavyModularFrame", "Heavy Modular Frame", ManufacturingTime.Of(30, 0.6m), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.ModularFrame), Ingredient.As(20, Item.SteelPipe), Ingredient.As(5, Item.EncasedIndustrialBeam), Ingredient.As(120, Item.Screws)],
            [Product.As(1, Item.HeavyModularFrame)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe HeavyEncasedFrame = new(RecipeType.Alternate, "HeavyEncasedFrame", "Heavy Encased Frame", ManufacturingTime.Of(64, 1), VariablePowerConsumption.None(),
            [Ingredient.As(8, Item.ModularFrame), Ingredient.As(10, Item.EncasedIndustrialBeam), Ingredient.As(36, Item.SteelPipe), Ingredient.As(22, Item.Concrete)],
            [Product.As(3, Item.HeavyModularFrame)],
            [Building.Manufacturer]);

        public static readonly Recipe HeavyFlexibleFrame = new(RecipeType.Alternate, "HeavyFlexibleFrame", "Heavy Flexible Frame", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.ModularFrame), Ingredient.As(3, Item.EncasedIndustrialBeam), Ingredient.As(20, Item.Rubber), Ingredient.As(104, Item.Screws)],
            [Product.As(1, Item.HeavyModularFrame)],
            [Building.Manufacturer]);

        // FusedModularFrame
        public static readonly Recipe FusedModularFrame = new(RecipeType.Standard, "FusedModularFrame", "Fused Modular Frame", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.HeavyModularFrame), Ingredient.As(50, Item.AluminumCasing), Ingredient.As(25, Item.NitrogenGas)],
            [Product.As(1, Item.FusedModularFrame)],
            [Building.Blender]);

        public static readonly Recipe HeatFusedFrame = new(RecipeType.Alternate, "HeatFusedFrame", "Heat-Fused Frame", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.HeavyModularFrame), Ingredient.As(50, Item.AluminumIngot), Ingredient.As(8, Item.NitricAcid), Ingredient.As(10, Item.Fuel)],
            [Product.As(1, Item.FusedModularFrame)],
            [Building.Blender]);

        // SteelBeam
        public static readonly Recipe SteelBeam = new(RecipeType.Standard, "SteelBeam", "Steel Beam", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.SteelIngot)],
            [Product.As(1, Item.SteelBeam)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe MoldedBeam = new(RecipeType.Alternate, "MoldedBeam", "Molded Beam", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(24, Item.SteelIngot), Ingredient.As(16, Item.Concrete)],
            [Product.As(9, Item.SteelBeam)],
            [Building.Foundry]);

        public static readonly Recipe AluminumBeam = new(RecipeType.Alternate, "AluminumBeam", "Aluminum Beam", ManufacturingTime.Of(8, 99), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.AluminumIngot)],
            [Product.As(3, Item.SteelBeam)],
            [Building.Constructor]);

        // SteelPipe
        public static readonly Recipe SteelPipe = new(RecipeType.Standard, "SteelPipe", "Steel Pipe", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.SteelIngot)],
            [Product.As(2, Item.SteelPipe)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe MoldedSteelPipe = new(RecipeType.Alternate, "MoldedSteelPipe", "Molded Steel Pipe", ManufacturingTime.Of(6, 99), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.SteelIngot), Ingredient.As(3, Item.Concrete)],
            [Product.As(5, Item.SteelPipe)],
            [Building.Foundry]);

        public static readonly Recipe IronPipe = new(RecipeType.Alternate, "IronPipe", "Iron Pipe", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.IronIngot)],
            [Product.As(5, Item.SteelPipe)],
            [Building.Constructor]);

        // EncasedIndustrialBeam
        public static readonly Recipe EncasedIndustrialBeam = new(RecipeType.Standard, "EncasedIndustrialBeam", "Encased Industrial Beam", ManufacturingTime.Of(10, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.SteelBeam), Ingredient.As(6, Item.Concrete)],
            [Product.As(1, Item.EncasedIndustrialBeam)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe EncasedIndustrialPipe = new(RecipeType.Alternate, "EncasedIndustrialPipe", "Encased Industrial Pipe", ManufacturingTime.Of(15, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.SteelPipe), Ingredient.As(5, Item.Concrete)],
            [Product.As(1, Item.EncasedIndustrialBeam)],
            [Building.Assembler]);

        // CopperSheet
        public static readonly Recipe CopperSheet = new(RecipeType.Standard, "CopperSheet", "Copper Sheet", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.CopperIngot)],
            [Product.As(1, Item.CopperSheet)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe SteamedCopperSheet = new(RecipeType.Alternate, "SteamedCopperSheet", "Steamed Copper Sheet", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CopperIngot), Ingredient.As(3, Item.Water)],
            [Product.As(3, Item.CopperSheet)],
            [Building.Refinery]);

        // AluminumCasing
        public static readonly Recipe AluminumCasing = new(RecipeType.Standard, "AluminumCasing", "Aluminum Casing", ManufacturingTime.Of(2, 2), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.AluminumIngot)],
            [Product.As(2, Item.AluminumCasing)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe AlcladCasing = new(RecipeType.Alternate, "AlcladCasing", "Alclad Casing", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.AluminumIngot), Ingredient.As(10, Item.CopperIngot)],
            [Product.As(15, Item.AluminumCasing)],
            [Building.Assembler]);

        // AlcladAluminumSheet
        public static readonly Recipe AlcladAluminumSheet = new(RecipeType.Standard, "AlcladAluminumSheet", "Alclad Aluminum Sheet", ManufacturingTime.Of(6, 2), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.AluminumIngot), Ingredient.As(1, Item.CopperIngot)],
            [Product.As(3, Item.AlcladAluminumSheet)],
            [Building.Assembler, Building.CraftBench]);

        // FicsiteTrigon
        public static readonly Recipe FicsiteTrigon = new(RecipeType.Standard, "FicsiteTrigon", "Ficsite Trigon", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.FicsiteIngot)],
            [Product.As(3, Item.FicsiteTrigon)],
            [Building.Constructor, Building.CraftBench]);

        #endregion

        #region Industrial Parts
        // Rotor
        public static readonly Recipe Rotor = new(RecipeType.Standard, "Rotor", "Rotor", ManufacturingTime.Of(15, 0.8m), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronRod), Ingredient.As(25, Item.Screws)],
            [Product.As(1, Item.Rotor)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe CopperRotor = new(RecipeType.Alternate, "CopperRotor", "Copper Rotor", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.CopperSheet), Ingredient.As(52, Item.Screws)],
            [Product.As(3, Item.Rotor)],
            [Building.Assembler]);

        public static readonly Recipe SteelRotor = new(RecipeType.Alternate, "SteelRotor", "Steel Rotor", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.SteelPipe), Ingredient.As(6, Item.Wire)],
            [Product.As(1, Item.Rotor)],
            [Building.Assembler]);

        // Stator
        public static readonly Recipe Stator = new(RecipeType.Standard, "Stator", "Stator", ManufacturingTime.Of(12, 1.5m), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.SteelPipe), Ingredient.As(8, Item.Wire)],
            [Product.As(1, Item.Stator)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe QuickwireStator = new(RecipeType.Alternate, "QuickwireStator", "Quickwire Stator", ManufacturingTime.Of(15, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.SteelPipe), Ingredient.As(15, Item.Quickwire)],
            [Product.As(2, Item.Stator)],
            [Building.Assembler]);

        // Motor
        public static readonly Recipe Motor = new(RecipeType.Standard, "Motor", "Motor", ManufacturingTime.Of(12, 2), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Rotor), Ingredient.As(2, Item.Stator)],
            [Product.As(1, Item.Motor)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe ElectricMotor = new(RecipeType.Alternate, "ElectricMotor", "Electric Motor", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ElectromagneticControlRod), Ingredient.As(2, Item.Rotor)],
            [Product.As(2, Item.Motor)],
            [Building.Assembler]);

        public static readonly Recipe RigorMotor = new(RecipeType.Alternate, "RigorMotor", "Rigor Motor", ManufacturingTime.Of(48, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Rotor), Ingredient.As(3, Item.Stator), Ingredient.As(1, Item.CrystalOscillator)],
            [Product.As(6, Item.Motor)],
            [Building.Manufacturer]);

        // HeatSink
        public static readonly Recipe HeatSink = new(RecipeType.Standard, "HeatSink", "Heat Sink", ManufacturingTime.Of(8, 1.5m), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.AlcladAluminumSheet), Ingredient.As(3, Item.CopperSheet)],
            [Product.As(1, Item.HeatSink)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe HeatExchanger = new(RecipeType.Alternate, "HeatExchanger", "Heat Exchanger", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.AluminumCasing), Ingredient.As(3, Item.Rubber)],
            [Product.As(1, Item.HeatSink)],
            [Building.Assembler]);

        // CoolingSystem
        public static readonly Recipe CoolingSystem = new(RecipeType.Standard, "CoolingSystem", "Cooling System", ManufacturingTime.Of(10, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.HeatSink), Ingredient.As(2, Item.Rubber), Ingredient.As(5, Item.Water), Ingredient.As(25, Item.NitrogenGas)],
            [Product.As(1, Item.CoolingSystem)],
            [Building.Blender]);

        public static readonly Recipe CoolingDevice = new(RecipeType.Alternate, "CoolingDevice", "Cooling Device", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.HeatSink), Ingredient.As(1, Item.Motor), Ingredient.As(24, Item.NitrogenGas)],
            [Product.As(2, Item.CoolingSystem)],
            [Building.Blender]);

        // TurboMotor
        public static readonly Recipe TurboMotor = new(RecipeType.Standard, "TurboMotor", "Turbo Motor", ManufacturingTime.Of(32, 2), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.CoolingSystem), Ingredient.As(2, Item.RadioControlUnit), Ingredient.As(4, Item.Motor), Ingredient.As(24, Item.Rubber)],
            [Product.As(1, Item.TurboMotor)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe TurboElectricMotor = new(RecipeType.Alternate, "TurboElectricMotor", "Turbo Electric Motor", ManufacturingTime.Of(64, 1), VariablePowerConsumption.None(),
            [Ingredient.As(7, Item.Motor), Ingredient.As(9, Item.RadioControlUnit), Ingredient.As(5, Item.ElectromagneticControlRod), Ingredient.As(7, Item.Rotor)],
            [Product.As(3, Item.TurboMotor)],
            [Building.Manufacturer]);

        public static readonly Recipe TurboPressureMotor = new(RecipeType.Alternate, "TurboPressureMotor", "Turbo Pressure Motor", ManufacturingTime.Of(32, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Motor), Ingredient.As(1, Item.PressureConversionCube), Ingredient.As(24, Item.PackagedNitrogenGas), Ingredient.As(8, Item.Stator)],
            [Product.As(2, Item.TurboMotor)],
            [Building.Manufacturer]);

        // Battery
        public static readonly Recipe Battery = new(RecipeType.Standard, "Battery", "Battery", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2.5m, Item.SulfuricAcid), Ingredient.As(2, Item.AluminaSolution), Ingredient.As(1, Item.AluminumCasing)],
            [Product.As(1, Item.Battery), Product.As(1.5m, Item.Water)],
            [Building.Blender]);

        public static readonly Recipe ClassicBattery = new(RecipeType.Alternate, "ClassicBattery", "Classic Battery", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Sulfur), Ingredient.As(7, Item.AlcladAluminumSheet), Ingredient.As(8, Item.Plastic), Ingredient.As(12, Item.Wire)],
            [Product.As(4, Item.Battery)],
            [Building.Manufacturer]);

        #endregion

        #region Compounds

        // Concrete
        public static readonly Recipe Concrete = new(RecipeType.Standard, "Concrete", "Concrete", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Limestone)],
            [Product.As(1, Item.Concrete)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe FineConcrete = new(RecipeType.Alternate, "FineConcrete", "Fine Concrete", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Silica), Ingredient.As(12, Item.Limestone)],
            [Product.As(10, Item.Concrete)],
            [Building.Assembler]);

        public static readonly Recipe RubberConcrete = new(RecipeType.Alternate, "RubberConcrete", "Rubber Concrete", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Limestone), Ingredient.As(2, Item.Rubber)],
            [Product.As(9, Item.Concrete)],
            [Building.Assembler]);

        public static readonly Recipe WetConcrete = new(RecipeType.Alternate, "WetConcrete", "Wet Concrete", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Limestone), Ingredient.As(5, Item.Water)],
            [Product.As(4, Item.Concrete)],
            [Building.Refinery]);

        // QuartzCrystal
        public static readonly Recipe QuartzCrystal = new(RecipeType.Standard, "QuartzCrystal", "Quartz Crystal", ManufacturingTime.Of(8, 2), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.RawQuartz)],
            [Product.As(3, Item.QuartzCrystal)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe PureQuartzCrystal = new(RecipeType.Alternate, "PureQuartzCrystal", "Pure Quartz Crystal", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(9, Item.RawQuartz), Ingredient.As(5, Item.Water)],
            [Product.As(7, Item.QuartzCrystal)],
            [Building.Refinery]);

        public static readonly Recipe QuartzPurification = new(RecipeType.Alternate, "QuartzPurification", "Quartz Purification", ManufacturingTime.Of(12, 99), VariablePowerConsumption.None(),
            [Ingredient.As(24, Item.RawQuartz), Ingredient.As(2, Item.NitricAcid)],
            [Product.As(15, Item.QuartzCrystal), Product.As(12, Item.DissolvedSilica)],
            [Building.Refinery]);

        public static readonly Recipe FusedQuartzCrystal = new(RecipeType.Alternate, "FusedQuartzCrystal", "Fused Quartz Crystal", ManufacturingTime.Of(20, 99), VariablePowerConsumption.None(),
            [Ingredient.As(25, Item.RawQuartz), Ingredient.As(12, Item.Coal)],
            [Product.As(18, Item.QuartzCrystal)],
            [Building.Foundry]);

        // Silica
        public static readonly Recipe Silica = new(RecipeType.Standard, "Silica", "Silica", ManufacturingTime.Of(8, 2), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.RawQuartz)],
            [Product.As(5, Item.Silica)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CheapSilica = new(RecipeType.Alternate, "CheapSilica", "Cheap Silica", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.RawQuartz), Ingredient.As(5, Item.Limestone)],
            [Product.As(7, Item.Silica)],
            [Building.Assembler]);

        public static readonly Recipe DistilledSilica = new(RecipeType.Alternate, "DistilledSilica", "Distilled Silica", ManufacturingTime.Of(6, 99), VariablePowerConsumption.None(),
            [Ingredient.As(12, Item.DissolvedSilica), Ingredient.As(5, Item.Limestone), Ingredient.As(10, Item.Water)],
            [Product.As(27, Item.Silica), Product.As(8, Item.Water)],
            [Building.Blender]);

        // Compacted Coal
        public static readonly Recipe CompactedCoal = new(RecipeType.Alternate, "CompactedCoal", "Compacted Coal", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Coal), Ingredient.As(5, Item.Sulfur)],
            [Product.As(5, Item.CompactedCoal)],
            [Building.Assembler]);

        // CopperPowder
        public static readonly Recipe CopperPowder = new(RecipeType.Standard, "CopperPowder", "Copper Powder", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(30, Item.CopperIngot)],
            [Product.As(5, Item.CopperPowder)],
            [Building.Constructor, Building.CraftBench]);

        #endregion

        #region Electronics
        // Wire
        public static readonly Recipe Wire = new(RecipeType.Standard, "Wire", "Wire", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CopperIngot)],
            [Product.As(2, Item.Wire)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CateriumWire = new(RecipeType.Alternate, "CateriumWire", "Caterium Wire", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CateriumIngot)],
            [Product.As(8, Item.Wire)],
            [Building.Constructor]);

        public static readonly Recipe FusedWire = new(RecipeType.Alternate, "FusedWire", "Fused Wire", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.CopperIngot), Ingredient.As(1, Item.CateriumIngot)],
            [Product.As(30, Item.Wire)],
            [Building.Assembler]);

        public static readonly Recipe IronWire = new(RecipeType.Alternate, "IronWire", "Iron Wire", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.IronIngot)],
            [Product.As(9, Item.Wire)],
            [Building.Constructor]);

        // Cable
        public static readonly Recipe Cable = new(RecipeType.Standard, "Cable", "Cable", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Wire)],
            [Product.As(1, Item.Cable)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CoatedCable = new(RecipeType.Alternate, "CoatedCable", "Coated Cable", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Wire), Ingredient.As(2, Item.HeavyOilResidue)],
            [Product.As(9, Item.Cable)],
            [Building.Refinery]);

        public static readonly Recipe InsulatedCable = new(RecipeType.Alternate, "InsulatedCable", "Insulated Cable", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(9, Item.Wire), Ingredient.As(6, Item.Rubber)],
            [Product.As(20, Item.Cable)],
            [Building.Assembler]);

        public static readonly Recipe QuickwireCable = new(RecipeType.Alternate, "QuickwireCable", "Quickwire Cable", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Quickwire), Ingredient.As(2, Item.Rubber)],
            [Product.As(11, Item.Cable)],
            [Building.Assembler]);

        // CircuitBoard
        public static readonly Recipe CircuitBoard = new(RecipeType.Standard, "CircuitBoard", "Circuit Board", ManufacturingTime.Of(8, 1.5m), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.CopperSheet), Ingredient.As(4, Item.Plastic)],
            [Product.As(1, Item.CircuitBoard)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe CateriumCircuitBoard = new(RecipeType.Alternate, "CateriumCircuitBoard", "Caterium Circuit Board", ManufacturingTime.Of(48, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Plastic), Ingredient.As(30, Item.Quickwire)],
            [Product.As(7, Item.CircuitBoard)],
            [Building.Assembler]);

        public static readonly Recipe ElectrodeCircuitBoard = new(RecipeType.Alternate, "ElectrodeCircuitBoard", "Electrode Circuit Board", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Rubber), Ingredient.As(8, Item.PetroleumCoke)],
            [Product.As(1, Item.CircuitBoard)],
            [Building.Assembler]);

        public static readonly Recipe SiliconCircuitBoard = new(RecipeType.Alternate, "SiliconCircuitBoard", "Silicon Circuit Board", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(11, Item.CopperSheet), Ingredient.As(11, Item.Silica)],
            [Product.As(5, Item.CircuitBoard)],
            [Building.Assembler]);

        // HighSpeedConnector
        public static readonly Recipe HighSpeedConnector = new(RecipeType.Standard, "HighSpeedConnector", "High-Speed Connector", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(56, Item.Quickwire), Ingredient.As(10, Item.Cable), Ingredient.As(1, Item.CircuitBoard)],
            [Product.As(1, Item.HighSpeedConnector)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe SiliconHighSpeedConnector = new(RecipeType.Alternate, "SiliconHighSpeedConnector", "Silicon High-Speed Connector", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(60, Item.Quickwire), Ingredient.As(25, Item.Silica), Ingredient.As(2, Item.CircuitBoard)],
            [Product.As(2, Item.HighSpeedConnector)],
            [Building.Manufacturer]);

        // AILimiter
        public static readonly Recipe AILimiter = new(RecipeType.Standard, "AILimiter", "AI Limiter", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.CopperSheet), Ingredient.As(20, Item.Quickwire)],
            [Product.As(1, Item.AILimiter)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe PlasticAILimiter = new(RecipeType.Alternate, "PlasticAILimiter", "Plastic AI Limiter", ManufacturingTime.Of(15, 99), VariablePowerConsumption.None(),
            [Ingredient.As(30, Item.Quickwire), Ingredient.As(7, Item.Plastic)],
            [Product.As(2, Item.AILimiter)],
            [Building.Assembler]);

        // Quickwire
        public static readonly Recipe Quickwire = new(RecipeType.Standard, "Quickwire", "Quickwire", ManufacturingTime.Of(5, 2), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CateriumIngot)],
            [Product.As(5, Item.Quickwire)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe FusedQuickwire = new(RecipeType.Alternate, "FusedQuickwire", "Fused Quickwire", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CateriumIngot), Ingredient.As(5, Item.CopperIngot)],
            [Product.As(12, Item.Quickwire)],
            [Building.Assembler]);

        // SingularityCell
        public static readonly Recipe SingularityCell = new(RecipeType.Standard, "SingularityCell", "Singularity Cell", ManufacturingTime.Of(60, 1), VariablePowerConsumption.Of(0, 0),
            [Ingredient.As(1, Item.NuclearPasta), Ingredient.As(20, Item.DarkMatterCrystal), Ingredient.As(100, Item.IronPlate), Ingredient.As(200, Item.Concrete)],
            [Product.As(10, Item.SingularityCell)],
            [Building.Manufacturer]);

        // ReanimatedSAM
        public static readonly Recipe ReanimatedSAM = new(RecipeType.Standard, "ReanimatedSAM", "Reanimated SAM", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.SAM)],
            [Product.As(1, Item.ReanimatedSAM)],
            [Building.Constructor, Building.CraftBench]);

        // SAMFluctuator
        public static readonly Recipe SAMFluctuator = new(RecipeType.Standard, "SAMFluctuator", "SAM Fluctuator", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.ReanimatedSAM), Ingredient.As(5, Item.Wire), Ingredient.As(3, Item.SteelPipe)],
            [Product.As(1, Item.SAMFluctuator)],
            [Building.Manufacturer, Building.CraftBench]);

        #endregion

        #region Communications
        // Computer
        public static readonly Recipe Computer = new(RecipeType.Standard, "Computer", "Computer", ManufacturingTime.Of(24, 1.5m), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.CircuitBoard), Ingredient.As(8, Item.Cable), Ingredient.As(16, Item.Plastic)],
            [Product.As(1, Item.Computer)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe CateriumComputer = new(RecipeType.Alternate, "CateriumComputer", "Caterium Computer", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.CircuitBoard), Ingredient.As(14, Item.Quickwire), Ingredient.As(6, Item.Rubber)],
            [Product.As(1, Item.Computer)],
            [Building.Manufacturer]);

        public static readonly Recipe CrystalComputer = new(RecipeType.Alternate, "CrystalComputer", "Crystal Computer", ManufacturingTime.Of(36, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CircuitBoard), Ingredient.As(1, Item.CrystalOscillator)],
            [Product.As(2, Item.Computer)],
            [Building.Assembler]);

        // Supercomputer
        public static readonly Recipe Supercomputer = new(RecipeType.Standard, "Supercomputer", "Supercomputer", ManufacturingTime.Of(32, 1.5m), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Computer), Ingredient.As(2, Item.AILimiter), Ingredient.As(3, Item.HighSpeedConnector), Ingredient.As(28, Item.Plastic)],
            [Product.As(1, Item.Supercomputer)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe OCSupercomputer = new(RecipeType.Alternate, "OCSupercomputer", "OC Supercomputer", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.RadioControlUnit), Ingredient.As(2, Item.CoolingSystem)],
            [Product.As(1, Item.Supercomputer)],
            [Building.Assembler]);

        public static readonly Recipe SuperStateComputer = new(RecipeType.Alternate, "SuperStateComputer", "Super-State Computer", ManufacturingTime.Of(25, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Computer), Ingredient.As(1, Item.ElectromagneticControlRod), Ingredient.As(10, Item.Battery), Ingredient.As(25, Item.Wire)],
            [Product.As(1, Item.Supercomputer)],
            [Building.Manufacturer]);

        // RadioControlUnit
        public static readonly Recipe RadioControlUnit = new(RecipeType.Standard, "RadioControlUnit", "Radio Control Unit", ManufacturingTime.Of(48, 1), VariablePowerConsumption.None(),
            [Ingredient.As(32, Item.AluminumCasing), Ingredient.As(1, Item.CrystalOscillator), Ingredient.As(2, Item.Computer)],
            [Product.As(2, Item.RadioControlUnit)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe RadioConnectionUnit = new(RecipeType.Alternate, "RadioConnectionUnit", "Radio Connection Unit", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.HeatSink), Ingredient.As(2, Item.HighSpeedConnector), Ingredient.As(12, Item.QuartzCrystal)],
            [Product.As(1, Item.RadioControlUnit)],
            [Building.Manufacturer]);

        public static readonly Recipe RadioControlSystem = new(RecipeType.Alternate, "RadioControlSystem", "Radio Control System", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CrystalOscillator), Ingredient.As(10, Item.CircuitBoard), Ingredient.As(60, Item.AluminumCasing), Ingredient.As(30, Item.Rubber)],
            [Product.As(3, Item.RadioControlUnit)],
            [Building.Manufacturer]);

        // CrystalOscillator
        public static readonly Recipe CrystalOscillator = new(RecipeType.Standard, "CrystalOscillator", "Crystal Oscillator", ManufacturingTime.Of(120, 0.3m), VariablePowerConsumption.None(),
            [Ingredient.As(36, Item.QuartzCrystal), Ingredient.As(28, Item.Cable), Ingredient.As(5, Item.ReinforcedIronPlate)],
            [Product.As(2, Item.CrystalOscillator)],
            [Building.Manufacturer, Building.CraftBench]);

        public static readonly Recipe InsulatedCrystalOscillator = new(RecipeType.Alternate, "InsulatedCrystalOscillator", "Insulated Crystal Oscillator", ManufacturingTime.Of(32, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.QuartzCrystal), Ingredient.As(7, Item.Rubber), Ingredient.As(1, Item.AILimiter)],
            [Product.As(1, Item.CrystalOscillator)],
            [Building.Manufacturer]);

        #endregion

        #region Oil Products

        // Packaged Oil
        public static readonly Recipe PackagedOil = new(RecipeType.Standard, "PackagedOil", "Packaged Oil", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.CrudeOil), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedOil)],
            [Building.Packager]);

        // Plastic
        public static readonly Recipe Plastic = new(RecipeType.Standard, "Plastic", "Plastic", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CrudeOil)],
            [Product.As(2, Item.Plastic), Product.As(1, Item.HeavyOilResidue)],
            [Building.Refinery]);

        public static readonly Recipe RecycledPlastic = new(RecipeType.Alternate, "RecycledPlastic", "Recycled Plastic", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Rubber), Ingredient.As(6, Item.Fuel)],
            [Product.As(12, Item.Plastic)],
            [Building.Refinery]);

        public static readonly Recipe ResidualPlastic = new(RecipeType.Standard, "ResidualPlastic", "Residual Plastic", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.PolymerResin), Ingredient.As(2, Item.Water)],
            [Product.As(2, Item.Plastic)],
            [Building.Refinery]);

        // Rubber
        public static readonly Recipe Rubber = new(RecipeType.Standard, "Rubber", "Rubber", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CrudeOil)],
            [Product.As(2, Item.Rubber), Product.As(2, Item.HeavyOilResidue)],
            [Building.Refinery]);

        public static readonly Recipe ResidualRubber = new(RecipeType.Standard, "ResidualRubber", "Residual Rubber", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.PolymerResin), Ingredient.As(4, Item.Water)],
            [Product.As(2, Item.Rubber)],
            [Building.Refinery]);

        public static readonly Recipe RecycledRubber = new(RecipeType.Alternate, "RecycledRubber", "Recycled Rubber", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Plastic), Ingredient.As(6, Item.Fuel)],
            [Product.As(12, Item.Rubber)],
            [Building.Refinery]);

        // PolymerResin
        public static readonly Recipe PolymerResin = new(RecipeType.Alternate, "PolymerResin", "Polymer Resin", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.CrudeOil)],
            [Product.As(13, Item.PolymerResin), Product.As(2, Item.HeavyOilResidue)],
            [Building.Refinery]);

        // PetroleumCoke
        public static readonly Recipe PetroleumCoke = new(RecipeType.Standard, "PetroleumCoke", "Petroleum Coke", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.HeavyOilResidue)],
            [Product.As(12, Item.PetroleumCoke)],
            [Building.Refinery]);

        // HeavyOilResidue
        public static readonly Recipe UnpackageHeavyOilResidue = new(RecipeType.Standard, "UnpackageHeavyOilResidue", "Unpackage Heavy Oil Residue", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedHeavyOilResidue)],
            [Product.As(2, Item.HeavyOilResidue), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        public static readonly Recipe HeavyOilResidue = new(RecipeType.Alternate, "HeavyOilResidue", "Heavy Oil Residue", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CrudeOil)],
            [Product.As(4, Item.HeavyOilResidue), Product.As(2, Item.PolymerResin)],
            [Building.Refinery]);

        // Packaged Heavy Oil Residue
        public static readonly Recipe PackagedHeavyOilResidue = new(RecipeType.Standard, "PackagedHeavyOilResidue", "Packaged Heavy Oil Residue", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.HeavyOilResidue), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedHeavyOilResidue)],
            [Building.Packager]);

        #endregion

        #region Advanced Refinement

        // AluminumScrap
        public static readonly Recipe AluminumScrap = new(RecipeType.Standard, "AluminumScrap", "Aluminum Scrap", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.AluminaSolution), Ingredient.As(2, Item.Coal)],
            [Product.As(6, Item.AluminumScrap), Product.As(2, Item.Water)],
            [Building.Refinery]);

        public static readonly Recipe ElectrodeAluminumScrap = new(RecipeType.Alternate, "ElectrodeAluminumScrap", "Electrode Aluminum Scrap", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(12, Item.AluminaSolution), Ingredient.As(4, Item.PetroleumCoke)],
            [Product.As(20, Item.AluminumScrap), Product.As(7, Item.Water)],
            [Building.Refinery]);

        public static readonly Recipe InstantScrap = new(RecipeType.Alternate, "InstantScrap", "Instant Scrap", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.Bauxite), Ingredient.As(10, Item.Coal), Ingredient.As(5, Item.SulfuricAcid), Ingredient.As(6, Item.Water)],
            [Product.As(30, Item.AluminumScrap), Product.As(5, Item.Water)],
            [Building.Blender]);

        // AluminaSolution
        public static readonly Recipe AluminaSolution = new(RecipeType.Standard, "AluminaSolution", "Alumina Solution", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(12, Item.Bauxite), Ingredient.As(18, Item.Water)],
            [Product.As(12, Item.AluminaSolution), Product.As(5, Item.Silica)],
            [Building.Refinery]);

        public static readonly Recipe SloppyAlumina = new(RecipeType.Alternate, "SloppyAlumina", "Sloppy Alumina", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Bauxite), Ingredient.As(10, Item.Water)],
            [Product.As(12, Item.AluminaSolution)],
            [Building.Refinery]);

        public static readonly Recipe UnpackageAluminaSolution = new(RecipeType.Standard, "UnpackageAluminaSolution", "Unpackage Alumina Solution", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedAluminaSolution)],
            [Product.As(2, Item.AluminaSolution), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        // PackagedAluminaSolution
        public static readonly Recipe PackagedAluminaSolution = new(RecipeType.Standard, "PackagedAluminaSolution", "Packaged Alumina Solution", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.AluminaSolution), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedAluminaSolution)],
            [Building.Packager]);

        // SulfuricAcid
        public static readonly Recipe SulfuricAcid = new(RecipeType.Standard, "SulfuricAcid", "Sulfuric Acid", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Sulfur), Ingredient.As(5, Item.Water)],
            [Product.As(5, Item.SulfuricAcid)],
            [Building.Refinery]);

        public static readonly Recipe UnpackageSulfuricAcid = new(RecipeType.Standard, "UnpackageSulfuricAcid", "Unpackage Sulfuric Acid", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PackagedSulfuricAcid)],
            [Product.As(1, Item.SulfuricAcid), Product.As(1, Item.EmptyCanister)],
            [Building.Packager]);

        // PackagedSulfuricAcid
        public static readonly Recipe PackagedSulfuricAcid = new(RecipeType.Standard, "PackagedSulfuricAcid", "Packaged Sulfuric Acid", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.SulfuricAcid), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedSulfuricAcid)],
            [Building.Packager]);

        // NitricAcid
        public static readonly Recipe NitricAcid = new(RecipeType.Standard, "NitricAcid", "Nitric Acid", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(12, Item.NitrogenGas), Ingredient.As(3, Item.Water), Ingredient.As(1, Item.IronPlate)],
            [Product.As(3, Item.NitricAcid)],
            [Building.Blender]);

        public static readonly Recipe UnpackageNitricAcid = new(RecipeType.Standard, "UnpackageNitricAcid", "Unpackage Nitric Acid", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PackagedNitricAcid)],
            [Product.As(1, Item.NitricAcid), Product.As(1, Item.EmptyFluidTank)],
            [Building.Packager]);

        // PackagedNitricAcid
        public static readonly Recipe PackagedNitricAcid = new(RecipeType.Standard, "PackagedNitricAcid", "Packaged Nitric Acid", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.NitricAcid), Ingredient.As(1, Item.EmptyFluidTank)],
            [Product.As(1, Item.PackagedNitricAcid)],
            [Building.Packager]);

        // Diamonds
        public static readonly Recipe Diamonds = new(RecipeType.Standard, "Diamonds", "Diamonds", ManufacturingTime.Of(2, 1), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(20, Item.Coal)],
            [Product.As(1, Item.Diamonds)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe CloudyDiamonds = new(RecipeType.Alternate, "CloudyDiamonds", "Cloudy Diamonds", ManufacturingTime.Of(3, 99), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(12, Item.Coal), Ingredient.As(24, Item.Limestone)],
            [Product.As(1, Item.Diamonds)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe PinkDiamonds = new(RecipeType.Alternate, "PinkDiamonds", "Pink Diamonds", ManufacturingTime.Of(4, 99), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(8, Item.Coal), Ingredient.As(3, Item.QuartzCrystal)],
            [Product.As(1, Item.Diamonds)],
            [Building.Converter]);

        public static readonly Recipe PetroleumDiamonds = new(RecipeType.Alternate, "PetroleumDiamonds", "Petroleum Diamonds", ManufacturingTime.Of(2, 99), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(24, Item.PetroleumCoke)],
            [Product.As(1, Item.Diamonds)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe OilBasedDiamonds = new(RecipeType.Alternate, "OilBasedDiamonds", "Oil-Based Diamonds", ManufacturingTime.Of(3, 99), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(10, Item.CrudeOil)],
            [Product.As(2, Item.Diamonds)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe TurboDiamonds = new(RecipeType.Alternate, "TurboDiamonds", "Turbo Diamonds", ManufacturingTime.Of(3, 99), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(30, Item.Coal), Ingredient.As(2, Item.PackagedTurbofuel)],
            [Product.As(3, Item.Diamonds)],
            [Building.ParticleAccelerator]);

        // DissolvedSilica

        #endregion

        #region Biomass

        // Fabric
        public static readonly Recipe Fabric = new(RecipeType.Standard, "Fabric", "Fabric", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Mycelia), Ingredient.As(5, Item.Biomass)],
            [Product.As(1, Item.Fabric)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe PolyesterFabric = new(RecipeType.Alternate, "PolyesterFabric", "Polyester Fabric", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PolymerResin), Ingredient.As(1, Item.Water)],
            [Product.As(1, Item.Fabric)],
            [Building.Refinery]);

        // Biomass
        public static readonly Recipe BiomassAlienProtein = new(RecipeType.Standard, "BiomassAlienProtein", "Biomass (Alien Protein)", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.AlienProtein)],
            [Product.As(100, Item.Biomass)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe BiomassLeaves = new(RecipeType.Standard, "BiomassLeaves", "Biomass (Leaves)", ManufacturingTime.Of(5, 0.4m), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Leaves)],
            [Product.As(5, Item.Biomass)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe BiomassMycelia = new(RecipeType.Standard, "BiomassMycelia", "Biomass (Mycelia)", ManufacturingTime.Of(4, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Mycelia)],
            [Product.As(10, Item.Biomass)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe BiomassWood = new(RecipeType.Standard, "BiomassWood", "Biomass (Wood)", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Wood)],
            [Product.As(20, Item.Biomass)],
            [Building.Constructor, Building.CraftBench]);

        // SolidBiofuel
        public static readonly Recipe SolidBiofuel = new(RecipeType.Standard, "SolidBiofuel", "Solid Biofuel", ManufacturingTime.Of(4, 5), VariablePowerConsumption.None(),
            [Ingredient.As(8, Item.Biomass)],
            [Product.As(4, Item.SolidBiofuel)],
            [Building.Constructor, Building.CraftBench]);

        #endregion

        #region Fuel

        // Liquid Biofuel
        public static readonly Recipe LiquidBiofuel = new(RecipeType.Standard, "LiquidBiofuel", "Liquid Biofuel", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.SolidBiofuel), Ingredient.As(3, Item.Water)],
            [Product.As(4, Item.LiquidBiofuel)],
            [Building.Refinery]);

        public static readonly Recipe UnpackageLiquidBiofuel = new(RecipeType.Standard, "UnpackageLiquidBiofuel", "Unpackage Liquid Biofuel", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedLiquidBiofuel)],
            [Product.As(2, Item.LiquidBiofuel), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        // Packaged Liquid Biofuel
        public static readonly Recipe PackagedLiquidBiofuel = new(RecipeType.Standard, "PackagedLiquidBiofuel", "Packaged Liquid Biofuel", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.LiquidBiofuel), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedLiquidBiofuel)],
            [Building.Packager]);

        // Fuel
        public static readonly Recipe Fuel = new(RecipeType.Standard, "Fuel", "Fuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.CrudeOil)],
            [Product.As(4, Item.Fuel), Product.As(3, Item.PolymerResin)],
            [Building.Refinery]);

        public static readonly Recipe ResidualFuel = new(RecipeType.Standard, "ResidualFuel", "Residual Fuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.HeavyOilResidue)],
            [Product.As(4, Item.Fuel)],
            [Building.Refinery]);

        public static readonly Recipe UnpackageFuel = new(RecipeType.Standard, "UnpackageFuel", "Unpackage Fuel", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedFuel)],
            [Product.As(2, Item.Fuel), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        public static readonly Recipe DilutedFuel = new(RecipeType.Alternate, "DilutedFuel", "Diluted Fuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.HeavyOilResidue), Ingredient.As(10, Item.Water)],
            [Product.As(10, Item.Fuel)],
            [Building.Blender]);

        // PackagedFuel
        public static readonly Recipe PackagedFuel = new(RecipeType.Standard, "PackagedFuel", "Packaged Fuel", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Fuel), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedFuel)],
            [Building.Packager]);

        public static readonly Recipe DilutedPackagedFuel = new(RecipeType.Alternate, "DilutedPackagedFuel", "Diluted Packaged Fuel", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.HeavyOilResidue), Ingredient.As(2, Item.PackagedWater)],
            [Product.As(2, Item.PackagedFuel)],
            [Building.Refinery]);

        // TurboFuel
        public static readonly Recipe Turbofuel = new(RecipeType.Standard, "Turbofuel", "Turbofuel", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Fuel), Ingredient.As(4, Item.CompactedCoal)],
            [Product.As(5, Item.Turbofuel)],
            [Building.Refinery]);

        public static readonly Recipe UnpackageTurbofuel = new(RecipeType.Standard, "UnpackageTurbofuel", "Unpackage Turbofuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedTurbofuel)],
            [Product.As(2, Item.Turbofuel), Product.As(2, Item.EmptyCanister)],
            [Building.Packager]);

        public static readonly Recipe TurboBlendFuel = new(RecipeType.Alternate, "TurboBlendFuel", "Turbo Blend Fuel", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Fuel), Ingredient.As(4, Item.HeavyOilResidue), Ingredient.As(3, Item.Sulfur), Ingredient.As(3, Item.PetroleumCoke)],
            [Product.As(6, Item.Turbofuel)],
            [Building.Blender]);

        public static readonly Recipe TurboHeavyFuel = new(RecipeType.Alternate, "TurboHeavyFuel", "Turbo Heavy Fuel", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.HeavyOilResidue), Ingredient.As(4, Item.CompactedCoal)],
            [Product.As(4, Item.Turbofuel)],
            [Building.Refinery]);

        // PackagedTurboFuel
        public static readonly Recipe PackagedTurbofuel = new(RecipeType.Standard, "PackagedTurbofuel", "Packaged Turbofuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Turbofuel), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedTurbofuel)],
            [Building.Packager]);

        // RocketFuel
        public static readonly Recipe RocketFuel = new(RecipeType.Standard, "RocketFuel", "Rocket Fuel", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.Turbofuel), Ingredient.As(1, Item.NitricAcid)],
            [Product.As(10, Item.RocketFuel), Product.As(1, Item.CompactedCoal)],
            [Building.Blender]);

        public static readonly Recipe NitroRocketFuel = new(RecipeType.Alternate, "NitroRocketFuel", "Nitro Rocket Fuel", ManufacturingTime.Of(2.4m, 99), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Fuel), Ingredient.As(3, Item.NitrogenGas), Ingredient.As(4, Item.Sulfur), Ingredient.As(2, Item.Coal)],
            [Product.As(6, Item.RocketFuel), Product.As(1, Item.CompactedCoal)],
            [Building.Blender]);

        public static readonly Recipe UnpackageRocketFuel = new(RecipeType.Standard, "UnpackageRocketFuel", "Unpackage Rocket Fuel", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PackagedRocketFuel)],
            [Product.As(2, Item.RocketFuel), Product.As(1, Item.EmptyFluidTank)],
            [Building.Packager]);

        // PackagedRocketFuel
        public static readonly Recipe PackagedRocketFuel = new(RecipeType.Standard, "PackagedRocketFuel", "Packaged Rocket Fuel", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.RocketFuel), Ingredient.As(1, Item.EmptyFluidTank)],
            [Product.As(1, Item.PackagedRocketFuel)],
            [Building.Packager]);

        // IonizedFuel
        public static readonly Recipe IonizedFuel = new(RecipeType.Standard, "IonizedFuel", "Ionized Fuel", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(16, Item.RocketFuel), Ingredient.As(1, Item.PowerShard)],
            [Product.As(16, Item.IonizedFuel), Product.As(2, Item.CompactedCoal)],
            [Building.Refinery]);

        public static readonly Recipe DarkIonFuel = new(RecipeType.Alternate, "DarkIonFuel", "Dark-Ion Fuel", ManufacturingTime.Of(3, 99), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(12, Item.PackagedRocketFuel), Ingredient.As(4, Item.DarkMatterCrystal)],
            [Product.As(10, Item.IonizedFuel), Product.As(2, Item.CompactedCoal)],
            [Building.Converter]);

        public static readonly Recipe UnpackageIonizedFuel = new(RecipeType.Standard, "UnpackageIonizedFuel", "Unpackage Ionized Fuel", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PackagedIonizedFuel)],
            [Product.As(4, Item.IonizedFuel), Product.As(2, Item.EmptyFluidTank)],
            [Building.Packager]);

        // PackagedIonizedFuel
        public static readonly Recipe PackagedIonizedFuel = new(RecipeType.Standard, "PackagedIonizedFuel", "Packaged Ionized Fuel", ManufacturingTime.Of(3, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.IonizedFuel), Ingredient.As(2, Item.EmptyFluidTank)],
            [Product.As(2, Item.PackagedIonizedFuel)],
            [Building.Packager]);

        #endregion

        #region Containers

        // Empty Canister
        public static readonly Recipe EmptyCanister = new(RecipeType.Standard, "EmptyCanister", "Empty Canister", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Plastic)],
            [Product.As(4, Item.EmptyCanister)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe CoatedIronCanister = new(RecipeType.Alternate, "CoatedIronCanister", "Coated Iron Canister", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronPlate), Ingredient.As(1, Item.CopperSheet)],
            [Product.As(4, Item.EmptyCanister)],
            [Building.Assembler]);

        public static readonly Recipe SteelCanister = new(RecipeType.Alternate, "SteelCanister", "Steel Canister", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.SteelIngot)],
            [Product.As(4, Item.EmptyCanister)],
            [Building.Constructor]);

        // EmptyFluidTank
        public static readonly Recipe EmptyFluidTank = new(RecipeType.Standard, "EmptyFluidTank", "Empty Fluid Tank", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.AluminumIngot)],
            [Product.As(1, Item.EmptyFluidTank)],
            [Building.Constructor, Building.CraftBench]);

        // PressureConversionCube
        public static readonly Recipe PressureConversionCube = new(RecipeType.Standard, "PressureConversionCube", "Pressure Conversion Cube", ManufacturingTime.Of(60, 2), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.FusedModularFrame), Ingredient.As(2, Item.RadioControlUnit)],
            [Product.As(1, Item.PressureConversionCube)],
            [Building.Assembler, Building.CraftBench]);

        // PackagedWater
        public static readonly Recipe PackagedWater = new(RecipeType.Standard, "PackagedWater", "Packaged Water", ManufacturingTime.Of(2, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Water), Ingredient.As(2, Item.EmptyCanister)],
            [Product.As(2, Item.PackagedWater)],
            [Building.Packager]);

        // PackagedNitrogenGas
        public static readonly Recipe PackagedNitrogenGas = new(RecipeType.Standard, "PackagedNitrogenGas", "Packaged Nitrogen Gas", ManufacturingTime.Of(1, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.NitrogenGas), Ingredient.As(1, Item.EmptyFluidTank)],
            [Product.As(1, Item.PackagedNitrogenGas)],
            [Building.Packager]);

        #endregion

        #region Nuclear
        // ElectromagneticControlRod
        public static readonly Recipe ElectromagneticControlRod = new(RecipeType.Standard, "ElectromagneticControlRod", "Electromagnetic Control Rod", ManufacturingTime.Of(30, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Stator), Ingredient.As(2, Item.AILimiter)],
            [Product.As(2, Item.ElectromagneticControlRod)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe ElectromagneticConnectionRod = new(RecipeType.Alternate, "ElectromagneticConnectionRod", "Electromagnetic Connection Rod", ManufacturingTime.Of(15, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Stator), Ingredient.As(1, Item.HighSpeedConnector)],
            [Product.As(2, Item.ElectromagneticControlRod)],
            [Building.Assembler]);

        // EncasedUraniumCell
        public static readonly Recipe EncasedUraniumCell = new(RecipeType.Standard, "EncasedUraniumCell", "Encased Uranium Cell", ManufacturingTime.Of(12, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Uranium), Ingredient.As(3, Item.Concrete), Ingredient.As(8, Item.SulfuricAcid)],
            [Product.As(5, Item.EncasedUraniumCell), Product.As(2, Item.SulfuricAcid)],
            [Building.Blender]);

        public static readonly Recipe InfusedUraniumCell = new(RecipeType.Alternate, "InfusedUraniumCell", "Infused Uranium Cell", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Uranium), Ingredient.As(3, Item.Silica), Ingredient.As(5, Item.Sulfur), Ingredient.As(15, Item.Quickwire)],
            [Product.As(4, Item.EncasedUraniumCell)],
            [Building.Manufacturer]);

        // NonFissileUranium
        public static readonly Recipe NonFissileUranium = new(RecipeType.Standard, "NonFissileUranium", "Non-Fissile Uranium", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.UraniumWaste), Ingredient.As(10, Item.Silica), Ingredient.As(6, Item.NitricAcid), Ingredient.As(6, Item.SulfuricAcid)],
            [Product.As(20, Item.NonFissileUranium), Product.As(6, Item.Water)],
            [Building.Blender]);

        public static readonly Recipe FertileUranium = new(RecipeType.Alternate, "FertileUranium", "Fertile Uranium", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Uranium), Ingredient.As(5, Item.UraniumWaste), Ingredient.As(3, Item.NitricAcid), Ingredient.As(5, Item.SulfuricAcid)],
            [Product.As(20, Item.NonFissileUranium), Product.As(8, Item.Water)],
            [Building.Blender]);

        // UraniumFuelRod
        public static readonly Recipe UraniumFuelRod = new(RecipeType.Standard, "UraniumFuelRod", "Uranium Fuel Rod", ManufacturingTime.Of(150, 1), VariablePowerConsumption.None(),
            [Ingredient.As(50, Item.EncasedUraniumCell), Ingredient.As(3, Item.EncasedIndustrialBeam), Ingredient.As(5, Item.ElectromagneticControlRod)],
            [Product.As(1, Item.UraniumFuelRod)],
            [Building.Manufacturer]);

        public static readonly Recipe UraniumFuelUnit = new(RecipeType.Alternate, "UraniumFuelUnit", "Uranium Fuel Unit", ManufacturingTime.Of(300, 1), VariablePowerConsumption.None(),
            [Ingredient.As(100, Item.EncasedUraniumCell), Ingredient.As(10, Item.ElectromagneticControlRod), Ingredient.As(3, Item.CrystalOscillator), Ingredient.As(10, Item.Rotor)],
            [Product.As(3, Item.UraniumFuelRod)],
            [Building.Manufacturer]);

        // PlutoniumPellet
        public static readonly Recipe PlutoniumPellet = new(RecipeType.Standard, "PlutoniumPellet", "Plutonium Pellet", ManufacturingTime.Of(60, 1), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(100, Item.NonFissileUranium), Ingredient.As(25, Item.UraniumWaste)],
            [Product.As(30, Item.PlutoniumPellet)],
            [Building.ParticleAccelerator]);

        // EncasedPlutoniumCell
        public static readonly Recipe EncasedPlutoniumCell = new(RecipeType.Standard, "EncasedPlutoniumCell", "Encased Plutonium Cell", ManufacturingTime.Of(12, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.PlutoniumPellet), Ingredient.As(4, Item.Concrete)],
            [Product.As(1, Item.EncasedPlutoniumCell)],
            [Building.Assembler, Building.CraftBench]);

        public static readonly Recipe InstantPlutoniumCell = new(RecipeType.Alternate, "InstantPlutoniumCell", "Instant Plutonium Cell", ManufacturingTime.Of(120, 1), VariablePowerConsumption.Of(250, 500),
            [Ingredient.As(150, Item.NonFissileUranium), Ingredient.As(20, Item.AluminumCasing)],
            [Product.As(20, Item.EncasedPlutoniumCell)],
            [Building.ParticleAccelerator]);

        // PlutoniumFuelRod
        public static readonly Recipe PlutoniumFuelRod = new(RecipeType.Standard, "PlutoniumFuelRod", "Plutonium Fuel Rod", ManufacturingTime.Of(240, 1), VariablePowerConsumption.None(),
            [Ingredient.As(30, Item.EncasedPlutoniumCell), Ingredient.As(18, Item.SteelBeam), Ingredient.As(6, Item.ElectromagneticControlRod), Ingredient.As(10, Item.HeatSink)],
            [Product.As(1, Item.PlutoniumFuelRod)],
            [Building.Manufacturer]);

        public static readonly Recipe PlutoniumFuelUnit = new(RecipeType.Alternate, "PlutoniumFuelUnit", "Plutonium Fuel Unit", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.EncasedPlutoniumCell), Ingredient.As(1, Item.PressureConversionCube)],
            [Product.As(1, Item.PlutoniumFuelRod)],
            [Building.Assembler]);

        // Ficsonium
        public static readonly Recipe Ficsonium = new(RecipeType.Standard, "Ficsonium", "Ficsonium", ManufacturingTime.Of(6, 0), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(1, Item.PlutoniumWaste), Ingredient.As(1, Item.SingularityCell), Ingredient.As(20, Item.DarkMatterResidue)],
            [Product.As(1, Item.Ficsonium)],
            [Building.ParticleAccelerator]);

        // FicsoniumFuelRod
        public static readonly Recipe FicsoniumFuelRod = new(RecipeType.Standard, "FicsoniumFuelRod", "Ficsonium Fuel Rod", ManufacturingTime.Of(24, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(2, Item.Ficsonium), Ingredient.As(2, Item.ElectromagneticControlRod), Ingredient.As(40, Item.FicsiteTrigon), Ingredient.As(20, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.FicsoniumFuelRod), Product.As(20, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        #endregion

        #region Quantum Technology

        // AlienPowerMatrix
        public static readonly Recipe AlienPowerMatrix = new(RecipeType.Standard, "AlienPowerMatrix", "Alien Power Matrix", ManufacturingTime.Of(24, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(5, Item.SAMFluctuator), Ingredient.As(3, Item.PowerShard), Ingredient.As(3, Item.SuperpositionOscillator), Ingredient.As(24, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.AlienPowerMatrix), Product.As(24, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        // TimeCrystal
        public static readonly Recipe TimeCrystal = new(RecipeType.Standard, "TimeCrystal", "Time Crystal", ManufacturingTime.Of(10, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(2, Item.Diamonds)],
            [Product.As(1, Item.TimeCrystal)],
            [Building.Converter]);

        // DarkMatterResidue
        public static readonly Recipe DarkMatterResidue = new(RecipeType.Standard, "DarkMatterResidue", "Dark Matter Residue", ManufacturingTime.Of(6, 1), VariablePowerConsumption.Of(100, 300),
            [Ingredient.As(5, Item.ReanimatedSAM)],
            [Product.As(10, Item.DarkMatterResidue)],
            [Building.Converter]);

        // DarkMatterCrystal
        public static readonly Recipe DarkMatterCrystal = new(RecipeType.Standard, "DarkMatterCrystal", "Dark Matter Crystal", ManufacturingTime.Of(2, 1), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(1, Item.Diamonds), Ingredient.As(5, Item.DarkMatterResidue)],
            [Product.As(1, Item.DarkMatterCrystal)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe DarkMatterCrystallization = new(RecipeType.Alternate, "DarkMatterCrystallization", "Dark Matter Crystallization", ManufacturingTime.Of(3, 99), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(10, Item.DarkMatterResidue)],
            [Product.As(1, Item.DarkMatterCrystal)],
            [Building.ParticleAccelerator]);

        public static readonly Recipe DarkMatterTrap = new(RecipeType.Alternate, "DarkMatterTrap", "Dark Matter Trap", ManufacturingTime.Of(2, 99), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(1, Item.TimeCrystal), Ingredient.As(5, Item.DarkMatterResidue)],
            [Product.As(2, Item.DarkMatterCrystal)],
            [Building.ParticleAccelerator]);

        // ExcitedPhotonicMatter
        public static readonly Recipe ExcitedPhotonicMatter = new(RecipeType.Standard, "ExcitedPhotonicMatter", "Excited Photonic Matter", ManufacturingTime.Of(3, 2), VariablePowerConsumption.Of(100, 300),
            [],
            [Product.As(10, Item.ExcitedPhotonicMatter)],
            [Building.Converter]);

        // SuperpositionOscillator
        public static readonly Recipe SuperpositionOscillator = new(RecipeType.Standard, "SuperpositionOscillator", "Superposition Oscillator", ManufacturingTime.Of(12, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(6, Item.DarkMatterCrystal), Ingredient.As(1, Item.CrystalOscillator), Ingredient.As(9, Item.AlcladAluminumSheet), Ingredient.As(25, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.SuperpositionOscillator), Product.As(25, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        // NeuralQuantumProcessor
        public static readonly Recipe NeuralQuantumProcessor = new(RecipeType.Standard, "NeuralQuantumProcessor", "Neural-Quantum Processor", ManufacturingTime.Of(20, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(5, Item.TimeCrystal), Ingredient.As(1, Item.Supercomputer), Ingredient.As(15, Item.FicsiteTrigon), Ingredient.As(25, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.NeuralQuantumProcessor), Product.As(25, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        #endregion

        #region Space Elevator

        // SmartPlating
        public static readonly Recipe SmartPlating = new(RecipeType.Standard, "SmartPlating", "Smart Plating", ManufacturingTime.Of(30, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ReinforcedIronPlate), Ingredient.As(1, Item.Rotor)],
            [Product.As(1, Item.SmartPlating)],
            [Building.Assembler]);

        public static readonly Recipe PlasticSmartPlating = new(RecipeType.Alternate, "PlasticSmartPlating", "Plastic Smart Plating", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ReinforcedIronPlate), Ingredient.As(1, Item.Rotor), Ingredient.As(3, Item.Plastic)],
            [Product.As(2, Item.SmartPlating)],
            [Building.Manufacturer]);

        // Versatile Framework
        public static readonly Recipe VersatileFramework = new(RecipeType.Standard, "VersatileFramework", "Versatile Framework", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ModularFrame), Ingredient.As(12, Item.SteelBeam)],
            [Product.As(2, Item.VersatileFramework)],
            [Building.Assembler]);

        public static readonly Recipe FlexibleFramework = new(RecipeType.Alternate, "FlexibleFramework", "Flexible Framework", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ModularFrame), Ingredient.As(6, Item.SteelBeam), Ingredient.As(8, Item.Rubber)],
            [Product.As(2, Item.VersatileFramework)],
            [Building.Manufacturer]);

        // AutomatedWiring
        public static readonly Recipe AutomatedWiring = new(RecipeType.Standard, "AutomatedWiring", "Automated Wiring", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Stator), Ingredient.As(20, Item.Cable)],
            [Product.As(1, Item.AutomatedWiring)],
            [Building.Assembler]);

        public static readonly Recipe AutomatedSpeedWiring = new(RecipeType.Alternate, "AutomatedSpeedWiring", "Automated Speed Wiring", ManufacturingTime.Of(32, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Stator), Ingredient.As(40, Item.Wire), Ingredient.As(1, Item.HighSpeedConnector)],
            [Product.As(4, Item.AutomatedWiring)],
            [Building.Manufacturer]);

        // ModularEngine
        public static readonly Recipe ModularEngine = new(RecipeType.Standard, "ModularEngine", "Modular Engine", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Motor), Ingredient.As(15, Item.Rubber), Ingredient.As(2, Item.SmartPlating)],
            [Product.As(1, Item.ModularEngine)],
            [Building.Manufacturer]);

        // AdaptiveControlUnit
        public static readonly Recipe AdaptiveControlUnit = new(RecipeType.Standard, "AdaptiveControlUnit", "Adaptive Control Unit", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.AutomatedWiring), Ingredient.As(5, Item.CircuitBoard), Ingredient.As(1, Item.HeavyModularFrame), Ingredient.As(2, Item.Computer)],
            [Product.As(1, Item.AdaptiveControlUnit)],
            [Building.Manufacturer]);

        // AssemblyDirectorSystem
        public static readonly Recipe AssemblyDirectorSystem = new(RecipeType.Standard, "AssemblyDirectorSystem", "Assembly Director System", ManufacturingTime.Of(80, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.AdaptiveControlUnit), Ingredient.As(1, Item.Supercomputer)],
            [Product.As(1, Item.AssemblyDirectorSystem)],
            [Building.Assembler]);

        // MagneticFieldGenerator
        public static readonly Recipe MagneticFieldGenerator = new(RecipeType.Standard, "MagneticFieldGenerator", "Magnetic Field Generator", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.VersatileFramework), Ingredient.As(2, Item.ElectromagneticControlRod)],
            [Product.As(2, Item.MagneticFieldGenerator)],
            [Building.Assembler]);

        // ThermalPropulsionRocket
        public static readonly Recipe ThermalPropulsionRocket = new(RecipeType.Standard, "ThermalPropulsionRocket", "Thermal Propulsion Rocket", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.ModularEngine), Ingredient.As(2, Item.TurboMotor), Ingredient.As(6, Item.CoolingSystem), Ingredient.As(2, Item.FusedModularFrame)],
            [Product.As(2, Item.ThermalPropulsionRocket)],
            [Building.Manufacturer]);

        // NuclearPasta
        public static readonly Recipe NuclearPasta = new(RecipeType.Standard, "NuclearPasta", "Nuclear Pasta", ManufacturingTime.Of(120, 1), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(200, Item.CopperPowder), Ingredient.As(1, Item.PressureConversionCube)],
            [Product.As(1, Item.NuclearPasta)],
            [Building.ParticleAccelerator]);

        // BiochemicalSculptor
        public static readonly Recipe BiochemicalSculptor = new(RecipeType.Standard, "BiochemicalSculptor", "Biochemical Sculptor", ManufacturingTime.Of(120, 1), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(1, Item.AssemblyDirectorSystem), Ingredient.As(80, Item.FicsiteTrigon), Ingredient.As(20, Item.Water)],
            [Product.As(4, Item.BiochemicalSculptor)],
            [Building.Blender]);

        // BallisticWarpDrive
        public static readonly Recipe BallisticWarpDrive = new(RecipeType.Standard, "BallisticWarpDrive", "Ballistic Warp Drive", ManufacturingTime.Of(60, 1), VariablePowerConsumption.Of(500, 1000),
            [Ingredient.As(1, Item.ThermalPropulsionRocket), Ingredient.As(5, Item.SingularityCell), Ingredient.As(2, Item.SuperpositionOscillator), Ingredient.As(40, Item.DarkMatterCrystal)],
            [Product.As(1, Item.BallisticWarpDrive)],
            [Building.Manufacturer]);

        // AIExpansionServer
        public static readonly Recipe AIExpansionServer = new(RecipeType.Standard, "AIExpansionServer", "AI Expansion Server", ManufacturingTime.Of(15, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(1, Item.MagneticFieldGenerator), Ingredient.As(1, Item.NeuralQuantumProcessor), Ingredient.As(1, Item.SuperpositionOscillator), Ingredient.As(25, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.AIExpansionServer), Product.As(25, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        #endregion

        #region Consumables

        // GasFilter
        public static readonly Recipe GasFilter = new(RecipeType.Standard, "GasFilter", "Gas Filter", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.Coal), Ingredient.As(2, Item.IronPlate), Ingredient.As(2, Item.Fabric)],
            [Product.As(1, Item.GasFilter)],
            [Building.Manufacturer, Building.EquipmentWorkshop]);

        // IodineInfusedFilter
        public static readonly Recipe IodineInfusedFilter = new(RecipeType.Standard, "IodineInfusedFilter", "Iodine-Infused Filter", ManufacturingTime.Of(16, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.GasFilter), Ingredient.As(8, Item.Quickwire), Ingredient.As(1, Item.AluminumCasing)],
            [Product.As(1, Item.IodineInfusedFilter)],
            [Building.Manufacturer, Building.EquipmentWorkshop]);

        // MedicinalInhaler
        public static readonly Recipe NutritionalInhaler = new(RecipeType.Standard, "NutritionalInhaler", "Nutritional Inhaler", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.BaconAgaric), Ingredient.As(2, Item.Paleberry), Ingredient.As(5, Item.BerylNut)],
            [Product.As(1, Item.MedicinalInhaler)],
            [Building.EquipmentWorkshop]);

        public static readonly Recipe ProteinInhaler = new(RecipeType.Standard, "ProteinInhaler", "Protein Inhaler", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.AlienProtein), Ingredient.As(10, Item.BerylNut)],
            [Product.As(1, Item.MedicinalInhaler)],
            [Building.EquipmentWorkshop]);

        public static readonly Recipe TherapeuticInhaler = new(RecipeType.Standard, "TherapeuticInhaler", "Therapeutic Inhaler", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.Mycelia), Ingredient.As(1, Item.AlienProtein), Ingredient.As(1, Item.BaconAgaric)],
            [Product.As(1, Item.MedicinalInhaler)],
            [Building.EquipmentWorkshop]);

        public static readonly Recipe VitaminInhaler = new(RecipeType.Standard, "VitaminInhaler", "Vitamin Inhaler", ManufacturingTime.Of(20, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.Mycelia), Ingredient.As(5, Item.Paleberry)],
            [Product.As(1, Item.MedicinalInhaler)],
            [Building.EquipmentWorkshop]);

        #endregion

        #region Tools

        // PortableMiner
        public static readonly Recipe PortableMiner = new(RecipeType.Standard, "PortableMiner", "Portable Miner", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronPlate), Ingredient.As(4, Item.IronRod)],
            [Product.As(1, Item.PortableMiner)],
            [Building.EquipmentWorkshop]);

        public static readonly Recipe AutomatedMiner = new(RecipeType.Alternate, "AutomatedMiner", "Automated Miner", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.SteelPipe), Ingredient.As(4, Item.IronPlate)],
            [Product.As(1, Item.PortableMiner)],
            [Building.Assembler]);

        // Chainsaw
        public static readonly Recipe Chainsaw = new(RecipeType.Standard, "Chainsaw", "Chainsaw", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.ReinforcedIronPlate), Ingredient.As(25, Item.IronRod), Ingredient.As(160, Item.Screws), Ingredient.As(15, Item.Cable)],
            [Product.As(1, Item.Chainsaw)],
            [Building.EquipmentWorkshop]);

        // ObjectScanner
        public static readonly Recipe ObjectScanner = new(RecipeType.Standard, "ObjectScanner", "Object Scanner", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.ReinforcedIronPlate), Ingredient.As(20, Item.Wire), Ingredient.As(50, Item.Screws)],
            [Product.As(1, Item.ObjectScanner)],
            [Building.EquipmentWorkshop]);

        // Zipline
        public static readonly Recipe Zipline = new(RecipeType.Standard, "Zipline", "Zipline", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.XenoZapper), Ingredient.As(30, Item.Quickwire), Ingredient.As(3, Item.IronRod), Ingredient.As(10, Item.Cable)],
            [Product.As(1, Item.Zipline)],
            [Building.EquipmentWorkshop]);

        // FactoryCart
        public static readonly Recipe FactoryCart = new(RecipeType.Standard, "FactoryCart", "Factory Cart™", ManufacturingTime.Of(20, 2), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.ReinforcedIronPlate), Ingredient.As(4, Item.IronRod), Ingredient.As(2, Item.Rotor)],
            [Product.As(1, Item.FactoryCart)],
            [Building.EquipmentWorkshop]);

        // GoldenFactoryCart
        public static readonly Recipe GoldenFactoryCart = new(RecipeType.Standard, "GoldenFactoryCart", "Golden Factory Cart™", ManufacturingTime.Of(20, 2), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.CateriumIngot), Ingredient.As(4, Item.IronRod), Ingredient.As(2, Item.Rotor)],
            [Product.As(1, Item.GoldenFactoryCart)],
            [Building.EquipmentWorkshop]);

        #endregion

        #region Body Equipment

        // Parachute
        public static readonly Recipe Parachute = new(RecipeType.Standard, "Parachute", "Parachute", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.Fabric), Ingredient.As(10, Item.Cable)],
            [Product.As(1, Item.Parachute)],
            [Building.EquipmentWorkshop]);

        // Hoverpack
        public static readonly Recipe Hoverpack = new(RecipeType.Standard, "Hoverpack", "Hoverpack", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(8, Item.Motor), Ingredient.As(4, Item.HeavyModularFrame), Ingredient.As(8, Item.Computer), Ingredient.As(40, Item.AlcladAluminumSheet)],
            [Product.As(1, Item.Hoverpack)],
            [Building.EquipmentWorkshop]);

        // HazmatSuit
        public static readonly Recipe HazmatSuit = new(RecipeType.Standard, "HazmatSuit", "Hazmat Suit", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(50, Item.Rubber), Ingredient.As(50, Item.Plastic), Ingredient.As(50, Item.AlcladAluminumSheet), Ingredient.As(50, Item.Fabric)],
            [Product.As(1, Item.HazmatSuit)],
            [Building.EquipmentWorkshop]);

        // GasMask
        public static readonly Recipe GasMask = new(RecipeType.Standard, "GasMask", "Gas Mask", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.CopperSheet), Ingredient.As(10, Item.SteelPipe), Ingredient.As(50, Item.Fabric)],
            [Product.As(1, Item.GasMask)],
            [Building.EquipmentWorkshop]);

        // Jetpack
        public static readonly Recipe Jetpack = new(RecipeType.Standard, "Jetpack", "Jetpack", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.SteelPipe), Ingredient.As(25, Item.IronPlate), Ingredient.As(50, Item.Wire), Ingredient.As(5, Item.Motor)],
            [Product.As(1, Item.Jetpack)],
            [Building.EquipmentWorkshop]);

        // BladeRunners
        public static readonly Recipe BladeRunners = new(RecipeType.Standard, "BladeRunners", "Blade Runners", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.Silica), Ingredient.As(3, Item.ModularFrame), Ingredient.As(3, Item.Rotor)],
            [Product.As(1, Item.BladeRunners)],
            [Building.EquipmentWorkshop]);

        #endregion

        #region Weapons

        // XenoZapper
        public static readonly Recipe XenoZapper = new(RecipeType.Standard, "XenoZapper", "Xeno-Zapper", ManufacturingTime.Of(40, 1), VariablePowerConsumption.None(),
            [Ingredient.As(10, Item.IronRod), Ingredient.As(2, Item.ReinforcedIronPlate), Ingredient.As(15, Item.Cable), Ingredient.As(50, Item.Wire)],
            [Product.As(1, Item.XenoZapper)],
            [Building.EquipmentWorkshop]);

        // Xeno-basher
        public static readonly Recipe XenoBasher = new(RecipeType.Standard, "XenoBasher", "Xeno-Basher", ManufacturingTime.Of(80, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.ModularFrame), Ingredient.As(2, Item.XenoZapper), Ingredient.As(25, Item.IronRod), Ingredient.As(500, Item.Wire)],
            [Product.As(1, Item.XenoBasher)],
            [Building.EquipmentWorkshop]);

        // RebarGun
        public static readonly Recipe RebarGun = new(RecipeType.Standard, "RebarGun", "Rebar Gun", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.ReinforcedIronPlate), Ingredient.As(16, Item.IronRod), Ingredient.As(100, Item.Screws)],
            [Product.As(1, Item.RebarGun)],
            [Building.EquipmentWorkshop]);

        // Rifle
        public static readonly Recipe Rifle = new(RecipeType.Standard, "Rifle", "Rifle", ManufacturingTime.Of(120, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.Motor), Ingredient.As(10, Item.Rubber), Ingredient.As(25, Item.SteelPipe), Ingredient.As(250, Item.Screws)],
            [Product.As(1, Item.Rifle)],
            [Building.EquipmentWorkshop]);

        // NobeliskDetonator
        public static readonly Recipe NobeliskDetonator = new(RecipeType.Standard, "NobeliskDetonator", "Nobelisk Detonator", ManufacturingTime.Of(80, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.ObjectScanner), Ingredient.As(10, Item.SteelBeam), Ingredient.As(50, Item.Cable)],
            [Product.As(1, Item.NobeliskDetonator)],
            [Building.EquipmentWorkshop]);

        #endregion

        #region Ammunition
        // BlackPowder
        public static readonly Recipe BlackPowder = new(RecipeType.Standard, "BlackPowder", "Black Powder", ManufacturingTime.Of(4, 2), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Coal), Ingredient.As(1, Item.Sulfur)],
            [Product.As(2, Item.BlackPowder)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        public static readonly Recipe FineBlackPowder = new(RecipeType.Alternate, "FineBlackPowder", "Fine Black Powder", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Sulfur), Ingredient.As(2, Item.CompactedCoal)],
            [Product.As(6, Item.BlackPowder)],
            [Building.Assembler]);

        // SmokelessPowder
        public static readonly Recipe SmokelessPowder = new(RecipeType.Standard, "SmokelessPowder", "Smokeless Powder", ManufacturingTime.Of(6, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.BlackPowder), Ingredient.As(1, Item.HeavyOilResidue)],
            [Product.As(2, Item.SmokelessPowder)],
            [Building.Refinery]);

        // IronRebar
        public static readonly Recipe IronRebar = new(RecipeType.Standard, "IronRebar", "Iron Rebar", ManufacturingTime.Of(4, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronRod)],
            [Product.As(1, Item.IronRebar)],
            [Building.Constructor, Building.EquipmentWorkshop]);

        // ExplosiveRebar
        public static readonly Recipe ExplosiveRebar = new(RecipeType.Standard, "ExplosiveRebar", "Explosive Rebar", ManufacturingTime.Of(12, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronRebar), Ingredient.As(2, Item.SmokelessPowder), Ingredient.As(2, Item.SteelPipe)],
            [Product.As(1, Item.ExplosiveRebar)],
            [Building.Manufacturer, Building.EquipmentWorkshop]);

        // ShatterRebar
        public static readonly Recipe ShatterRebar = new(RecipeType.Standard, "ShatterRebar", "Shatter Rebar", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.IronRebar), Ingredient.As(3, Item.QuartzCrystal)],
            [Product.As(1, Item.ShatterRebar)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // StunRebar
        public static readonly Recipe StunRebar = new(RecipeType.Standard, "StunRebar", "Stun Rebar", ManufacturingTime.Of(6, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.IronRebar), Ingredient.As(5, Item.Quickwire)],
            [Product.As(1, Item.StunRebar)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // RifleAmmo
        public static readonly Recipe RifleAmmo = new(RecipeType.Standard, "RifleAmmo", "Rifle Ammo", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.CopperSheet), Ingredient.As(2, Item.SmokelessPowder)],
            [Product.As(15, Item.RifleAmmo)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // HomingRifleAmmo
        public static readonly Recipe HomingRifleAmmo = new(RecipeType.Standard, "HomingRifleAmmo", "Homing Rifle Ammo", ManufacturingTime.Of(24, 0.666667m), VariablePowerConsumption.None(),
            [Ingredient.As(20, Item.RifleAmmo), Ingredient.As(1, Item.HighSpeedConnector)],
            [Product.As(10, Item.HomingRifleAmmo)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // TurboRifleAmmo
        public static readonly Recipe TurboRifleAmmo = new(RecipeType.Standard, "TurboRifleAmmo", "Turbo Rifle Ammo", ManufacturingTime.Of(12, 3.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(25, Item.RifleAmmo), Ingredient.As(3, Item.AluminumCasing), Ingredient.As(3, Item.Turbofuel)],
            [Product.As(50, Item.TurboRifleAmmo)],
            [Building.Blender]);

        public static readonly Recipe TurboRifleAmmoPackaged = new(RecipeType.Standard, "TurboRifleAmmoPackaged", "Turbo Rifle Ammo", ManufacturingTime.Of(12, 3.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(25, Item.RifleAmmo), Ingredient.As(3, Item.AluminumCasing), Ingredient.As(3, Item.PackagedTurbofuel)],
            [Product.As(50, Item.TurboRifleAmmo)],
            [Building.Manufacturer, Building.EquipmentWorkshop]);

        // Nobelisk
        public static readonly Recipe Nobelisk = new(RecipeType.Standard, "Nobelisk", "Nobelisk", ManufacturingTime.Of(6, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.BlackPowder), Ingredient.As(2, Item.SteelPipe)],
            [Product.As(1, Item.Nobelisk)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // ClusterNobelisk
        public static readonly Recipe ClusterNobelisk = new(RecipeType.Standard, "ClusterNobelisk", "Cluster Nobelisk", ManufacturingTime.Of(24, 0.666667m), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.Nobelisk), Ingredient.As(4, Item.SmokelessPowder)],
            [Product.As(1, Item.ClusterNobelisk)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // GasNobelisk
        public static readonly Recipe GasNobelisk = new (RecipeType.Standard, "GasNobelisk", "Gas Nobelisk", ManufacturingTime.Of(12, 0.666667m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.Nobelisk), Ingredient.As(10, Item.Biomass)],
            [Product.As(1, Item.GasNobelisk)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // PulseNobelisk
        public static readonly Recipe PulseNobelisk = new(RecipeType.Standard, "PulseNobelisk", "Pulse Nobelisk", ManufacturingTime.Of(60, 0.666666m), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Nobelisk), Ingredient.As(1, Item.CrystalOscillator)],
            [Product.As(5, Item.PulseNobelisk)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // NukeNobelisk
        public static readonly Recipe NukeNobelisk = new(RecipeType.Standard, "NukeNobelisk", "Nuke Nobelisk", ManufacturingTime.Of(120, 0.5m), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.Nobelisk), Ingredient.As(20, Item.EncasedUraniumCell), Ingredient.As(10, Item.SmokelessPowder), Ingredient.As(6, Item.AILimiter)],
            [Product.As(1, Item.NukeNobelisk)],
            [Building.Manufacturer, Building.EquipmentWorkshop]);
        #endregion

        #region Alien Remains

        // AlienProtein
        public static readonly Recipe HogProtein = new(RecipeType.Standard, "HogProtein", "Hog Protein", ManufacturingTime.Of(3, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.HogRemains)],
            [Product.As(1, Item.AlienProtein)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe HatcherProtein = new(RecipeType.Standard, "HatcherProtein", "Hatcher Protein", ManufacturingTime.Of(3, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.HatcherRemains)],
            [Product.As(1, Item.AlienProtein)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe SpitterProtein = new(RecipeType.Standard, "SpitterProtein", "Spitter Protein", ManufacturingTime.Of(3, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.SpitterRemains)],
            [Product.As(1, Item.AlienProtein)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe StingerProtein = new(RecipeType.Standard, "StingerProtein", "Stinger Protein", ManufacturingTime.Of(3, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.StingerRemains)],
            [Product.As(1, Item.AlienProtein)],
            [Building.Constructor, Building.CraftBench]);

        // AlienDNACapsule
        public static readonly Recipe AlienDNACapsule = new(RecipeType.Standard, "AlienDNACapsule", "Alien DNA Capsule", ManufacturingTime.Of(6, 1.333333m), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.AlienProtein)],
            [Product.As(1, Item.AlienDNACapsule)],
            [Building.Constructor, Building.CraftBench]);

        #endregion

        #region Power Shards

        // PowerShard
        public static readonly Recipe PowerShard1 = new(RecipeType.Standard, "PowerShard1", "Power Shard (1)", ManufacturingTime.Of(8, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.BluePowerSlug)],
            [Product.As(1, Item.PowerShard)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe PowerShard2 = new(RecipeType.Standard, "PowerShard2", "Power Shard (2)", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.YellowPowerSlug)],
            [Product.As(2, Item.PowerShard)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe PowerShard5 = new(RecipeType.Standard, "PowerShard5", "Power Shard (5)", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.PurplePowerSlug)],
            [Product.As(5, Item.PowerShard)],
            [Building.Constructor, Building.CraftBench]);

        public static readonly Recipe SyntheticPowerShard = new(RecipeType.Standard, "SyntheticPowerShard", "Synthetic Power Shard", ManufacturingTime.Of(12, 1), VariablePowerConsumption.Of(0, 2000),
            [Ingredient.As(2, Item.TimeCrystal), Ingredient.As(2, Item.DarkMatterCrystal), Ingredient.As(12, Item.QuartzCrystal), Ingredient.As(12, Item.ExcitedPhotonicMatter)],
            [Product.As(1, Item.PowerShard), Product.As(12, Item.DarkMatterResidue)],
            [Building.QuantumEncoder]);

        #endregion

        #region FICSMAS

        // RedFICSMASOrnament
        public static readonly Recipe RedFICSMASOrnament = new(RecipeType.Standard, "RedFICSMASOrnament", "Red FICSMAS Ornament", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.FICSMASGift)],
            [Product.As(1, Item.RedFICSMASOrnament)],
            [Building.Smelter]);

        // BlueFICSMASOrnament
        public static readonly Recipe BlueFICSMASOrnament = new(RecipeType.Standard, "BlueFICSMASOrnament", "Blue FICSMAS Ornament", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.FICSMASGift)],
            [Product.As(2, Item.BlueFICSMASOrnament)],
            [Building.Smelter]);

        // CopperFICSMASOrnament
        public static readonly Recipe CopperFICSMASOrnament = new(RecipeType.Standard, "CopperFICSMASOrnament", "Copper FICSMAS Ornament", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.RedFICSMASOrnament), Ingredient.As(2, Item.CopperIngot)],
            [Product.As(1, Item.CopperFICSMASOrnament)],
            [Building.Foundry]);

        // IronFICSMASOrnament
        public static readonly Recipe IronFICSMASOrnament = new(RecipeType.Standard, "IronFICSMASOrnament", "Iron FICSMAS Ornament", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.BlueFICSMASOrnament), Ingredient.As(3, Item.IronIngot)],
            [Product.As(1, Item.IronFICSMASOrnament)],
            [Building.Foundry]);

        // FICSMASOrnamentBundle
        public static readonly Recipe FICSMASOrnamentBundle = new(RecipeType.Standard, "FICSMASOrnamentBundle", "FICSMAS Ornament Bundle", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.CopperFICSMASOrnament), Ingredient.As(1, Item.IronFICSMASOrnament)],
            [Product.As(1, Item.FICSMASOrnamentBundle)],
            [Building.Assembler]);

        // FICSMASWreath
        public static readonly Recipe FICSMASWreath = new(RecipeType.Standard, "FICSMASWreath", "FICSMAS Wreath", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(15, Item.FICSMASTreeBranch), Ingredient.As(6, Item.FICSMASOrnamentBundle)],
            [Product.As(2, Item.FICSMASWreath)],
            [Building.Assembler]);

        // FICSMASActualSnow
        public static readonly Recipe FICSMASActualSnow = new(RecipeType.Standard, "FICSMASActualSnow", "FICSMAS Actual Snow", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.FICSMASGift)],
            [Product.As(2, Item.FICSMASActualSnow)],
            [Building.Constructor]);

        // Snowball
        public static readonly Recipe Snowball = new(RecipeType.Standard, "Snowball", "Snowball", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.FICSMASActualSnow)],
            [Product.As(1, Item.Snowball)],
            [Building.Constructor]);

        // FICSMASBow
        public static readonly Recipe FICSMASBow = new(RecipeType.Standard, "FICSMASBow", "FICSMAS Bow", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.FICSMASGift)],
            [Product.As(1, Item.FICSMASBow)],
            [Building.Constructor]);

        // FICSMASTreeBranch
        public static readonly Recipe FICSMASTreeBranch = new(RecipeType.Standard, "FICSMASTreeBranch", "FICSMAS Tree Branch", ManufacturingTime.Of(6, 1), VariablePowerConsumption.None(),
            [Ingredient.As(1, Item.FICSMASGift)],
            [Product.As(1, Item.FICSMASTreeBranch)],
            [Building.Constructor]);

        // FICSMASWonderStar
        public static readonly Recipe FICSMASWonderStar = new(RecipeType.Standard, "FICSMASWonderStar", "FICSMAS Wonder Star", ManufacturingTime.Of(60, 1), VariablePowerConsumption.None(),
            [Ingredient.As(5, Item.FICSMASWreath), Ingredient.As(20, Item.CandyCane)],
            [Product.As(1, Item.FICSMASWonderStar)],
            [Building.Assembler]);

        // CandyCane
        public static readonly Recipe CandyCane = new(RecipeType.Standard, "CandyCane", "Candy Cane", ManufacturingTime.Of(12, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.FICSMASGift)],
            [Product.As(1, Item.CandyCane)],
            [Building.Constructor]);

        // CandyCaneBasher
        public static readonly Recipe CandyCaneBasher = new(RecipeType.Standard, "CandyCaneBasher", "Candy Cane Basher", ManufacturingTime.Of(80, 1), VariablePowerConsumption.None(),
            [Ingredient.As(2, Item.XenoZapper), Ingredient.As(25, Item.CandyCane), Ingredient.As(15, Item.FICSMASGift)],
            [Product.As(1, Item.CandyCaneBasher)],
            [Building.EquipmentWorkshop]);

        // SweetFireworks
        public static readonly Recipe SweetFireworks = new(RecipeType.Standard, "SweetFireworks", "Sweet Fireworks", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(6, Item.FICSMASTreeBranch), Ingredient.As(3, Item.CandyCane)],
            [Product.As(1, Item.SweetFireworks)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // FancyFireworks
        public static readonly Recipe FancyFireworks = new(RecipeType.Standard, "FancyFireworks", "Fancy Fireworks", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(4, Item.FICSMASTreeBranch), Ingredient.As(3, Item.FICSMASBow)],
            [Product.As(1, Item.FancyFireworks)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        // SparklyFireworks
        public static readonly Recipe SparklyFireworks = new(RecipeType.Standard, "SparklyFireworks", "Sparkly Fireworks", ManufacturingTime.Of(24, 1), VariablePowerConsumption.None(),
            [Ingredient.As(3, Item.FICSMASTreeBranch), Ingredient.As(2, Item.FICSMASActualSnow)],
            [Product.As(1, Item.SparklyFireworks)],
            [Building.Assembler, Building.EquipmentWorkshop]);

        #endregion
    }
}
