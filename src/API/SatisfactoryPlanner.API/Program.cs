using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SatisfactoryPlanner.API.Configuration.Authentication;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.API.Configuration.ExecutionContext;
using SatisfactoryPlanner.API.Configuration.Extensions;
using SatisfactoryPlanner.API.Configuration.Modules;
using SatisfactoryPlanner.API.Configuration.Routing;
using SatisfactoryPlanner.API.Configuration.Validation;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.EventBus;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

using (LogContext.PushProperty("Context", "Startup"))
{
    var _logger = CreateLogger(configuration);

    var _loggerForApi = _logger.ForContext("Module", "API");
    _loggerForApi.Information("Application starting...");

    builder.Host.UseSerilog(_loggerForApi);
    Log.Logger = _loggerForApi;

    ConfigureServices(builder.Services, configuration);

    // Using a custom DI container.
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(RegisterModules);

    var app = builder.Build();

    var eventsBus = new InMemoryEventBusClient();

    var lifeTime = app.Lifetime;
    lifeTime.ApplicationStopping.Register(() =>
    {
        using (LogContext.PushProperty("Context", "Stopping"))
        {
            _loggerForApi.Information("Application stopping...");
            ProductionStartup.Stop();
            ResourcesStartup.Stop();
            UserAccessStartup.Stop();
            WarehousesStartup.Stop();
            WorldsStartup.Stop();
            eventsBus.Stop();
        }
    });
    lifeTime.ApplicationStopped.Register(() =>
    {
        using (LogContext.PushProperty("Context", "Stopped"))
        {
            _loggerForApi.Information("Application stopped");
            _logger.Dispose();
        }
    });

    Configure(app, app.Environment, _logger, configuration, eventsBus);

    app.MapControllers();

    _loggerForApi.Information("Application started");
    app.Run();
}

static Logger CreateLogger(ConfigurationManager configuration)
{
    var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // Filter out ASP.NET Core infrastructre logs that are Information and below
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Fatal) // See Program.ConfigureAuthenticationService comments for why this is being done
            .Enrich.FromLogContext();

    if (configuration.GetValue<bool>("Logs:EnableConsoleLogging"))
    {
        loggerConfiguration
        .WriteTo.Console(
            outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}");
    }

    if (configuration.GetValue<bool>("Logs:Seq:Enable"))
    {
        var seqServerUrl = configuration.GetValue<string>("Logs:Seq:ServerUrl") ?? throw new InvalidOperationException("Logs:Seq:ServerUrl not defined.");

        loggerConfiguration.WriteTo.Seq(seqServerUrl);
    }

    return loggerConfiguration
        .WriteTo.File(new CompactJsonFormatter(),
            "logs/logs.json",
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 5 * 1024 * 1024)
        .CreateLogger();
}

static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.ConfigureAuthenticationService(configuration);
    services.ConfigureAuthorizationService();

    services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    });

    services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
    });

    services.AddSwaggerDocumentation();

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

    services.AddProblemDetails(options =>
    {
        options.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
        options.Map<BusinessRuleValidationException>(
            ex => new BusinessRuleValidationExceptionProblemDetails(ex));
    });
}

static void RegisterModules(ContainerBuilder containerBuilder)
{
    containerBuilder.RegisterModule<ApiModule>();
}

static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger, ConfigurationManager configuration, IEventsBus eventsBus)
{
    app.UseCors(builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );

    StartModules(app, logger, configuration, eventsBus);

    app.UseMiddleware<CorrelationMiddleware>();

    app.UseProblemDetails();

    if (env.IsDevelopment())
    {
        app.UseSwaggerDocumentation();
    }
    // else
    // {
    //     app.UseHsts();
    // }

    //app.UseHttpsRedirection();

    app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogExtensions.EnrichFromRequest);

    app.UseAuthentication();

    // To be used by WorldAuthorization to get world id from the body of the request
    app.Use((context, next) =>
    {
        context.Request.EnableBuffering(1_000_000);
        return next();
    });
    app.UseAuthorization();
}

static void StartModules(IApplicationBuilder app, ILogger logger, ConfigurationManager configuration, IEventsBus eventsBus)
{
    var container = app.ApplicationServices.GetAutofacRoot();
    var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
    var connectionString = configuration.GetConnectionString("SatisfactoryPlanner") ?? throw new InvalidOperationException("SatisfactoryPlanner connection string not defined.");
    var internalProcessingExecutionInterval = configuration.GetValue<TimeSpan>("InternalProcessingExecutionInterval");

    ProductionStartup.Start(
        connectionString,
        executionContextAccessor,
        logger,
        eventsBus,
        new ProductionConfiguration()
        {
            InternalProcessingExecutionInterval = internalProcessingExecutionInterval
        }
    );

    ResourcesStartup.Start(
        connectionString,
        executionContextAccessor,
        logger,
        eventsBus,
        new ResourcesConfiguration()
        {
            InternalProcessingExecutionInterval = internalProcessingExecutionInterval
        }
    );

    UserAccessStartup.Start(
        connectionString,
        executionContextAccessor,
        logger,
        eventsBus,
        new UserAccessConfiguration()
        {
            InternalProcessingExecutionInterval = internalProcessingExecutionInterval
        }
    );

    WarehousesStartup.Start(
        connectionString,
        executionContextAccessor,
        logger,
        eventsBus,
        new WarehousesConfiguration()
        {
            InternalProcessingExecutionInterval = internalProcessingExecutionInterval
        });

    WorldsStartup.Start(
        connectionString,
        executionContextAccessor,
        logger,
        eventsBus,
        new WorldsConfiguration()
        {
            InternalProcessingExecutionInterval = internalProcessingExecutionInterval
        }
    );
}