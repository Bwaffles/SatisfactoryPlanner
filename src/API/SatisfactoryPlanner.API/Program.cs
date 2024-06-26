using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
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

    ConfigureServices(builder);

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

    return loggerConfiguration
        .WriteTo.File(new CompactJsonFormatter(),
            "logs/logs.json",
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 5 * 1024 * 1024)
        .CreateLogger();
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    ConfigureAuthenticationService(builder);
    ConfigureAuthorizationService(builder);

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new ProducesAttribute("application/json"));
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    });

    builder.Services.AddRouting(options =>
    {
        options.LowercaseUrls = true;
    });

    builder.Services.AddSwaggerDocumentation();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

    builder.Services.AddProblemDetails(options =>
    {
        options.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
        options.Map<BusinessRuleValidationException>(
            ex => new BusinessRuleValidationExceptionProblemDetails(ex));
    });
}

static void ConfigureAuthenticationService(WebApplicationBuilder builder) => builder.Services
    // The warnings in logs about signing keys is because of this issue https://github.com/dotnet/aspnetcore/issues/47410.
    // AddAuthentication calls AddDataProtection because it's a general function for different auth types,
    // but we aren't using data protection for JWT tokens and it's giving us a warning. Seems to be put in the backlog by microsoft for now.
    // I have ignored the warnings for the DataProtection namespace in the logs so that no one spends time on this again.
    // Can revisit later on to see if a solution has been implemented.
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"];
    });

static void ConfigureAuthorizationService(WebApplicationBuilder builder)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
        {
            policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
        });
        options.AddPolicy(WorldAuthorizationAttribute.HasPermissionPolicyName, policyBuilder =>
        {
            policyBuilder.Requirements.Add(new WorldAuthorizationRequirement());
        });
    });

    builder.Services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, WorldAuthorizationHandler>();
}

static void RegisterModules(ContainerBuilder containerBuilder)
{
    containerBuilder.RegisterModule(new ProductionAutofacModule());
    containerBuilder.RegisterModule(new ResourcesAutofacModule());
    containerBuilder.RegisterModule(new UserAccessAutofacModule());
    containerBuilder.RegisterModule(new WarehousesAutofacModule());
    containerBuilder.RegisterModule(new WorldsAutofacModule());
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

    app.UseSerilogRequestLogging();

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