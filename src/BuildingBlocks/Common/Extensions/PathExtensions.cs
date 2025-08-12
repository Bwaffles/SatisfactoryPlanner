using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;

namespace SatisfactoryPlanner.BuildingBlocks.Common.Extensions
{
    public static class PathExtensions
    {
        public static string GetAppDataFolder(this IAppFolderInfo appFolderInfo) => appFolderInfo.AppDataFolder;

        public static string GetLogFolder(this IAppFolderInfo appFolderInfo) => Path.Combine(GetAppDataFolder(appFolderInfo), "logs");
    }
}
