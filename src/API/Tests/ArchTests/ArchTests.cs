using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace SatisfactoryPlanner.API.ArchTests
{
    public abstract class ArchTests
    {
        protected readonly static Assembly ApiAssembly = typeof(Program).Assembly;
        protected static void AssertFailingTypes(IEnumerable<Type> types) => types.Should().BeNullOrEmpty();

        protected static void AssertArchTestResult(TestResult result) => AssertFailingTypes(result.FailingTypes);
    }
}