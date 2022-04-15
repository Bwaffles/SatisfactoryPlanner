using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Application.ResourceNodeExtractions.ExtractResourceNode
{
    public class ExtractResourceNodeCommand : CommandBase<Guid>
    {
        public ExtractResourceNodeCommand(Guid resourceNodeId, Guid resourceExtractorId, decimal amount, string name)
        {
            ResourceNodeId = resourceNodeId;
            ResourceExtractorId = resourceExtractorId;
            Amount = amount;
            Name = name;
        }

        public Guid ResourceNodeId { get; }

        public Guid ResourceExtractorId { get; }

        public decimal Amount { get; }

        public string Name { get; }
    }
}
