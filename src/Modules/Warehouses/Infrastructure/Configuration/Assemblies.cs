using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of the Warehouses.Application project.
        /// </summary>
        public static readonly Assembly Application = typeof(IWarehousesModule).Assembly;

        /// <summary>
        ///     The assembly of the Warehouses.Infrastructure project.
        /// </summary>
        public static readonly Assembly Infrastructure = typeof(WarehousesStartup).Assembly;
    }
}
