namespace SatisfactoryPlanner.API.IntegrationTests
{
    public static class AuthenticatedUser
    {
        private const string defaultAuth0UserId = "api-integration-test-user";

        public static string Auth0UserId { get; set; } = defaultAuth0UserId;

        internal static void Reset() => Auth0UserId = defaultAuth0UserId;
    }
}