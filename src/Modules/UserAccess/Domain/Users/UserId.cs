using SatisfactoryPlanner.BuildingBlocks.Domain;
using System;

namespace SatisfactoryPlanner.Modules.UserAccess.Domain.Users
{
    public record UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value) { }
    }
}