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
            ProductionLineName.As("Line 1").Value.Should().Be("Line 1");
        }

        [Test]
        public void NamesAreCaseInsensitive()
        {
            var nameOne = ProductionLineName.As("Line 1");
            var nameTwo = ProductionLineName.As("LINE 1");

            nameOne.Should().BeEquivalentTo(nameTwo);
        }

        [TestCase("")]
        [TestCase("  ")]
        public void NameCannotBeEmpty(string name)
        {
            RuleAssertions.AssertBrokenRule<ProductionLineNameCannotBeEmptyRule>(() => ProductionLineName.As(name));
        }
    }
}