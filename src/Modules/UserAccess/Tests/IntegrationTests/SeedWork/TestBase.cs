using Dapper;
using MediatR;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using Serilog;
using System.Data;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; } = default!;

        protected ILogger Logger { get; private set; } = default!;

        protected IUserAccessModule UserAccessModule { get; private set; } = default!;

        protected ExecutionContextMock ExecutionContext { get; private set; } = default!;

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

            UserAccessStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            UserAccessModule = new UserAccessModule();
        }

        protected async Task<T> GetLastOutboxMessage<T>()
            where T : class, INotification
        {
            await using var connection = new NpgsqlConnection(ConnectionString);
            var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

            return OutboxMessagesHelper.Deserialize<T>(messages.Last());
        }

        [TearDown]
        public void AfterEachTest()
        {
            UserAccessStartup.Stop();
            //SystemClock.Reset();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            const string sql = "DELETE FROM users.inbox_messages;" +
                               "DELETE FROM users.internal_commands;" +
                               "DELETE FROM users.outbox_messages;" +
                               "DELETE FROM users.users;" +
                               "DELETE FROM users.user_roles;";

            await connection.ExecuteScalarAsync(sql);
        }
    }
}