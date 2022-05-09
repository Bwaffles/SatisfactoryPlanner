using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.UserAccess.Domain.UserRegistrations
{
    public class UserRegistrationId : TypedIdValueBase
    {
        public UserRegistrationId(Guid value)
            : base(value)
        {
        }
    }
}
