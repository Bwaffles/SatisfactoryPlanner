using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.Users.GetCurrentUser
{
    public class CurrentUserDto
    {
        /// <summary>
        ///     The unique id of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The identifier of the user in Auth0.
        /// </summary>
        public string Auth0UserId { get; set; }
    }
}