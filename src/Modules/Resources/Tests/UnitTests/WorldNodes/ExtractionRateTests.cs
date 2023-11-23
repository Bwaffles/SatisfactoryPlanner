using FluentAssertions;
using NUnit.Framework;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.WorldNodes
{
    [TestFixture]
    public class ExtractionRateTests
    {
        [Test]
        public void PositiveRate_Succeeds()
        {
            ExtractionRate.Of(42).Rate.Should().Be(42);
        }

        [Test]
        public void ZeroRate_Succeeds()
        {
            ExtractionRate.Of(0).Rate.Should().Be(0);
        }

        [Test]
        public void NegativeRate_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ExtractionRate.Of(-1));
        }
    }
}