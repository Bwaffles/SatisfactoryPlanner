using MediatR;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Commands;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of our Pioneers.Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(InternalCommandBase).Assembly;

        /// <summary>
        ///     The assembly of our Pioneers.Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(PioneersContext).Assembly;

        /// <summary>
        ///     The assembly of the MediatR library.
        /// </summary>
        internal static readonly Assembly Mediatr = typeof(IMediator).GetTypeInfo().Assembly;
    }
}