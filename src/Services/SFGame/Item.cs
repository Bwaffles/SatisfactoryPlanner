using Newtonsoft.Json;
using Services.SFGame.Models.DocExtraction;

namespace Services.SFGame
{
    public class Item
    {
        [JsonProperty("category")]
        public ItemType Type { get; set; }

        [JsonProperty("code")]
        public string ClassName { get; set; }

        /// <summary>
        /// Used to get the resource name in blueprints
        /// </summary>
        [JsonProperty("name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Used to get the resource description in blueprints.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The number of items of a certain type we can stack in one inventory slot.
        /// </summary>
        [JsonProperty("stack_size")]
        public StackSize StackSize { get; set; }

        /// <summary>
        /// Returns if this item can be discarded.
        /// </summary>
        [JsonProperty("can_be_discarded")]
        public bool CanBeDiscarded { get; set; }

        /// <summary>
        /// Returns if we should store if this item ever has been picked up.
        /// </summary>
        [JsonProperty("remember_pickup")]
        public bool RememberPickUp { get; set; }

        /// <summary>
        /// Energy value for this resource if used as fuel.
        /// </summary>
        [JsonProperty("energy_value")]
        public decimal EnergyValue { get; set; }

        /// <summary>
        /// How much radiation this item gives out, 0 means it's not radioactive.
        /// </summary>
        [JsonProperty("radioactive_decay")]
        public decimal RadioactiveDecay { get; set; }

        [JsonProperty("form")]
        public ResourceForm Form { get; set; }

        /// <summary>
        /// The small icon of the item.
        /// </summary>
        [JsonProperty("icon")]
        public string SmallIcon { get; }

        [JsonProperty("resource_sink_points")]
        public long ResourceSinkPoints { get; set; }

        [JsonProperty("health_gain")]
        public decimal HealthGain { get; internal set; }
    }
}
