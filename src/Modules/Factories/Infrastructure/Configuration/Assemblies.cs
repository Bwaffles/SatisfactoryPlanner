using MediatR;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Commands;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of our Factories.Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(InternalCommandBase).Assembly;

        /// <summary>
        ///     The assembly of our Factories.Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(FactoriesContext).Assembly;

        /// <summary>
        ///     The assembly of the MediatR library.
        /// </summary>
        internal static readonly Assembly Mediatr = typeof(IMediator).GetTypeInfo().Assembly;
    }
}