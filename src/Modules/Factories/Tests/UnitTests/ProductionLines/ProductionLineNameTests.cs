using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines;
using SatisfactoryPlanner.Modules.Factories.Domain.ProductionLines.Rules;

namespace SatisfactoryPlanner.Modules.Factories.UnitTests.ProductionLines
{
    [TestFixture]
    public class ProductionLineNameTests
    {
        [Test]
        public void ValueSet()
        {
            ProductionLineName.As("Line 1").Value
                .Should().Be("Line 1");
        }

        [TestCase("")]
        [TestCase("  ")]
        public void NameCannotBeEmpty(string name)
        {
            RuleAssertions.AssertBrokenRule<ProductionLineNameCannotBeEmptyRule>(() => ProductionLineName.As(name));
        }
    }
}