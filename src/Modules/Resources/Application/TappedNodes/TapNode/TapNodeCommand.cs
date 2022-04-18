using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode
{
    public class TapNodeCommand : CommandBase<Guid>
    {
        public TapNodeCommand(Guid nodeId, Guid extractorId, decimal amountToExtract, string name)
        {
            NodeId = nodeId;
            ExtractorId = extractorId;
            AmountToExtract = amountToExtract;
            Name = name;
        }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public decimal AmountToExtract { get; }

        public string Name { get; }
    }
}
