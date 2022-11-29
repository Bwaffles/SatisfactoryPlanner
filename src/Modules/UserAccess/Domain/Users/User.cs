using SatisfactoryPlanner.BuildingBlocks.Domain;
using System.Collections.Generic;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        /// <summary>
        ///     The unique id of the user.
        /// </summary>
        public UserId Id { get; private set; }

        /// <summary>
        ///     The identifier of the user in Auth0.
        /// </summary>
        private readonly string _auth0UserId;

        private readonly string _username;

        private readonly string _password;

        private readonly string _email;

        private readonly bool _isActive;

        private readonly List<UserRole> _roles;

        private User() { /* Only for EF. */ }
    }
}
