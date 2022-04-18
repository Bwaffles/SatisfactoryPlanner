using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests
{
    internal static class Rules
    {
        public static void AssertBrokenRule<TRule>(Action action) where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";

            action
                .Should()
                    .ThrowExactly<BusinessRuleValidationException>(message)
                .And
                    .BrokenRule.Should().BeOfType<TRule>(message);
        }
    }
}
