using CommandLine;
using System;

namespace DatabaseMigrator
{
    internal class Options
    {
        [Option('s', "server-connection-string", Required = true, HelpText = "The connection to the database server.")]
        public string ServerConnectionString { get; set; }
    }

    [Verb("migrate", HelpText = "Run all migrations.")]
    internal class MigrateOptions : Options
    {
    }

    [Verb("dev", HelpText = "Start an interactive session to manually control migrations and rollbacks for development of new migrations.")]
    internal class DevOptions : Options
    {
    }

    internal class Program
    {
        private static int Main(string[] args)
        {
            Console.WriteLine("Starting migration...");

            return Parser.Default.ParseArguments<MigrateOptions, DevOptions>(args)
                .MapResult(
                    (MigrateOptions opts) => RunMigrate(opts),
                    (DevOptions opts) => RunDev(opts),
                    _ => 1);
        }

        private static MigrationRunner CreateMigrationRunner(Options options)
        {
            var serverConnectionString = options.ServerConnectionString;
            return new MigrationRunner(serverConnectionString, "satisfactory-planner");
        }

        private static int RunDev(DevOptions options)
        {
            var runner = CreateMigrationRunner(options);

            runner.ListMigrations();

            bool quit = false;
            while (!quit)
            {
                quit = SelectOption(runner);
            }

            return 0;
        }

        private static int RunMigrate(MigrateOptions options)
        {
            var runner = CreateMigrationRunner(options);
            runner.Migrate();

            return 0;
        }

        private static bool SelectOption(MigrationRunner runner)
        {
            Console.WriteLine("\r\nSelect option\r\n" +
                              "\t(1) Rollback\r\n" +
                              "\t(2) Migrate Next\r\n" +
                              "\t(3) Retest Last Migration\r\n" +
                              "\t(Q) Quit:");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    runner.Rollback();
                    runner.ListMigrations();
                    return false;
                case "2":
                    runner.Migrate();
                    runner.ListMigrations();
                    return false;
                case "3":
                    runner.RerunPreviousMigration();
                    runner.ListMigrations();
                    return false;
                case "Q":
                case "q":
                    return true;
                default:
                    Console.WriteLine("Invalid option.");
                    return false;

            }
        }
    }
}
