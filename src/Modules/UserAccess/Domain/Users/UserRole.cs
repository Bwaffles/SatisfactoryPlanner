using SatisfactoryPlanner.BuildingBlocks.Domain;

namespace SatisfactoryPlanner.UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Member => new(nameof(Member));

        public string Value { get; }

        private UserRole(string value)
        {
            Value = value;
        }
    }
}
