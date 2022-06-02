using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    public class UserUsernameMustBeUniqueRule : IBusinessRule
    {
        private readonly IUsersCounter _usersCounter;
        private readonly string _username;

        internal UserUsernameMustBeUniqueRule(IUsersCounter usersCounter, string username)
        {
            _usersCounter = usersCounter;
            _username = username;
        }

        public bool IsBroken() => _usersCounter.CountUsersWithUsername(_username) > 0;

        public string Message => "User Username must be unique.";
    }
}
