using Dapper;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Worlds.Application.Contracts;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;
using System.Data;

namespace SatisfactoryPlanner.Modules.Worlds.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; } = null!;

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
                await ClearDatabase(connection);
            }

            Logger = Substitute.For<ILogger>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            WorldsStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            WorldsModule = new WorldsModule();
        }

        [TearDown]
        public void AfterEachTest()
        {
            WorldsStartup.Stop();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM worlds.inbox_messages;" +
                               "DELETE FROM worlds.internal_commands;" +
                               "DELETE FROM worlds.outbox_messages;" +
                               "DELETE FROM worlds.pioneers;" +
                               "DELETE FROM worlds.worlds;";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}