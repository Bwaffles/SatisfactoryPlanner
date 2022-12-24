using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode
{
    public class TapNodeCommand : CommandBase<Guid>
    {
        public TapNodeCommand(Guid worldId, Guid nodeId, Guid extractorId, decimal amountToExtract, string name)
        {
            WorldId = worldId;
            NodeId = nodeId;
            ExtractorId = extractorId;
            AmountToExtract = amountToExtract;
            Name = name;
        }

        public Guid WorldId { get; }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public decimal AmountToExtract { get; }

        public string Name { get; }
    }
}
