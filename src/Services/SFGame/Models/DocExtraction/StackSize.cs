using System.ComponentModel;

namespace Services.SFGame.Models.DocExtraction
{
    /// <summary>
    /// Stack size for items.
    /// </summary>
    public enum StackSize
    {
        [Description("N/A")]
        UNKNOWN = 0,
        [Description("One")]
        SS_ONE = 1,
        [Description("Small")]
        SS_SMALL = 50,
        [Description("Medium")]
        SS_MEDIUM = 100,
        [Description("Big")]
        SS_BIG = 200,
        [Description("Huge")]
        SS_HUGE = 500,
        [Description("Fluid")]
        SS_FLUID = 600
    }
}
