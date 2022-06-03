using SatisfactoryPlanner.Modules.UserAccess.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Application.UserRegistrations.ConfirmRegistration
{
    public class ConfirmRegistrationCommand : CommandBase
    {
        public Guid UserRegistrationId { get; }

        public ConfirmRegistrationCommand(Guid userRegistrationId) => UserRegistrationId = userRegistrationId;
    }
}