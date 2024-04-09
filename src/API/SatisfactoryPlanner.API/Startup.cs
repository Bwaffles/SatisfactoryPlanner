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
using ILogger = Serilog.ILogger;

namespace SatisfactoryPlanner.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SatisfactoryPlanner") ??
                                throw new InvalidOperationException(
                                    "SatisfactoryPlanner connection string not defined.");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationAuthentication(services);
            ConfigurationAuthorization(services);

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

        /// <summary>
        ///     Configure the authorization.
        /// </summary>
        private void ConfigurationAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
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

            services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, WorldAuthorizationHandler>();
        }

        /// <summary>
        ///     Configure the authentication with Auth0.
        /// </summary>
        private void ConfigurationAuthentication(IServiceCollection services) =>
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{_configuration["Auth0:Domain"]}/";
                    options.Audience = _configuration["Auth0:Audience"];
                });

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new WorldsAutofacModule());
            containerBuilder.RegisterModule(new ResourcesAutofacModule());
            containerBuilder.RegisterModule(new ProductionAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            InitializeModules(container, logger);

            app.UseMiddleware<CorrelationMiddleware>();


            if (env.IsDevelopment())
            {
                app.UseProblemDetails();
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

        private void InitializeModules(ILifetimeScope container, ILogger logger)
        {
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();

            ProductionStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                logger
            );

            WorldsStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                logger
            );

            ResourcesStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                logger
            );

            UserAccessStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                logger);
        }
    }
}