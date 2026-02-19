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
            var appFolderInfo = GetAppFolderInfo(@"C:\ProgramData\SatisfactoryPlanner");
            appFolderInfo.GetAppDataFolder().Should().Be(@"C:\ProgramData\SatisfactoryPlanner");
        }

        [Test]
        public void GetConfigPathTest()
        {
            var appFolderInfo = GetAppFolderInfo(@"C:\ProgramData\SatisfactoryPlanner");
            appFolderInfo.GetConfigPath().Should().Be(@"C:\ProgramData\SatisfactoryPlanner\config.json");
        }

        [Test]
        public void GetLogFolderTest()
        {
            var appFolderInfo = GetAppFolderInfo(@"C:\ProgramData\SatisfactoryPlanner");
            appFolderInfo.GetLogFolder().Should().Be(@"C:\ProgramData\SatisfactoryPlanner\logs");
        }

        private static IAppFolderInfo GetAppFolderInfo(string appDataFolder)
        {
            var appFolderInfo = Substitute.For<IAppFolderInfo>();
            appFolderInfo.AppDataFolder.Returns(appDataFolder);

            return appFolderInfo;
        }
    }
}
