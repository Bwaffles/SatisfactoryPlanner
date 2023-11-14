using Autofac;

namespace SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration
{
    internal static class WorldsCompositionRoot
    {
        private static IContainer? _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container!.BeginLifetimeScope();
    }
}