using DatabaseMigrator.Migrations;
using DatabaseMigrator.Migrations.Factories;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DatabaseMigrator
{
    public class MigrationRunner
    {
        private readonly IMigrationRunner _migrationRunner;

        public MigrationRunner(ILogger logger, string serverConnectionString, string connectionString)
        {
            Database.EnsureDatabase(logger, serverConnectionString, "satisfactory-planner");

            var servicesProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(Create_Factories).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                //.AddScoped<PostgresQuoter, NoQuoteQuoter>()
                // Build the service provider
                .BuildServiceProvider(false);

            _migrationRunner = servicesProvider.GetRequiredService<IMigrationRunner>();
        }

        public void ListMigrations() => _migrationRunner.ListMigrations();

        public void Migrate()
        {
            _migrationRunner.MigrateUp();
        }

        public void RerunPreviousMigration()
        {
            _migrationRunner.Rollback(1);
            _migrationRunner.ListMigrations();
            _migrationRunner.MigrateUp();
        }

        public void Rollback()
        {
            _migrationRunner.Rollback(1);
        }
    }
}
