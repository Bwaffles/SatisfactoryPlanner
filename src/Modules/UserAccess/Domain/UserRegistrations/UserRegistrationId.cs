using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationId : TypedIdValueBase
    {
        public UserRegistrationId(Guid value)
            : base(value)
        {
        }
    }
}
