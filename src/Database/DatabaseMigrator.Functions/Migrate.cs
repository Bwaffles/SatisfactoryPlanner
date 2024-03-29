using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DatabaseMigrator.Functions
{
    public class Migrate
    {
        private readonly ILogger _logger;

        public Migrate(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Migrate>();
        }

        [Function("Migrate")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Running migrations...");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var serverConnectionString = Environment.GetEnvironmentVariable("ConnectionString_Server", EnvironmentVariableTarget.Process);
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString_Database", EnvironmentVariableTarget.Process);
            var migrationRunner = new MigrationRunner(_logger,serverConnectionString, connectionString);
            migrationRunner.Migrate();
            migrationRunner.ListMigrations();

            response.WriteString("Migrations completed.");

            return response;
        }
    }
}
