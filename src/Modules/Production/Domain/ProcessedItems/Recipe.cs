using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public partial class Recipe : ValueObject
    {
        private readonly List<IngredientOld> _ingredients;

        private Recipe(List<IngredientOld> ingredients)
        {
            _ingredients = ingredients;
        }

        public bool HasIngredient(ItemId itemId) => _ingredients.Any(ingredient => ingredient.ItemId == itemId);

        public static Recipe As(List<IngredientOld> items) => new(items);

        // Everything ^^ above should be removed but will refactor that later

        public string Id { get; }

        public string Name { get; }
        public RecipeType Type { get; }
        public List<Ingredient> Ingredients { get; }
        public List<Product> Products { get; }
        public ManufacturingTime ManufacturingTime { get; }
        public List<Building> ProducedIn { get; }
        public VariablePowerConsumption VariablePowerConsumption { get; }

        private Recipe(RecipeType type, string id, string name, ManufacturingTime manufacturingTime, VariablePowerConsumption variablePowerConsumption, List<Ingredient> ingredients, List<Product> products, List<Building> producedIn)
        {
            Type = type;
            Id = id;
            Name = name;
            ManufacturingTime = manufacturingTime;
            VariablePowerConsumption = variablePowerConsumption;
            Ingredients = ingredients;
            Products = products;
            ProducedIn = producedIn;
        }

        /// <summary>
        /// Returns whether the recipe can be automated in a production building or not.
        /// </summary>
        /// <returns>Returns true when the recipe can be automated, and false when it has to be manually crafted.</returns>
        public bool CanBeAutomated() => ProducedIn.Any(building => building.ProductionMethod == ProductionMethod.Automatic);

        public bool ConsumesIngredient(string itemId) => Ingredients.Any(ingredient => ingredient.Item.Id == itemId);

        public bool Produces(string itemId) => Products.Any(product => product.Item.Id == itemId);

        static Recipe()
        {
            All =
            [
                // Resources
                BauxiteCaterium,
                BauxiteCopper,
                CateriumOreCopper,
                CateriumOreQuartz,
                CoalIron,
                CoalLimestone,
                Charcoal,
                Biocoal,
                CopperOreQuartz,
                CopperOreSulfur,
                IronOreLimestone,
                LimestoneSulfur,
                UnpackageWater,
                UnpackageOil,
                NitrogenGasBauxite,
                NitrogenGasCaterium,
                UnpackageNitrogenGas,
                RawQuartzBauxite,
                RawQuartzCoal,
                SulfurCoal,
                SulfurIron,
                UraniumOreBauxite,

                // Ingots
                IronIngot,
                BasicIronIngot,
                IronAlloyIngot,
                PureIronIngot,
                LeachedIronIngot,
                CopperIngot,
                TemperedCopperIngot,
                CopperAlloyIngot,
                PureCopperIngot,
                LeachedCopperIngot,
                SteelIngot,
                CokeSteelIngot,
                CompactedSteelIngot,
                SolidSteelIngot,
                CateriumIngot,
                TemperedCateriumIngot,
                PureCateriumIngot,
                LeachedCateriumIngot,
                AluminumIngot,
                PureAluminumIngot,
                FicsiteIngotIron,
                FicsiteIngotCaterium,
                FicsiteIngotAluminum,

                // Standard Parts
                IronRod,
                AluminumRod,
                SteelRod,
                IronPlate,
                CoatedIronPlate,
                SteelCastPlate,
                ReinforcedIronPlate,
                AdheredIronPlate,
                BoltedIronPlate,
                StitchedIronPlate,
                Screws,
                CastScrews,
                SteelScrews,
                ModularFrame,
                BoltedFrame,
                SteeledFrame,
                HeavyModularFrame,
                HeavyEncasedFrame,
                HeavyFlexibleFrame,
                FusedModularFrame,
                HeatFusedFrame,
                SteelBeam,
                MoldedBeam,
                AluminumBeam,
                SteelPipe,
                MoldedSteelPipe,
                IronPipe,
                EncasedIndustrialBeam,
                EncasedIndustrialPipe,
                CopperSheet,
                SteamedCopperSheet,
                AluminumCasing,
                AlcladCasing,
                AlcladAluminumSheet,
                FicsiteTrigon,

                // Industrial Parts
                Rotor,
                CopperRotor,
                SteelRotor,
                Stator,
                QuickwireStator,
                Motor,
                ElectricMotor,
                RigorMotor,
                HeatSink,
                HeatExchanger,
                CoolingSystem,
                CoolingDevice,
                TurboMotor,
                TurboElectricMotor,
                TurboPressureMotor,
                Battery,
                ClassicBattery,

                // Compounds
                Concrete,
                FineConcrete,
                RubberConcrete,
                WetConcrete,
                QuartzCrystal,
                PureQuartzCrystal,
                QuartzPurification,
                FusedQuartzCrystal,
                Silica,
                CheapSilica,
                DistilledSilica,
                CompactedCoal,
                CopperPowder,

                // Electronics
                Wire,
                CateriumWire,
                FusedWire,
                IronWire,
                Cable,
                CoatedCable,
                InsulatedCable,
                QuickwireCable,
                CircuitBoard,
                CateriumCircuitBoard,
                ElectrodeCircuitBoard,
                SiliconCircuitBoard,
                HighSpeedConnector,
                SiliconHighSpeedConnector,
                AILimiter,
                PlasticAILimiter,
                Quickwire,
                FusedQuickwire,
                SingularityCell,
                ReanimatedSAM,
                SAMFluctuator,

                // Communications
                Computer,
                CateriumComputer,
                CrystalComputer,
                Supercomputer,
                OCSupercomputer,
                SuperStateComputer,
                RadioControlUnit,
                RadioConnectionUnit,
                RadioControlSystem,
                CrystalOscillator,
                InsulatedCrystalOscillator,

                // Oil Products
                PackagedOil,
                Plastic,
                RecycledPlastic,
                ResidualPlastic,
                Rubber,
                ResidualRubber,
                RecycledRubber,
                PolymerResin,
                PetroleumCoke,
                UnpackageHeavyOilResidue,
                HeavyOilResidue,
                PackagedHeavyOilResidue,

                // Advanced Refinement
                AluminumScrap,
                ElectrodeAluminumScrap,
                InstantScrap,
                AluminaSolution,
                SloppyAlumina,
                UnpackageAluminaSolution,
                PackagedAluminaSolution,
                SulfuricAcid,
                UnpackageSulfuricAcid,
                PackagedSulfuricAcid,
                NitricAcid,
                UnpackageNitricAcid,
                PackagedNitricAcid,
                Diamonds,
                CloudyDiamonds,
                PinkDiamonds,
                PetroleumDiamonds,
                OilBasedDiamonds,
                TurboDiamonds,

                // Biomass
                Fabric,
                PolyesterFabric,
                BiomassAlienProtein,
                BiomassLeaves,
                BiomassMycelia,
                BiomassWood,
                SolidBiofuel,

                // Fuel
                LiquidBiofuel,
                UnpackageLiquidBiofuel,
                PackagedLiquidBiofuel,
                Fuel,
                ResidualFuel,
                UnpackageFuel,
                DilutedFuel,
                PackagedFuel,
                DilutedPackagedFuel,
                Turbofuel,
                UnpackageTurbofuel,
                TurboBlendFuel,
                TurboHeavyFuel,
                PackagedTurbofuel,
                RocketFuel,
                NitroRocketFuel,
                UnpackageRocketFuel,
                PackagedRocketFuel,
                IonizedFuel,
                DarkIonFuel,
                UnpackageIonizedFuel,
                PackagedIonizedFuel,

                // Containers
                EmptyCanister,
                CoatedIronCanister,
                SteelCanister,
                EmptyFluidTank,
                PressureConversionCube,
                PackagedWater,
                PackagedNitrogenGas,

                // Nuclear
                ElectromagneticControlRod,
                ElectromagneticConnectionRod,
                EncasedUraniumCell,
                InfusedUraniumCell,
                NonFissileUranium,
                FertileUranium,
                UraniumFuelRod,
                UraniumFuelUnit,
                PlutoniumPellet,
                EncasedPlutoniumCell,
                InstantPlutoniumCell,
                PlutoniumFuelRod,
                PlutoniumFuelUnit,
                Ficsonium,
                FicsoniumFuelRod,

                // Quantum Technology
                AlienPowerMatrix,
                TimeCrystal,
                DarkMatterResidue,
                DarkMatterCrystal,
                DarkMatterCrystallization,
                DarkMatterTrap,
                ExcitedPhotonicMatter,
                SuperpositionOscillator,
                NeuralQuantumProcessor,

                // Space Elevator
                SmartPlating,
                PlasticSmartPlating,
                VersatileFramework,
                FlexibleFramework,
                AutomatedWiring,
                AutomatedSpeedWiring,
                ModularEngine,
                AdaptiveControlUnit,
                AssemblyDirectorSystem,
                MagneticFieldGenerator,
                ThermalPropulsionRocket,
                NuclearPasta,
                BallisticWarpDrive,
                BiochemicalSculptor,
                AIExpansionServer,

                // Consumables
                GasFilter,
                IodineInfusedFilter,
                NutritionalInhaler,
                ProteinInhaler,
                TherapeuticInhaler,
                VitaminInhaler,

                // Tools
                PortableMiner,
                AutomatedMiner,
                Chainsaw,
                ObjectScanner,
                Zipline,
                FactoryCart,
                GoldenFactoryCart,

                // Body Equipment
                Parachute,
                Hoverpack,
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

                // Ammunition
                BlackPowder,
                FineBlackPowder,
                SmokelessPowder,
                IronRebar,
                ExplosiveRebar,
                ShatterRebar,
                StunRebar,
                RifleAmmo,
                HomingRifleAmmo,
                TurboRifleAmmo,
                TurboRifleAmmoPackaged,
                Nobelisk,
                ClusterNobelisk,
                GasNobelisk,
                PulseNobelisk,
                NukeNobelisk,

                // Alien Remains
                HogProtein,
                HatcherProtein,
                SpitterProtein,
                StingerProtein,
                AlienDNACapsule,

                // Power Shards
                PowerShard1,
                PowerShard2,
                PowerShard5,
                SyntheticPowerShard,

                // FICSMAS
                RedFICSMASOrnament,
                BlueFICSMASOrnament,
                CopperFICSMASOrnament,
                IronFICSMASOrnament,
                FICSMASOrnamentBundle,
                FICSMASWreath,
                FICSMASActualSnow,
                Snowball,
                FICSMASBow,
                FICSMASTreeBranch,
                FICSMASWonderStar,
                CandyCane,
                CandyCaneBasher,
                SweetFireworks,
                FancyFireworks,
                SparklyFireworks
            ];
        }

        public static readonly List<Recipe> All;
    }
}