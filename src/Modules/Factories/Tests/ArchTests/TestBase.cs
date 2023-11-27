using FluentAssertions;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Domain.Factories;
using SatisfactoryPlanner.Modules.Factories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Factories.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(Factory).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(FactoriesContext).Assembly;

        protected static void AssertAreImmutable(IEnumerable<Type> types)
        {
            var failingTypes = types
                .Where(type => type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite));

            AssertFailingTypes(failingTypes);
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