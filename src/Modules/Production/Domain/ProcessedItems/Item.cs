using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public partial class Item : ValueObject
    {
        public ItemCategory Category { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// Used to get the resource name in blueprints
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used to get the resource description in blueprints.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The number of items of a certain type we can stack in one inventory slot.
        /// </summary>
        public StackSize StackSize { get; set; }

        /// <summary>
        /// Energy value for this resource if used as fuel.
        /// </summary>
        public decimal EnergyValue { get; set; }

        /// <summary>
        /// How much radiation this item gives out, 0 means it's not radioactive.
        /// </summary>
        public decimal RadioactiveDecay { get; set; }

        public ResourceForm Form { get; set; }

        public long ResourceSinkPoints { get; set; }

        public decimal HealthGain { get; internal set; }

        private Item(ItemCategory subCategory, string id, string name, StackSize stackSize, ResourceForm form, long resourceSinkPoints, decimal energyValue, decimal radioactiveDecay,
            decimal healthGain, string description)
        {
            Category = subCategory;
            Id = id;
            Name = name;
            Form = form;
            StackSize = stackSize;
            ResourceSinkPoints = resourceSinkPoints;
            EnergyValue = energyValue;
            RadioactiveDecay = radioactiveDecay;
            HealthGain = healthGain;
            Description = description;
        }

        static Item()
        {
            All =
            [
                // Resources
                IronOre,
                CopperOre,
                Limestone,
                Coal,
                Sulfur,
                RawQuartz,
                CateriumOre,
                Bauxite,
                Uranium,
                Water,
                CrudeOil,
                NitrogenGas,

                // Ingots
                IronIngot,
                CopperIngot,
                SteelIngot,
                CateriumIngot,
                AluminumIngot,

                // Standard Parts
                IronRod,
                IronPlate,
                ReinforcedIronPlate,
                Screw,
                ModularFrame,
                HeavyModularFrame,
                FusedModularFrame,
                SteelBeam,
                SteelPipe,
                EncasedIndustrialBeam,
                CopperSheet,
                AluminumCasing,
                AlcladAluminumSheet,

                // Industrial Parts
                Rotor,
                Stator,
                Motor,
                HeatSink,
                CoolingSystem,
                TurboMotor,
                Battery,

                // Compounds
                Concrete,
                QuartzCrystal,
                Silica,
                CompactedCoal,
                CopperPowder,

                // Electronics
                Cable,
                Wire,
                CircuitBoard,
                HighSpeedConnector,
                AILimiter,
                Quickwire,

                // Communications
                Computer,
                Supercomputer,
                RadioControlUnit,
                CrystalOscillator,
                
                // Oil Products
                PackagedOil,
                Plastic,
                Rubber,
                PolymerResin,
                PetroleumCoke,
                HeavyOilResidue,
                PackagedHeavyOilResidue,
                
                // Advanced Refinement
                AluminumScrap,
                AluminaSolution,
                PackagedAluminaSolution,
                SulfuricAcid,
                PackagedSulfuricAcid,
                NitricAcid,
                PackagedNitricAcid,
                
                // Biomass
                ColorCartridge,
                Fabric,
                FlowerPetals,
                Wood,
                Mycelia,
                Leaves,
                Biomass,
                SolidBiofuel,
                
                // Fuel
                LiquidBiofuel,
                PackagedLiquidBiofuel,
                Fuel,
                PackagedFuel,
                Turbofuel,
                PackagedTurbofuel,
                
                // Containers
                EmptyCanister,
                EmptyFluidTank,
                PressureConversionCube,
                PackagedWater,
                PackagedNitrogenGas,
                
                // Nuclear
                ElectromagneticControlRod,
                EncasedUraniumCell,
                NonFissileUranium,
                UraniumFuelRod,
                UraniumWaste,
                PlutoniumPellet,
                EncasedPlutoniumCell,
                PlutoniumFuelRod,
                PlutoniumWaste,

                // Space Elevator
                SmartPlating,
                VersatileFramework,
                AutomatedWiring,
                ModularEngine,
                AdaptiveControlUnit,
                AssemblyDirectorSystem,
                MagneticFieldGenerator,
                ThermalPropulsionRocket,
                NuclearPasta,

                // Consumables
                GasFilter,
                IodineInfusedFilter,
                BerylNut,
                MedicinalInhaler,
                BaconAgaric,
                Paleberry,

                // Tools
                Beacon,
                PortableMiner,
                Chainsaw,
                ObjectScanner,
                Zipline,
                FactoryCart,
                GoldenFactoryCart,
                
                // Body Equipment
                Parachute,
                HoverPack,
                HazmatSuit,
                GasMask,
                Jetpack,
                BladeRunners,

                // Weapons
                XenoZapper,
                XenoBasher,
                RebarGun,
                Rifle,
                NobeliskDetonator,

                //Ammunition
                BlackPowder,
                SmokelessPowder,
                // TODO more ammo needed

                // Alien Remains
                HatcherRemains,
                HogRemains,
                PlasmaSpitterRemains,
                StingerRemains,
                AlienProtein,
                AlienDNACapsule,

                // Power Shards
                BluePowerSlug,
                YellowPowerSlug,
                PurplePowerSlug,
                PowerShard,

                // FICSMAS
                FICSMASWonderStar,
                FICSMASOrnamentBundle,
                FICSMASDecoration,
                CandyCane,
                ActualSnow,
                FICSMASBow,
                CopperFICSMASOrnament,
                IronFICSMASOrnament,
                FICSMASTreeBranch,
                RedFICSMASOrnament,
                BlueFICSMASOrnament,
                FICSMASGift,
                CandyCaneBasher,

                // Other
                HUBParts
            ];
        }

        public static readonly List<Item> All;
    }
}
