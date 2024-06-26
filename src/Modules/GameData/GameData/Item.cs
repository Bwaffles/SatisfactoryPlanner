namespace SatisfactoryPlanner.Modules.GameData.GameData
{
    public partial class Item
    {
        public ItemCategory Category { get; }

        public string Id { get; }

        /// <summary>
        /// Used to get the resource name in blueprints
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Used to get the resource description in blueprints.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The number of items of a certain type we can stack in one inventory slot.
        /// </summary>
        public StackSize StackSize { get; }

        /// <summary>
        /// Energy value for this resource if used as fuel.
        /// </summary>
        public decimal EnergyValue { get; }

        /// <summary>
        /// How much radiation this item gives out, 0 means it's not radioactive.
        /// </summary>
        public decimal RadioactiveDecay { get; }

        public ResourceForm Form { get; }

        public long ResourceSinkPoints { get; }

        public decimal HealthGain { get; }

        private Item(ItemCategory category, string id, string name, StackSize stackSize, ResourceForm form, long resourceSinkPoints, decimal energyValue, decimal radioactiveDecay,
            decimal healthGain, string description)
        {
            Category = category;
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
                Wire,
                Cable,
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
                IronRebar,
                ExplosiveRebar,
                ShatterRebar,
                StunRebar,
                RifleAmmo,
                HomingRifleAmmo,
                TurboRifleAmmo,
                Nobelisk,
                ClusterNobelisk,
                GasNobelisk,
                NukeNobelisk,
                PulseNobelisk,

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
                FICSMASGift,
                RedFICSMASOrnament,
                BlueFICSMASOrnament,
                CopperFICSMASOrnament,
                IronFICSMASOrnament,
                FICSMASOrnamentBundle,
                FICSMASDecoration,
                ActualSnow,
                Snowball,
                FICSMASBow,
                FICSMASTreeBranch,
                FICSMASWonderStar,
                CandyCane,
                CandyCaneBasher,
                SweetFireworks,
                FancyFireworks,
                SparklyFireworks,

                // Other
                HUBParts
            ];
        }

        public static readonly List<Item> All;

        /// <summary>
        /// Get an <see cref="Item"/> by the given <paramref name="id"/>.
        /// </summary>
        /// <returns>The found <see cref="Item"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when there is no item with that id.</exception>
        public static Item GetById(string id)
        {
            var item = All.SingleOrDefault(item => item.Id == id);
            if (item == null)
                throw new ArgumentException("Id does not match a known item", nameof(id));

            return item;
        }
    }
}
