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
using SatisfactoryPlanner.API.Configuration.Routing;
using SatisfactoryPlanner.API.Configuration.Validation;
using SatisfactoryPlanner.API.Modules.Production;
using SatisfactoryPlanner.API.Modules.Resources;
using SatisfactoryPlanner.API.Modules.UserAccess;
using SatisfactoryPlanner.API.Modules.Worlds;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

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

using (LogContext.PushProperty("Context", "Startup"))
{
    var _loggerForApi = _logger.ForContext("Module", "API");
    _loggerForApi.Information("Application started.");

    var builder = WebApplication.CreateBuilder(args);

    ConfigureServices(builder);

    // Using a custom DI container.
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(RegisterModules);

    var app = builder.Build();

    Configure(app, app.Environment, _logger, builder.Configuration);

    app.MapControllers();

    app.Run();
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
    containerBuilder.RegisterModule(new WorldsAutofacModule());
    containerBuilder.RegisterModule(new ResourcesAutofacModule());
    containerBuilder.RegisterModule(new ProductionAutofacModule());
    containerBuilder.RegisterModule(new UserAccessAutofacModule());
}

static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger, ConfigurationManager configuration)
{
    app.UseCors(builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );

    InitializeModules(app, logger, configuration);

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

    app.UseAuthentication();

    // To be used by WorldAuthorization to get world id from the body of the request
    app.Use((context, next) =>
    {
        context.Request.EnableBuffering(1_000_000);
        return next();
    });
    app.UseAuthorization();
}

static void InitializeModules(IApplicationBuilder app, ILogger logger, ConfigurationManager configuration)
{
    var container = app.ApplicationServices.GetAutofacRoot();
    var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
    var connectionString = configuration.GetConnectionString("SatisfactoryPlanner") ?? throw new InvalidOperationException("SatisfactoryPlanner connection string not defined.");

    ProductionStartup.Initialize(
        connectionString,
        executionContextAccessor,
        logger
    );

    ResourcesStartup.Initialize(
        connectionString,
        executionContextAccessor,
        logger
    );

    UserAccessStartup.Initialize(
        connectionString,
        executionContextAccessor,
        logger);

    WorldsStartup.Initialize(
        connectionString,
        executionContextAccessor,
        logger
    );
}
