using Dapper;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using Serilog;
using System.Data;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; } = null!;

        protected ILogger Logger { get; private set; } = null!;

        protected IResourcesModule ResourcesModule { get; private set; } = null!;

        protected ExecutionContextMock ExecutionContext { get; private set; } = null!;

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

            ResourcesStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            ResourcesModule = new ResourcesModule();
        }

        //protected async Task<T> GetLastOutboxMessage<T>()
        //    where T : class, INotification
        //{
        //    await using var connection = new NpgsqlConnection(ConnectionString);
        //    var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

        //    return OutboxMessagesHelper.Deserialize<T>(messages.Last());
        //}

        [TearDown]
        public void AfterEachTest()
        {
            //ResourcesStartup.Stop();
            //SystemClock.Reset();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = //"DELETE FROM resources.inbox_messages;" +
                               //"DELETE FROM resources.internal_commands;" +
                               //"DELETE FROM resources.outbox_messages;" +
                               "DELETE FROM resources.tapped_nodes;";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}