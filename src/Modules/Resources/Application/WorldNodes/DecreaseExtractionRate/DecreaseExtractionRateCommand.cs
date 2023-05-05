using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate
{
    public class DecreaseExtractionRateCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public decimal ExtractionRate { get; }

        public DecreaseExtractionRateCommand(Guid worldId, Guid nodeId, decimal extractionRate)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractionRate = extractionRate;
        }
    }
}