using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations
{
    public interface IUserRegistrationRepository
    {
        Task AddAsync(UserRegistration userRegistration);

        /// <summary>
        ///     Get the <see cref="UserRegistration" /> by its id.
        /// </summary>
        /// <returns>
        ///     Returns the <see cref="UserRegistration" /> if found, otherwise returns null.
        /// </returns>
        Task<UserRegistration> GetByIdAsync(UserRegistrationId userRegistrationId);
    }
}