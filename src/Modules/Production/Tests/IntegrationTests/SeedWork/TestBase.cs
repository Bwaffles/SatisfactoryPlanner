using Dapper;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Production.Infrastructure;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using Serilog;
using System.Data;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork
{
    public class TestBase
    {
        protected string ConnectionString { get; private set; } = null!;

        protected ILogger Logger { get; private set; } = null!;

        protected IProductionModule ProductionModule { get; private set; } = null!;

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

            ProductionStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            ProductionModule = new ProductionModule();
        }

        //protected async Task<T> GetLastOutboxMessage<T>()
        //    where T : class, INotification
        //{
        //    await using var connection = new NpgsqlConnection(ConnectionString);
        //    var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

        //    return OutboxMessagesHelper.Deserialize<T>(messages.Last());
        //}

        protected static void AssertInvalidCommand(AsyncTestDelegate testDelegate)
        {
            var message = $"Expected invalid command.";
            Assert.CatchAsync<InvalidCommandException>(testDelegate, message);
        }

        protected static void AssertBrokenRule<TRule>(AsyncTestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule.";
            var businessRuleValidationException = Assert.CatchAsync<BusinessRuleValidationException>(testDelegate, message);
            businessRuleValidationException?.BrokenRule.Should().BeOfType<TRule>(message);
        }

        [TearDown]
        public void AfterEachTest()
        {
            ProductionStartup.Stop();
            //SystemClock.Reset();
        }

        private static async Task ClearDatabase(IDbConnection connection)
        {
            /* Not clearing the following tables since they're pre-loaded reference tables:
             */

            var sql = ClearDatabaseSqlGenerator.InSchema("production")
                .ClearTable("inbox_messages")
                .ClearTable("internal_commands")
                .ClearTable("outbox_messages")
                .ClearTable("production_lines")
                .GenerateSql();

            await connection.ExecuteScalarAsync(sql);
        }
    }
}