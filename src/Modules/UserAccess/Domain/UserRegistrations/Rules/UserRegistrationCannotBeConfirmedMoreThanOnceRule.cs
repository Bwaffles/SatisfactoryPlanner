using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Rules
{
    public class UserRegistrationCannotBeConfirmedMoreThanOnceRule : IBusinessRule
    {
        private readonly UserRegistrationStatus _currentStatus;

        public UserRegistrationCannotBeConfirmedMoreThanOnceRule(UserRegistrationStatus currentStatus) =>
            _currentStatus = currentStatus;

        public bool IsBroken() => _currentStatus == UserRegistrationStatus.Confirmed;

        public string Message => "User registration cannot be confirmed more than once.";
    }
}