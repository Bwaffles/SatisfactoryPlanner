using System.ComponentModel;

namespace Services.SFGame.Models.DocExtraction
{
    /// <summary>
    /// The form this item is in, i.e. does it require pipes or conveyors, can the player pick it up etc.
    /// </summary>
    public enum ResourceForm
    {
        [Description("Solid")]
        RF_SOLID,
        [Description("Liquid")]
        RF_LIQUID,
        [Description("Gas")]
        RF_GAS,
        [Description("Heat")]
        RF_HEAT,
        [Description("Invalid")]
        RF_INVALID
    }
}
