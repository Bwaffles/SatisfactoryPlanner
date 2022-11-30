using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Rules
{
    /// <summary>
    ///     Check that the auth0 user id isn't already assigned to a user.
    ///     This is how we connect the auth0 login to the user in the database.
    /// </summary>
    public class UserAuth0UserIdMustBeUniqueRule : IBusinessRule
    {
        private readonly string _auth0UserId;
        private readonly IUsersCounter _usersCounter;

        public UserAuth0UserIdMustBeUniqueRule(string auth0UserId, IUsersCounter usersCounter)
        {
            _auth0UserId = auth0UserId;
            _usersCounter = usersCounter;
        }

        public string Message => "User's Auth0UserId must be unique.";

        public bool IsBroken() => _usersCounter.CountUsersWithAuth0UserId(_auth0UserId) > 0;
    }
}