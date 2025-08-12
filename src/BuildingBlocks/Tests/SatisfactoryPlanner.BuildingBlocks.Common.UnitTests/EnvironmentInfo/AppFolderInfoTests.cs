using FluentAssertions;
using Microsoft.Extensions.Logging.Testing;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;

namespace SatisfactoryPlanner.BuildingBlocks.Common.UnitTests.EnvironmentInfo
{
    [TestFixture]
    public static class AppFolderInfoTests
    {
        [TestFixture]
        public class ConstructorTests
        {
            [Test]
            public void AppDataConfigured_ShouldOverrideAppDataFolder()
            {
                var startupContext = Substitute.For<IStartupContext>();
                startupContext.AppData.Returns(@"C:\ProgramData\SatisfactoryPlanner");

                var logger = new FakeLogger();

                var appFolderInfo = new AppFolderInfo(startupContext, logger);

                appFolderInfo.AppDataFolder.Should().Be(@"C:\ProgramData\SatisfactoryPlanner");
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("    ")]
            public void AppDataNotConfigured_ShouldUseDefaultAppDataFolder(string? appData)
            {
                var startupContext = Substitute.For<IStartupContext>();
                startupContext.AppData.Returns(appData);

                var logger = new FakeLogger();

                var appFolderInfo = new AppFolderInfo(startupContext, logger);

                appFolderInfo.AppDataFolder.Should().Be(@"C:\ProgramData\SatisfactoryPlanner");
            }
        }
    }
}
