using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions
{
    public class ResourceNodeExtraction : Entity, IAggregateRoot
    {
        public ResourceNodeExtractionId Id { get; private set; }

        private readonly NodeId _nodeId;

        private readonly ExtractorId _extractorId;

        private readonly decimal _amount;

        private readonly string _name;

        private ResourceNodeExtraction() { }

        private ResourceNodeExtraction(Node node, Extractor extractor, decimal amount, string name,
            ResourceNodeExtraction existingResouceNodeExtraction)
        {
            // TODO create domain service for calculating the max potentional resources for the node/extractor combo
            // TODO instead of existingResourceNodeExtraction create a service like IUsersCounter that when called does a select statement to check
            // that way the logic is encapsulated in there instead of in the application layer
            CheckRule(new NodeCannotAlreadyBeExtractedRule(existingResouceNodeExtraction));
            CheckRule(new NodeMustBeExtractableByTheExtractorRule(node, extractor));
            CheckRule(new CannotExtractMoreThanTheAvailableResourcesRule(node, extractor, amount));

            Id = new ResourceNodeExtractionId(Guid.NewGuid());
            _nodeId = node.Id;
            _extractorId = extractor.Id;
            _amount = amount;
            _name = name;
        }

        public static ResourceNodeExtraction ExtractNew(Node node, Extractor extractor, decimal amount, string name,
            ResourceNodeExtraction existingResouceNodeExtraction)
            => new(node, extractor, amount, name, existingResouceNodeExtraction);
    }
}
