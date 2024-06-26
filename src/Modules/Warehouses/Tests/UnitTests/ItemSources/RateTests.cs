using FluentAssertions;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.UnitTests.ItemSources
{
    [TestFixture]
    public class RateTests
    {
        [Test]
        public void PositiveRate_Succeeds()
        {
            Rate.Of(42).Value.Should().Be(42);
        }

        [Test]
        public void ZeroRate_Succeeds()
        {
            Rate.Of(0).Value.Should().Be(0);
        }

        [Test]
        public void NegativeRate_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Rate.Of(-1));
        }
    }
}