using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration
{
    /// <summary>
    ///     Helper class to find assemblies in this module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        ///     The assembly of the UserAccess.Application project.
        /// </summary>
        public static readonly Assembly Application = typeof(IUserAccessModule).Assembly;
    }
}