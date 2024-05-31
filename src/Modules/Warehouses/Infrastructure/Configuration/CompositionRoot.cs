using Autofac;

namespace SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration
{
    internal static class CompositionRoot
    {
        private static IContainer? _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container!.BeginLifetimeScope();
    }
}
