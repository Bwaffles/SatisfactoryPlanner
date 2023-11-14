using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor
{
    public class DowngradeExtractorCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public DowngradeExtractorCommand(Guid worldId, Guid nodeId, Guid extractorId)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
        }
    }
}