using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SatisfactoryPlanner.API.Configuration.ExecutionContext;
using SatisfactoryPlanner.API.Modules.Factories;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Factories.Infrastructure;

namespace SatisfactoryPlanner.API
{
    public class Startup
    {
        private const string FactoriesConnectionString = "FactoriesConnectionString";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IExecutionContextAccessor, ExecutionContextAccessor>();
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new FactoriesAutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var container = app.ApplicationServices.GetAutofacRoot();

            InitializeModules(container);

            app.UseMiddleware<CorrelationMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SatisfactoryPlanner.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitializeModules(ILifetimeScope container)
        {
            var httpContextAccessor = container.Resolve<IHttpContextAccessor>();
            var executionContextAccessor = new ExecutionContextAccessor(httpContextAccessor);

            //var emailsConfiguration = new EmailsConfiguration(_configuration["EmailsConfiguration:FromEmail"]);

            FactoriesStartup.Initialize(
                   _configuration[FactoriesConnectionString],
                   executionContextAccessor
                   //,
                   //_logger,
                   //emailsConfiguration,
                   //null
                   );
        }
    }
}
