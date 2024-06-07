using FluentAssertions.Execution;
using MediatR;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Infrastructure;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using Serilog;

namespace SatisfactoryPlanner.Modules.Resources.IntegrationTests.SeedWork
{
    public class IntegrationTest
    {
        protected string ConnectionString { get; private set; } = null!;

        public IEventsBus EventsBus { get; private set; } = default!;

        protected ILogger Logger { get; private set; } = null!;

        protected IResourcesModule ResourcesModule { get; private set; } = null!;

        protected ExecutionContextMock ExecutionContext { get; private set; } = null!;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
                throw new ApplicationException($"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}.");

            await using (var connection = new NpgsqlConnection(ConnectionString))
                await DatabaseClearer.Clear(connection);

            Logger = Substitute.For<ILogger>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());
            EventsBus = Substitute.For<IEventsBus>();

            ResourcesStartup.Start(
                ConnectionString,
                ExecutionContext,
                Logger,
                EventsBus);

            ResourcesModule = new ResourcesModule();
        }

        //protected async Task<T> GetLastOutboxMessage<T>()
        //    where T : class, INotification
        //{
        //    await using var connection = new NpgsqlConnection(ConnectionString);
        //    var messages = await OutboxMessagesHelper.GetOutboxMessages(connection);

        protected static void AssertAll(Action assert)
        {
            using (new AssertionScope())
                assert();
        }

        [TearDown]
        public void AfterEachTest()
        {
            ResourcesStartup.Stop();
        }
    }
}