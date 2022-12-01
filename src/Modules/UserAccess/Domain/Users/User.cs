using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.UserAccess.Domain.Users.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public class User : Entity, IAggregateRoot
    {
        /// <summary>
        ///     The identifier of the user in Auth0.
        /// </summary>
        private readonly string _auth0UserId;

        private readonly List<UserRole> _roles;

        /// <summary>
        ///     The unique id of the user.
        /// </summary>
        public UserId Id { get; }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private User()
        { /* Only for EF. */
        }

        private User(string auth0UserId, IUsersCounter usersCounter)
        {
            if (auth0UserId == "")
                throw new ArgumentException($"{nameof(auth0UserId)} can't be empty", nameof(auth0UserId));

            CheckRule(new UserAuth0UserIdMustBeUniqueRule(auth0UserId, usersCounter));

            Id = new UserId(Guid.NewGuid());
            _auth0UserId = auth0UserId;

        }

        public static User CreatePioneer(string auth0UserId, IUsersCounter usersCounter) 
            => new (auth0UserId, usersCounter);
    }
}