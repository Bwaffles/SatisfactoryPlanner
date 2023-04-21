using FluentAssertions;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System;
using Xunit;

namespace SatisfactoryPlanner.Modules.Resources.UnitTests.TappedNodes
{
    public class ExtractionRateTests
    {
        [Fact]
        public void PositiveRate_Succeeds()
        {
            ExtractionRate.Of(42).Rate.Should().Be(42);
        }

        [Fact]
        public void ZeroRate_Succeeds()
        {
            ExtractionRate.Of(0).Rate.Should().Be(0);
        }

        [Fact]
        public void NegativeRate_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ExtractionRate.Of(-1));
        }
    }
}