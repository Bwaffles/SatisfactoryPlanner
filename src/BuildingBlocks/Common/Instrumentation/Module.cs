namespace SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation
{
    /// <summary>
    /// The modules used in the app. It includes not only the actual modules, but also other general areas like Api, ConsoleApp etc.
    /// </summary>
    /// <remarks>It seems a tad odd to call this Module but include things that aren't in the Modules folder.
    /// Maybe it's worthwhile to rename this to something more generic to encompass all areas I intend to log in.</remarks>
    public enum Module
    {
        ConsoleApp,
        Api
    }
}
