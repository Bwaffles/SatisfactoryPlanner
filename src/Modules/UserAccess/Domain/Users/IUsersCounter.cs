namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public interface IUsersCounter
    {
        /// <summary>
        ///     Get the number of users with the given <paramref name="auth0UserId" />.
        /// </summary>
        public int CountUsersWithAuth0UserId(string auth0UserId);
    }
}