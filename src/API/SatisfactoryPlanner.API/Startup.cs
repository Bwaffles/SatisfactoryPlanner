using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SatisfactoryPlanner.API.Configuration.ExecutionContext;
using SatisfactoryPlanner.API.Configuration.Validation;
using SatisfactoryPlanner.API.Modules.Factories;
using SatisfactoryPlanner.API.Modules.Resources;
using SatisfactoryPlanner.API.Modules.UserAccess;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.BuildingBlocks.Infrastructure.Emails;
using SatisfactoryPlanner.Modules.Factories.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.Resources.Infrastructure.Configuration;
using SatisfactoryPlanner.Modules.UserAccess.Application.IdentityServer;
using SatisfactoryPlanner.Modules.UserAccess.Infrastructure.Configuration;
using Serilog;
using Serilog.Formatting.Compact;

namespace SatisfactoryPlanner.API
{
    public class Startup
    {
        private const string FactoriesConnectionString = "FactoriesConnectionString";
        private static ILogger _logger;
        private static ILogger _loggerForApi;

        public Startup(IConfiguration configuration)
        {
            ConfigureLogger();

            _configuration = configuration;

            _loggerForApi.Information("Connection string:" + _configuration.GetConnectionString(FactoriesConnectionString));
        }

        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SatisfactoryPlanner.API", Version = "v1" });
            });

            ConfigureIdentityServer(services);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
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

        private void ConfigureIdentityServer(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(IdentityServerConfiguration.IdentityResources)
                .AddInMemoryApiResources(IdentityServerConfiguration.ApiResources)
                .AddInMemoryClients(IdentityServerConfiguration.Clients)
                .AddInMemoryApiScopes(IdentityServerConfiguration.ApiScopes)
                .AddTestUsers(IdentityServerConfiguration.TestUsers)
                .AddDeveloperSigningCredential();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ResourcesAutofacModule());
            containerBuilder.RegisterModule(new FactoriesAutofacModule());
            containerBuilder.RegisterModule(new UserAccessAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();

            //app.UseSwaggerDocumentation();

            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseProblemDetails();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SatisfactoryPlanner.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureLogger()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] [{Module}] [{Context}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
                .CreateLogger();

            _loggerForApi = _logger.ForContext("Module", "API");

            _loggerForApi.Information("Logger configured");
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            var emailsConfiguration = new EmailsConfiguration(_configuration.GetValue<string>("EmailsConfiguration:FromEmail"));

            ResourcesStartup.Initialize(
                   _configuration.GetConnectionString(FactoriesConnectionString),
                   executionContextAccessor,
                   _logger
                   //,
                   //emailsConfiguration,
                   //null
                   );

            FactoriesStartup.Initialize(
                   _configuration.GetConnectionString(FactoriesConnectionString),
                   executionContextAccessor,
                   _logger
                   //,
                   //emailsConfiguration,
                   //null
                   );

            UserAccessStartup.Initialize(
                _configuration.GetConnectionString(FactoriesConnectionString),
                executionContextAccessor,
                _logger,
                emailsConfiguration,
                _configuration["Security:TextEncryptionKey"],
                null);
        }
    }
}
