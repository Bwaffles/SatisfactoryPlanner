using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions.Rules;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodes;
using System;

namespace SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions
{
    public class ResourceNodeExtraction : Entity, IAggregateRoot
    {
        public ResourceNodeExtractionId Id { get; private set; }

        private readonly ResourceNodeId _resourceNodeId;

        private readonly ExtractorId _extractorId;

        private readonly decimal _amount;

        private readonly string _name;

        private ResourceNodeExtraction() { }

        private ResourceNodeExtraction(ResourceNode resourceNode, Extractor extractor, decimal amount, string name,
            ResourceNodeExtraction existingResouceNodeExtraction)
        {
            // TODO create domain service for calculating the max potentional resources for the node/extractor combo
            // TODO instead of existingResourceNodeExtraction create a service like IUsersCounter that when called does a select statement to check
            // that way the logic is encapsulated in there instead of in the application layer
            CheckRule(new ResourceNodeCannotAlreadyBeExtractedRule(existingResouceNodeExtraction));
            CheckRule(new ResourceNodeMustBeExtractableByTheExtractorRule(resourceNode, extractor));
            CheckRule(new CannotExtractMoreThanTheAvailableResourcesRule(resourceNode, extractor, amount));

            Id = new ResourceNodeExtractionId(Guid.NewGuid());
            _resourceNodeId = resourceNode.Id;
            _extractorId = extractor.Id;
            _amount = amount;
            _name = name;
        }

        public static ResourceNodeExtraction ExtractNew(ResourceNode resourceNode, Extractor extractor, decimal amount, string name,
            ResourceNodeExtraction existingResouceNodeExtraction)
            => new(resourceNode, extractor, amount, name, existingResouceNodeExtraction);
    }
}
