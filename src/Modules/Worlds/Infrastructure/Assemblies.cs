using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of our Worlds.Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(IWorldsModule).Assembly;

        /// <summary>
        ///     The assembly of our Worlds.Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(WorldsContext).Assembly;
    }
}