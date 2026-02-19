using FluentAssertions;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;

namespace SatisfactoryPlanner.BuildingBlocks.Common.UnitTests.EnvironmentInfo
{
    [TestFixture]
    public class BuildInfoTests
    {
        [Test]
        public void ShouldReturnVersion()
        {
            BuildInfo.Version.Major.Should().Be(0);
        }

        [Test]
        public void ShouldReturnAppName()
        {
            BuildInfo.AppName.Should().Be("Satisfactory Planner");
        }
    }
}
