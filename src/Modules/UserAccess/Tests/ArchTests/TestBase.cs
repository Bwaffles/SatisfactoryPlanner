using FluentAssertions;
using NetArchTest.Rules;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.UserAccess.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(UserRegistration).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(UserAccessContext).Assembly;

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
