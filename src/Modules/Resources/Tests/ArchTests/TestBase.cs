using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.ArchTests;
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