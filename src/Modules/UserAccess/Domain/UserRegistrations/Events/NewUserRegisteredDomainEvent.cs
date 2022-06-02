using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations.Events
{
    public class NewUserRegisteredDomainEvent : DomainEventBase
    {
        public UserRegistrationId UserRegistrationId { get; }

        public string Username { get; }

        public string Email { get; }

        public DateTime RegisterDate { get; }

        public string ConfirmLink { get; }

        public NewUserRegisteredDomainEvent(
            UserRegistrationId userRegistrationId,
            string username,
            string email,
            DateTime registerDate,
            string confirmLink)
        {
            UserRegistrationId = userRegistrationId;
            Username = username;
            Email = email;
            RegisterDate = registerDate;
            ConfirmLink = confirmLink;
        }
    }
}
