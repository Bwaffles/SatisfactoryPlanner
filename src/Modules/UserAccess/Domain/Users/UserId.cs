using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.UserAccess.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}
