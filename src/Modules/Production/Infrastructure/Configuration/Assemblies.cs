﻿using MediatR;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of our Application project.
        /// </summary>
        internal static readonly Assembly Application = typeof(InternalCommandBase).Assembly;

        /// <summary>
        ///     The assembly of our Infrastructure project.
        /// </summary>
        internal static readonly Assembly Infrastructure = typeof(ProductionContext).Assembly;

        /// <summary>
        ///     The assembly of the MediatR library.
        /// </summary>
        internal static readonly Assembly Mediatr = typeof(IMediator).GetTypeInfo().Assembly;
    }
}