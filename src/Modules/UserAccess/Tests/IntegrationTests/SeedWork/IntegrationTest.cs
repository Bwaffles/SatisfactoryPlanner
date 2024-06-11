using MediatR;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using Serilog;

namespace SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork
{
    public class IntegrationTest
    {
        protected string ConnectionString { get; private set; } = default!;

        public IEventsBus EventsBus { get; private set; } = default!;

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
                await DatabaseClearer.Clear(connection);
            }

            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
            Logger = Substitute.For<ILogger>();
            EventsBus = Substitute.For<IEventsBus>();

            UserAccessStartup.Start(
                ConnectionString,
                ExecutionContext,
                Logger,
                EventsBus,
                new UserAccessConfiguration()
                {
                    InternalProcessingExecutionInterval = TimeSpan.FromMilliseconds(200)
                });

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
    }
}