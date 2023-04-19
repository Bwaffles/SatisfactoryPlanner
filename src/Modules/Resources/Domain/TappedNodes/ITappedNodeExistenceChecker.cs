using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    public interface ITappedNodeExistenceChecker
    {
        bool IsTapped(Guid worldId, Guid nodeId);
    }
}
