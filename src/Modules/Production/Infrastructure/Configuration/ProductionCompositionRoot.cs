using Autofac;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration
{
    internal static class ProductionCompositionRoot
    {
        private static IContainer? _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container!.BeginLifetimeScope();
    }
}