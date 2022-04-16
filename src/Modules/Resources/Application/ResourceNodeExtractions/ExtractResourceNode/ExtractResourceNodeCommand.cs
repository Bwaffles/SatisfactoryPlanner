using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    public class ExtractResourceNodeCommand : CommandBase<Guid>
    {
        public ExtractResourceNodeCommand(Guid resourceNodeId, Guid extractorId, decimal amount, string name)
        {
            ResourceNodeId = resourceNodeId;
            ExtractorId = extractorId;
            Amount = amount;
            Name = name;
        }

        public Guid ResourceNodeId { get; }

        public Guid ExtractorId { get; }

        public decimal Amount { get; }

        public string Name { get; }
    }
}
