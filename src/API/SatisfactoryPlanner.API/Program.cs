using Autofac;
using Autofac.Extensions.DependencyInjection;
using SatisfactoryPlanner.API;
using Serilog;
using Serilog.Formatting.Compact;

var _logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(new CompactJsonFormatter(),
        "logs/logs.json",
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 5 * 1024 * 1024)
    .CreateLogger();

var _loggerForApi = _logger.ForContext("Module", "API");
_loggerForApi.Information("Application started.");

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

// Using a custom DI container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

var app = builder.Build();

startup.Configure(app, app.Environment, _logger);

app.MapControllers();

app.Run();