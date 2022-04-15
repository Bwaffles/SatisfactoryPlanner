using DatabaseMigrator.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DatabaseMigrator
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Starting migration...");

            if (args.Length != 2)
            {
                Console.WriteLine("Invalid arguments. Execution: DatabaseMigrator [masterConnectionString] [connectionString].");
                return -1;
            }

            var masterConnectionString = args[0]; // to be able to create the DB
            var connectionString = args[1];

            Database.EnsureDatabase(masterConnectionString, "satisfactory-planner");

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

            using (var scope = servicesProvider.CreateScope())
            {
                UpdateDatabase(connectionString, scope.ServiceProvider);
            }

            return 0;
        }

        /// <summary>
        /// Update the database
        /// </sumamry>
        private static void UpdateDatabase(string connectionString, IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();

            // Data model changes
            //RollbackMigrations(runner);
            //MigrateNext(runner);
            //RetestPreviousMigrations(runner);

            runner.ListMigrations();
        }

        private static void RollbackMigrations(IMigrationRunner runner, int steps = 1)
        {
            runner.Rollback(steps);
        }

        private static void MigrateNext(IMigrationRunner runner, long? version = null)
        {
            if (version == null)
                runner.MigrateUp();
            else
                runner.MigrateUp(version.Value);
        }

        private static void RetestPreviousMigrations(IMigrationRunner runner)
        {
            runner.Rollback(1);
            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
