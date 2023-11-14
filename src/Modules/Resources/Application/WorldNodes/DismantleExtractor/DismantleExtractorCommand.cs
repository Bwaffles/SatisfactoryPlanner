using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor
{
    public class DismantleExtractorCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public DismantleExtractorCommand(Guid worldId, Guid nodeId)
        {
            WorldId = worldId;
            NodeId = nodeId;
        }
    }
}