using FluentAssertions.Execution;
using Npgsql;
using NSubstitute;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Infrastructure;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using Serilog;

namespace SatisfactoryPlanner.Modules.Production.IntegrationTests.SeedWork
{
    public class IntegrationTest
    {
        protected string ConnectionString { get; private set; } = null!;

        protected ILogger Logger { get; private set; } = null!;

        protected IProductionModule ProductionModule { get; private set; } = null!;

        protected ExecutionContextMock ExecutionContext { get; private set; } = null!;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
                throw new ApplicationException($"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}.");

            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                await DatabaseClearer.Clear(connection);
            }

            Logger = Substitute.For<ILogger>();
            ExecutionContext = new ExecutionContextMock(Guid.NewGuid());

            ProductionStartup.Initialize(
                ConnectionString,
                ExecutionContext,
                Logger);

            ProductionModule = new ProductionModule();
        }

        protected static void AssertAll(Action RunAssertions)
        {
            using (new AssertionScope())
            {
                RunAssertions();
            }
        }
        protected static void AssertInvalidCommand(AsyncTestDelegate testDelegate)
        {
            const string message = "Expected invalid command.";
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
        }
    }
}