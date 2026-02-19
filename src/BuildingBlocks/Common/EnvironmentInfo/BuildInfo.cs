using System.Reflection;

namespace SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo
{
    public static class BuildInfo
    {
        static BuildInfo()
        {
            var assembly = Assembly.GetExecutingAssembly();

            Version = assembly.GetName().Version ?? new Version();
        }

        public static string AppName { get; } = "Satisfactory Planner";

        public static Version Version { get; }

        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
