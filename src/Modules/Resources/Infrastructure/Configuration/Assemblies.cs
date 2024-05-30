using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of our Resources.Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(IResourcesModule).Assembly;

        /// <summary>
        ///     The assembly of our Resources.Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(ResourcesContext).Assembly;
    }
}