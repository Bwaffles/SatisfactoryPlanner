using SatisfactoryPlanner.UserAccess.Application.Contracts;
using System.Reflection;

namespace SatisfactoryPlanner.UserAccess.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(CommandBase).Assembly;
    }
}
