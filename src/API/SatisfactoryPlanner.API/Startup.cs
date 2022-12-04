using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SatisfactoryPlanner.API.Configuration;
using SatisfactoryPlanner.API.Configuration.ExecutionContext;
using SatisfactoryPlanner.API.Configuration.Extensions;
using SatisfactoryPlanner.API.Configuration.Validation;
using SatisfactoryPlanner.API.Modules.Factories;
using SatisfactoryPlanner.API.Modules.Resources;
using SatisfactoryPlanner.API.Modules.UserAccess;
using SatisfactoryPlanner.API.Modules.Worlds;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Emails;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.UserAccess.Application.IdentityServer;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Worlds.Infrastructure.Configuration;
using Serilog;
using Serilog.Formatting.Compact;

namespace SatisfactoryPlanner.API
{
    public class Startup
    {
        private static ILogger _logger;
        private static ILogger _loggerForApi;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        /// <summary>
        ///     Get the Auth0 domain.
        /// </summary>
        private string Domain => $"https://{_configuration["Auth0:Domain"]}/";

        public Startup(IConfiguration configuration)
        {
            ConfigureLogger();

            _configuration = configuration;

            _connectionString = _configuration.GetConnectionString("FactoriesConnectionString");
            _loggerForApi.Information($"Connection string: {_connectionString}");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationAuthorization(services);
            ConfigurationAuthentication(services);

            services.AddControllers();

            services.AddSwaggerDocumentation();

            //ConfigureIdentityServer(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(options =>
            {
                options.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                options.Map<BusinessRuleValidationException>(
                    ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(HasPermissionAttribute.HasPermissionPolicyName, policyBuilder =>
            //    {
            //        policyBuilder.Requirements.Add(new HasPermissionAuthorizationRequirement());
            //        policyBuilder.AddAuthenticationSchemes(IdentityServerAuthenticationDefaults.AuthenticationScheme);
            //    });
            //});

            //services.AddScoped<IAuthorizationHandler, HasPermissionAuthorizationHandler>();
        }

        /// <summary>
        ///     Configure the authorization with Auth0.
        /// </summary>
        private void ConfigurationAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:resources",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read:resources", Domain)));
            });

            // Register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        /// <summary>
        ///     Configure the authentication with Auth0.
        /// </summary>
        private void ConfigurationAuthentication(IServiceCollection services) =>
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Domain;
                    options.Audience = _configuration["Auth0:Audience"];
                });

        private void ConfigureIdentityServer(IServiceCollection services) =>
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
                .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
                .AddInMemoryClients(IdentityServerConfiguration.Clients)
                .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
                .AddTestUsers(IdentityServerConfiguration.TestUsers)
                .AddDeveloperSigningCredential();

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new WorldsAutofacModule());
            containerBuilder.RegisterModule(new ResourcesAutofacModule());
            containerBuilder.RegisterModule(new FactoriesAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();

            app.UseSwaggerDocumentation();

            //app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseProblemDetails();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SatisfactoryPlanner.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(),
                    path: "logs/logs.json",
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 5 * 1024 * 1024)
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var executionContextAccessor = container.Resolve<IExecutionContextAccessor>();

            var emailsConfiguration =
                new EmailsConfiguration(_configuration.GetValue<string>("EmailsConfiguration:FromEmail"));

            FactoriesStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                _logger
            );

            WorldsStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                _logger
            );

            ResourcesStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                _logger
            );

            UserAccessStartup.Initialize(
                _connectionString,
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                _configuration["Security:TextEncryptionKey"],
                null);
        }
    }
}