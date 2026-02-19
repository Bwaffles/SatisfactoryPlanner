using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SatisfactoryPlanner.API.Configuration.Authentication
{
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Configure everything needed for authentication of the API.
        /// </summary>
        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            // The warnings in logs about signing keys is because of this issue https://github.com/dotnet/aspnetcore/issues/47410.
            // AddAuthentication calls AddDataProtection because it's a general function for different auth types,
            // but we aren't using data protection for JWT tokens and it's giving us a warning. Seems to be put in the backlog by microsoft for now.
            // I have ignored the warnings for the DataProtection namespace in the logs so that no one spends time on this again.
            // Can revisit later on to see if a solution has been implemented.
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                    options.Audience = configuration["Auth0:Audience"];
                });

            return services;
        }
    }
}
