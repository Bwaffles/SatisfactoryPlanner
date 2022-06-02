namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IUsersCounter
    {
        int CountUsersWithUsername(string username);
    }
}
