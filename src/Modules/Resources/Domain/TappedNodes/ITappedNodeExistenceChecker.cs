using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes
{
    public interface ITappedNodeExistenceChecker
    {
        bool IsTapped(Guid nodeId);
    }
}
