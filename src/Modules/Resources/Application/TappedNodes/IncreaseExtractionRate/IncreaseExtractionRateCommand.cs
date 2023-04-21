using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.IncreaseExtractionRate
{
    public class IncreaseExtractionRateCommand : CommandBase
    {
        public Guid WorldId { get; }

        public Guid TappedNodeId { get; }

        public decimal NewExtractionRate { get; }

        public IncreaseExtractionRateCommand(Guid worldId, Guid tappedNodeId, decimal newExtractionRate)
        {
            WorldId = worldId;
            TappedNodeId = tappedNodeId;
            NewExtractionRate = newExtractionRate;
        }
    }
}