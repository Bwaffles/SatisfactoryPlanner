using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Templates;
using Serilog.Templates.Themes;
using System.Diagnostics;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation
{
    /// <summary>
    /// The logger for the application.
    /// </summary>
    /// <remarks>
    /// Logging is intended to be passed through DI as much as possible since it's easy for the Modules to set up their specific loggers.
    /// During Startup, and any common classes that might be used before DI is set up can still access the logs statically.
    /// 
    /// Intending to only expose Microsoft Logging, and abstracting the fact that we use Serilog as much as possible.
    /// </remarks>
    public static class SatisfactoryPlannerLogger
    {
        private const string Template = "{@t:u} [{@l}][{Module}:{Context}][{SourceContext}] {@m}\n{@x}";
        private static bool _isConfigured;

        /// <summary>
        /// Configure and register logs to be used in the application.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public static void Register(IStartupContext startupContext, bool inConsole)
        {
            if (_isConfigured)
                throw new InvalidOperationException("Loggers have already been registered.");

            _isConfigured = true;

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // Filter out ASP.NET Core infrastructre logs that are Information and below
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Fatal) // See API.Program.ConfigureAuthenticationService comments for why this is being done
                .Enrich.FromLogContext();

            if (inConsole)
            {
                loggerConfiguration.WriteTo.Console(new ExpressionTemplate(Template, theme: TemplateTheme.Code));
            }

            if (Debugger.IsAttached)
            {
                loggerConfiguration.WriteTo.Debug(new ExpressionTemplate(Template));
            }

            var appFolderInfo = new AppFolderInfo(startupContext, NullLogger.Instance);

            loggerConfiguration
               .WriteTo.File(new CompactJsonFormatter(),
                   Path.Combine(appFolderInfo.GetLogFolder(), "satisfactory-planner.json"),
                   rollOnFileSizeLimit: true,
                   fileSizeLimitBytes: 5 * 1024 * 1024);

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        /// <summary>
        /// Shutdown the logs and free up resources.
        /// </summary>
        public static void Shutdown()
        {
            Log.CloseAndFlush();
        }

        /// <summary>
        /// Get a factory for the given <paramref name="module"/> able to create instances of <see cref="ILogger{TCategoryName}"/>.
        /// </summary>
        public static ILoggerFactory GetLoggerFactory(Module module)
        {
            return new LoggerFactory()
                .AddSerilog(Log.Logger.ForContext("Module", module));
        }

        /// <summary>
        /// Push the <paramref name="context"/> value to the log enrichment Context property.
        /// </summary>
        /// <code>
        /// var logger = ...
        /// using (logger.PushContext("Startup"))
        /// {
        ///   ... log your messages here to include the Startup as the Context
        /// }
        /// 
        /// Messages logged here won't have Startup as the Context
        /// </code>>
        public static IDisposable? PushContext(this ILogger logger, string context)
        {
            return logger.BeginScope(new Dictionary<string, object>
            {
                { "Context", context }
            });
        }
    }
}
