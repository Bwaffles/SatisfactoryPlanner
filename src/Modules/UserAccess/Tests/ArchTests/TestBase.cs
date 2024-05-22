using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.ArchTests;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SatisfactoryPlanner.Modules.UserAccess.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

        protected static Assembly DomainAssembly => typeof(User).Assembly;

        protected static Assembly InfrastructureAssembly => typeof(UserAccessContext).Assembly;

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