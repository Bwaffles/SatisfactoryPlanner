using FluentAssertions;
using NetArchTest.Rules;
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
            IList<Type> failingTypes = new List<Type>();
            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }

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
