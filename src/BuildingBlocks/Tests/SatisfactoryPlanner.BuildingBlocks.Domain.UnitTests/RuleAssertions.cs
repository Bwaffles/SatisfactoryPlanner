using FluentAssertions;

namespace SatisfactoryPlanner.BuildingBlocks.Domain.UnitTests
{
    public static class RuleAssertions
    {
        public static void AssertBrokenRule<TRule>(Action action) where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule.";

            action
                .Should()
                .ThrowExactly<BusinessRuleValidationException>(message)
                .And
                .BrokenRule.Should().BeOfType<TRule>(message);
        }

        public static void AssertRuleNotBroken<TRule>(Action action) where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} to not be broken rule.";

            action
                .Should()
                .NotThrow<BusinessRuleValidationException>(message);
        }
    }
}