using FluentAssertions;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Extensions;

namespace SatisfactoryPlanner.BuildingBlocks.Common.UnitTests.Extensions
{
    [TestFixture]
    public class PathExtensionsTests
    {
        [Test]
        public void GetAppDataFolderTest()
        {
            var appFolderInfo = Substitute.For<IAppFolderInfo>();
            appFolderInfo.AppDataFolder.Returns(@"C:\ProgramData\SatisfactoryPlanner");

            appFolderInfo.GetAppDataFolder().Should().Be(@"C:\ProgramData\SatisfactoryPlanner");
        }

        [Test]
        public void GetLogFolderTest()
        {
            var appFolderInfo = Substitute.For<IAppFolderInfo>();
            appFolderInfo.AppDataFolder.Returns(@"C:\ProgramData\SatisfactoryPlanner");

            appFolderInfo.GetLogFolder().Should().Be(@"C:\ProgramData\SatisfactoryPlanner\logs");
        }
    }
}
