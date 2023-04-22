using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.IncreaseExtractionRate
{
    public class IncreaseExtractionRateCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public decimal ExtractionRate { get; }

        public IncreaseExtractionRateCommand(Guid worldId, Guid nodeId, decimal extractionRate)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractionRate = extractionRate;
        }
    }
}