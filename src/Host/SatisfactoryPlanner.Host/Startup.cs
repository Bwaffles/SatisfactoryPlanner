using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SatisfactoryPlanner.API.Configuration.Authentication;
using SatisfactoryPlanner.API.Configuration.Authorization;
using SatisfactoryPlanner.API.Configuration.ExecutionContext;
using SatisfactoryPlanner.API.Configuration.Extensions;
using SatisfactoryPlanner.API.Configuration.Routing;
using SatisfactoryPlanner.API.Configuration.Validation;
using SatisfactoryPlanner.API.Modules.UserAccess.Users;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Common.EnvironmentInfo;
using SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.EventBus;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.EventBus;
using SatisfactoryPlanner.Modules.Production.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;
using Module = SatisfactoryPlanner.BuildingBlocks.Common.Instrumentation.Module;

namespace SatisfactoryPlanner.Host
{
    public class Startup(IConfiguration configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationLogger = SatisfactoryPlannerLogger.GetSerilogLogger(Module.Application);
            services.AddSerilog(applicationLogger);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddResponseCompression();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ProducesAttribute("application/json"));
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                })
                .AddApplicationPart(typeof(GetCurrentUser).Assembly)
                .AddControllersAsServices();

            services.AddSwaggerDocumentation();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(options =>
            {
                options.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                options.Map<BusinessRuleValidationException>(
                    ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            services.ConfigureAuthenticationService(configuration);
            services.ConfigureAuthorizationService();
        }

        public void Configure(IApplicationBuilder app)
        {
            // TODO custom app work like create default config file, start app event etc.

            var eventsBus = new InMemoryEventBusClient();
            StartModules(app, configuration, eventsBus);

            app.UseForwardedHeaders();
            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogExtensions.EnrichFromRequest);
            // TODO investigate whether app.UseExceptionHandler can provide any value

            app.UseProblemDetails();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();

            // To be used by WorldAuthorization to get world id from the body of the request
            app.Use((context, next) =>
            {
                context.Request.EnableBuffering(1_000_000);
                return next();
            });
            app.UseAuthorization();
            app.UseResponseCompression();

            // Custom Middleware
            app.UseMiddleware<CorrelationMiddleware>();
            // TODO investigate StartingUpMiddleware

            app.UseWebSockets();

            if (BuildInfo.IsDebug)
            {
                app.UseSwaggerDocumentation();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void StartModules(IApplicationBuilder app, IConfiguration configuration, IEventsBus eventsBus)
        {
            var container = app.ApplicationServices.GetAutofacRoot();
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();
            var connectionString = configuration.GetConnectionString("SatisfactoryPlanner") ?? throw new InvalidOperationException("SatisfactoryPlanner connection string not defined.");
            var internalProcessingExecutionInterval = configuration.GetValue<TimeSpan>("InternalProcessingExecutionInterval");
            var logger = SatisfactoryPlannerLogger.GetSerilogLogger();

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
    }
}
