using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    public class ExtractResourceNodeCommand : CommandBase<Guid>
    {
        public ExtractResourceNodeCommand(Guid nodeId, Guid extractorId, decimal amount, string name)
        {
            NodeId = nodeId;
            ExtractorId = extractorId;
            Amount = amount;
            Name = name;
        }

        public Guid NodeId { get; }

        public Guid ExtractorId { get; }

        public decimal Amount { get; }

        public string Name { get; }
    }
}
