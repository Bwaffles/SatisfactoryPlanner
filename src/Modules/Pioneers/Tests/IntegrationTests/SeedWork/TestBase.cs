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
        protected string ConnectionString { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IWorldsModule WorldsModule { get; private set; }

        protected ExecutionContextMock ExecutionContext { get; private set; }

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable =
                "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
                throw new ApplicationException(
                    $"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}.");

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