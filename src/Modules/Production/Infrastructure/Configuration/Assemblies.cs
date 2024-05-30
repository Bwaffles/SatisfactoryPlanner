using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of the Production.Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(IProductionModule).Assembly;

        /// <summary>
        ///     The assembly of the Production.Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(ProductionContext).Assembly;
    }
}