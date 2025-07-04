﻿using SatisfactoryPlanner.BuildingBlocks.ArchTests;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Domain.Worlds;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Worlds.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(World).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(WorldsContext).Assembly;

        protected static void AssertAreImmutable(IEnumerable<Type> types)
        {
            types.AssertImmutability();
        }

        protected static void AssertFailingTypes(IEnumerable<Type> types)
        {
            types.Should().BeNullOrEmpty();
        }

        protected static void AssertArchTestResult(TestResult result)
        {
            AssertFailingTypes(result.FailingTypes);
        }
    }
}