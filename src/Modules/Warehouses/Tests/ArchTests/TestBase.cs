using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.ArchTests;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.Warehouses.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(IWarehousesModule).Assembly;

        //protected static Assembly DomainAssembly => typeof(ItemSource).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(WarehousesStartup).Assembly;

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