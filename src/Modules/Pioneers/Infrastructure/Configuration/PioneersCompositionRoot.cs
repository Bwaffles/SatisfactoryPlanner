﻿using Autofac;

namespace SatisfactoryPlanner.Modules.Pioneers.Infrastructure.Configuration
{
    internal static class PioneersCompositionRoot
    {
        private static IContainer _container;

        internal static void SetContainer(IContainer container) => _container = container;

        internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}