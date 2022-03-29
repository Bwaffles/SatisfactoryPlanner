using System.ComponentModel;

namespace Services.SFGame.Models.DocExtraction
{
    public enum SchematicType
    {
        [Description("Custom")]
        EST_Custom,
        [Description("Cheat")]
        EST_Cheat,
        [Description("Tutorial")]
        EST_Tutorial,
        [Description("Milestone")]
        EST_Milestone,
        [Description("Alternate")]
        EST_Alternate,
        [Description("Story")]
        EST_Story,
        [Description("MAM")]
        EST_MAM,
        [Description("Resource Sink")]
        EST_ResourceSink,
        [Description("Hard Drive")]
        EST_HardDrive,
        [Description("Prototype")]
        EST_Prototype
    };
}
