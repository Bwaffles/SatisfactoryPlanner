using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems
{
    public class Building : ValueObject
    {
        public static readonly Building CraftBench = new("CraftBench", "Craft Bench", ProductionMethod.Manual);
        public static readonly Building EquipmentWorkshop = new("EquipmentWorkshop", "Equipment Workshop", ProductionMethod.Manual);
        public static readonly Building Smelter = new("Smelter", "Smelter", ProductionMethod.Automatic);
        public static readonly Building Foundry = new("Foundry", "Foundry", ProductionMethod.Automatic);
        public static readonly Building Constructor = new("Constructor", "Constructor", ProductionMethod.Automatic);
        public static readonly Building Assembler = new("Assembler", "Assembler", ProductionMethod.Automatic);
        public static readonly Building Refinery = new("Refinery", "Refinery", ProductionMethod.Automatic);
        public static readonly Building Packager = new("Packager", "Packager", ProductionMethod.Automatic);
        public static readonly Building Manufacturer = new("Manufacturer", "Manufacturer", ProductionMethod.Automatic);
        public static readonly Building Blender = new("Blender", "Blender", ProductionMethod.Automatic);
        public static readonly Building ParticleAccelerator = new("ParticleAccelerator", "Particle Accelerator", ProductionMethod.Automatic);

        public string Id { get; }

        public string Name { get; }

        public ProductionMethod ProductionMethod { get; }

        private Building(string id, string name, ProductionMethod productionMethod)
        {
            Id = id;
            Name = name;
            ProductionMethod = productionMethod;
        }
    }
}