using Services.SFGame.Models.DocExtraction;

namespace Services.SFGame
{
    public class Schematic
    {
        public string ClassName { get; set; }
        public string FullName { get; set; }

        /// <summary>
        /// Returns the type of schematic.
        /// </summary>
        public SchematicType Type { get; set; }

        /// <summary>
        /// Returns the description of this schematic.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Returns the display name of this schematic.
        /// </summary>
        public string DisplayName { get; set; }
        public string SubCategories { get; internal set; }
        public decimal MenuPriority { get; internal set; }
        public long TechTier { get; internal set; }
        public string Cost { get; internal set; } // TODO split to list of 
        public decimal TimeToComplete { get; internal set; }
        public string RelevantShopSchematics { get; internal set; }
        public string RelevantEvents { get; internal set; }
        public string SchematicIcon { get; internal set; }
        public string SmallSchematicIcon { get; internal set; }
        public string IncludeInBuilds { get; internal set; }
    }
}
