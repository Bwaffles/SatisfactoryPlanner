using CommandLine;
using Microsoft.Extensions.Logging;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation;
using SatisfactoryPlanner.Host;

namespace SatisfactoryPlanner.Console
{
    internal class Options
    {
        [Option('d', "data-folder", Required = false, HelpText = "The path to the folder for the application data.")]
        public string? DataFolder { get; set; }
    }

    public static class ConsoleApp
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options => StartConsoleApp(options))
                .WithNotParsed(options => Exit(ExitCodes.Normal));
        }

        private static void StartConsoleApp(Options options)
        {
            var startupContext = new StartupContext()
            {
                AppData = options.DataFolder
            };

            try
            {
                System.Console.WriteLine("[Setup] Registering logger...");
                SatisfactoryPlannerLogger.Register(startupContext, true);
                System.Console.WriteLine("[Setup] Logger registered.");
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("");
                System.Console.WriteLine($"Logger Exception. {ex}");
                System.Console.ResetColor();

                Exit(ExitCodes.UnknownFailure);
            }

            var loggerFactory = SatisfactoryPlannerLogger.GetLoggerFactory(Module.ConsoleApp);
            var logger = loggerFactory.CreateLogger(typeof(ConsoleApp));
            using (logger.PushContext("Startup"))
            {
                try
                {
                    logger.LogInformation("Starting console...");
                    new Bootstrap(startupContext, loggerFactory).Start();
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "CRITICAL FAILURE!");

                    Exit(ExitCodes.UnknownFailure);
                }

                logger.LogInformation("Exiting console.");
                Exit(ExitCodes.Normal);
            }
        }

        private static void Exit(ExitCodes exitCode)
        {
            SatisfactoryPlannerLogger.Shutdown();

            if (exitCode != ExitCodes.Normal)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Press enter to exit...");

                Thread.Sleep(1000);

                // Please note that ReadLine silently succeeds if there is no console, KeyAvailable does not.
                System.Console.ReadLine();
            }

            Environment.Exit((int)exitCode);
        }

        private enum ExitCodes
        {
            Normal = 0,
            UnknownFailure = 1
        }
    }
}
