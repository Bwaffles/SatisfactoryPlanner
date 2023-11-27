using FluentAssertions;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Resources.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(Node).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(ResourcesContext).Assembly;

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