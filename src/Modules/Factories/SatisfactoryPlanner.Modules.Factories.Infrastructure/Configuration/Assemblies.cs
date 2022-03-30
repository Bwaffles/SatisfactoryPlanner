using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Commands;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}
