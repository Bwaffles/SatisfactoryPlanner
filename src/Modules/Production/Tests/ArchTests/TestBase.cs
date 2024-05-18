using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.ArchTests;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Domain.Factories;
using SatisfactoryPlanner.Modules.Production.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Production.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(Factory).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(ProductionContext).Assembly;

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