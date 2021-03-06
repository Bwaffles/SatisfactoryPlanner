using Autofac;

namespace SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration
{
    internal static class FactoriesCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _container.BeginLifetimeScope();
        }
    }
}
