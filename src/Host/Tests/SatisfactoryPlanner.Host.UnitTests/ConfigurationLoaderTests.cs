using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Options;

namespace SatisfactoryPlanner.Host.UnitTests
{
    public class ConfigurationLoaderTests : TestBase
    {
        [Test]
        public void ConfigurationFileFound_ShouldLoadValuesFromFile()
        {
            var startupContext = Substitute.For<IStartupContext>();
            startupContext.AppData.Returns(TestDataPath);

            var loggerFactory = Substitute.For<ILoggerFactory>();
            var config = ConfigurationLoader.Load(startupContext, loggerFactory);

            using (new AssertionScope())
            {
                config.Root.Should().NotBeNull();

                var serverOptions = config.ServerOptions;
                serverOptions.BindAddress.Should().Be("localhost");
                serverOptions.Port.Should().Be(12345);
            }
        }

        [Test]
        public void ConfigurationFileNotFound_ShouldLoadDefaultValues()
        {
            var startupContext = Substitute.For<IStartupContext>();
            startupContext.AppData.Returns("FakePathThatDoesNotExist");

            var loggerFactory = Substitute.For<ILoggerFactory>();
            var config = ConfigurationLoader.Load(startupContext, loggerFactory);

            using (new AssertionScope())
            {
                config.Root.Should().NotBeNull();

                var expectedServerOptions = new ServerOptions();
                config.ServerOptions.Should().BeEquivalentTo(expectedServerOptions);
            }
        }
    }
}