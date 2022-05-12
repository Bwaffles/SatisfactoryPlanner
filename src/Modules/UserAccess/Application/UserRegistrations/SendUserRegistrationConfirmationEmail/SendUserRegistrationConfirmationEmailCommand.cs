using Newtonsoft.Json;
using SatisfactoryPlanner.Modules.UserAccess.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    public class SendUserRegistrationConfirmationEmailCommand : InternalCommandBase
    {
        internal UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }

        internal string ConfirmLink { get; }

        [JsonConstructor]
        public SendUserRegistrationConfirmationEmailCommand(
            Guid id,
            UserRegistrationId userRegistrationId,
            string email,
            string confirmLink)
            : base(id)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
            ConfirmLink = confirmLink;
        }
    }
}