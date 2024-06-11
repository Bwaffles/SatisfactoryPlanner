using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork
{
    public class IntegrationTest
    {
        protected string ConnectionString { get; private set; } = null!;

        public IEventsBus EventsBus { get; private set; } = default!;

        protected ILogger Logger { get; private set; } = null!;

        protected IWorldsModule WorldsModule { get; private set; } = null!;

        protected ExecutionContextMock ExecutionContext { get; private set; } = null!;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);

            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                await DatabaseClearer.Clear(connection);
            }

            Logger = Substitute.For<ILogger>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
            EventsBus = Substitute.For<IEventsBus>();

            WorldsStartup.Start(
                ConnectionString,
                ExecutionContext,
                Logger,
                EventsBus,
                new WorldsConfiguration()
                {
                    InternalProcessingExecutionInterval = TimeSpan.FromMilliseconds(200)
                });

            WorldsModule = new WorldsModule();
        }

        [TearDown]
        public void AfterEachTest()
        {
            WorldsStartup.Stop();
        }
    }
}