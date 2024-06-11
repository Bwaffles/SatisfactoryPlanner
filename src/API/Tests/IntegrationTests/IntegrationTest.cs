using FluentAssertions.Execution;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests;
using SatisfactoryPlanner.BuildingBlocks.IntegrationTests.Probing;

namespace SatisfactoryPlanner.API.IntegrationTests
{
    // All integration tests should be run sequentially because they use the same dependencies like the database.
    // If the tests were running in parallel there could be issues with table locks, or tests deleting data that other tests rely on.
    [NonParallelizable]
    public class IntegrationTest
    {
        private WebApplicationFactory<Program> _webApplicationFactory = default!;

        protected string ConnectionString { get; private set; } = default!;

        public HttpClient Client { get; private set; } = default!;

        [SetUp]
        public async Task BeforeEachTest()
        {
            const string connectionStringEnvironmentVariable = "ASPNETCORE_SatisfactoryPlanner_IntegrationTests_ConnectionString";
            ConnectionString = EnvironmentVariablesProvider.GetVariable(connectionStringEnvironmentVariable);
            if (ConnectionString == null)
                throw new ApplicationException($"Define connection string to integration tests database using environment variable: {connectionStringEnvironmentVariable}.");

            await DatabaseClearer.Clear(ConnectionString);

            // since tests run sequentially, each test can manipulate this user to perform their test and it will reset before each test so the tests don't affect each other
            AuthenticatedUser.Reset();

            _webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
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

                builder.UseSetting("Logs:EnableConsoleLogging", false.ToString());

                builder.UseSetting("InternalProcessingExecutionInterval", TimeSpan.FromMilliseconds(200).ToString());
            });
            Client = _webApplicationFactory.CreateClient();
        }

        [TearDown]
        public void AfterEachTest()
        {
            _webApplicationFactory.Dispose(); // this will shut down the server as well as all clients
        }

        protected static async Task<T> GetEventually<T>(IProbe<T> probe, int timeout)
            where T : class => await Polling.GetEventually(probe, timeout);

        protected static void AssertAll(Action assert)
        {
            using (new AssertionScope())
                assert();
        }
    }
}