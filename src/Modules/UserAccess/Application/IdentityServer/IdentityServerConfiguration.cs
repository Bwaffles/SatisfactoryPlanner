using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Collections.Generic;
using System.Security.Claims;

namespace SatisfactoryPlanner.UserAccess.Application.IdentityServer
{
    public class IdentityServerConfiguration
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "cwm.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "satisfactoryPlannerAPI.read" }
            }
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("satisfactoryPlannerAPI", "Satisfactory Planner API")
            {
                Scopes = new List<string>{ "satisfactoryPlannerAPI.read", "satisfactoryPlannerAPI.write" },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("satisfactoryPlannerAPI.read"),
            new ApiScope("satisfactoryPlannerAPI.write")
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static List<TestUser> TestUsers => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1144",
                Username = "bwaffles",
                Password = "bwaffles123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "B. Waffles"),
                    new Claim(JwtClaimTypes.GivenName, "B."),
                    new Claim(JwtClaimTypes.FamilyName, "Waffles"),
                    new Claim(JwtClaimTypes.WebSite, "http://bwaffles.com"),
                }
            }
        };
    }
}
