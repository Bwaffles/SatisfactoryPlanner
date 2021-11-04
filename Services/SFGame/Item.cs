using Services.SFGame.Models.DocExtraction;

namespace Services.SFGame
{
    public class Item
    {
        public ItemCategory Category { get; set; }

        public string ClassName { get; set; }

        /// <summary>
        /// Get the item name from this class (Desc_IronScrew_C, becomes IronScrew).
        /// </summary>
        public string ItemName { get; }

        /// <summary>
        /// Used to get the resource name in blueprints
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Used to get the resource description in blueprints.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Used to get the abbreviated name of the item in blueprints.
        /// </summary>
        public string AbbreviatedDisplayName { get; set; }

        /// <summary>
        /// The number of items of a certain type we can stack in one inventory slot.
        /// </summary>
        public StackSize StackSize { get; set; }

        /// <summary>
        /// Returns if this item can be discarded.
        /// </summary>
        public bool CanBeDiscarded { get; set; }

        /// <summary>
        /// Returns if we should store if this item ever has been picked up.
        /// </summary>
        public bool RememberPickUp { get; set; }

        /// <summary>
        /// Energy value for this resource if used as fuel.
        /// </summary>
        public decimal EnergyValue { get; set; }

        /// <summary>
        /// How much radiation this item gives out, 0 means it's not radioactive.
        /// </summary>
        public decimal RadioactiveDecay { get; set; }

        public ResourceForm Form { get; set; }

        /// <summary>
        /// The small icon of the item.
        /// </summary>
        public string SmallIcon { get; set; }

        /// <summary>
        /// The big icon of the item.
        /// </summary>
        public string BigIcon { get; set; }

        /// <summary>
        /// Returns the color of this is a fluid.
        /// </summary>
        public string FluidColor { get; set; } // TODO convert to color

        /// <summary>
        /// Returns the color of this is a gas.
        /// </summary>
        public string GasColor { get; set; } // TODO convert to color

        public long ResourceSinkPoints { get; set; }
    }
}
