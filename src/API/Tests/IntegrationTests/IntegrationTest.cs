using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.Modules.UserAccess.IntegrationTests.SeedWork;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    // All integration tests should be run sequentially because they use the same dependencies like the database.
    // If the tests were running in parallel there could be issues with table locks, or tests deleting data that other tests rely on.
    [NonParallelizable]
    public class IntegrationTest
    {
        protected string ConnectionString { get; private set; } = default!;

        public HttpClient Client { get; private set; }

        [OneTimeSetUp]
        public void OnStartup()
        {
            const string connectionStringEnvironmentVariable = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
                throw new ApplicationException($"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}.");

            Client = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                // Override authentication to make all calls anonymous https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#authenticationhandler
                builder.ConfigureTestServices(services =>
                {
                    services
                    .AddAuthentication("IntegrationTest")
                    .AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>("IntegrationTest", options => { });
                });

                // Set this to a nonexistant server to be extra sure we don't spam our auth server
                builder.UseSetting("Auth0:Domain", "fakeDomain");
                builder.UseSetting("Auth0:Audience", "fakeAudience");

                builder.UseSetting("ConnectionStrings:SatisfactoryPlanner", ConnectionString);
            }).CreateClient();
        }

        [SetUp]
        public async Task BeforeEachTest()
        {
            await ClearDatabase();
            // since tests run sequentially, each test can manipulate this user to perform their test and it will reset before each test so the tests don't affect each other
            AuthenticatedUser.Reset(); 
        }

        [OneTimeTearDown]
        public void OnShutdown()
        {
            Client.Dispose();
        }

        private async Task ClearDatabase()
        {
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                // Call out to each module's integration test project so that the api tests don't need to stay in sync with all module changes.
                // Assuming each integration test project knows how to clear its own database data between tests.
                await DatabaseClearer.Clear(connection);
                // TODO add other module's database clearers 
            }
        }
    }
}