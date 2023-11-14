using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Pioneer => new(nameof(Pioneer));

        public string Value { get; }

        private UserRole(string value)
        {
            Value = value;
        }
    }
}