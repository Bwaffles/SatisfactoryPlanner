using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.UpgradeExtractor
{
    public class UpgradeExtractorCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public UpgradeExtractorCommand(Guid worldId, Guid nodeId, Guid extractorId)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }
    }
}