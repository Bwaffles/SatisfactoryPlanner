using SatisfactoryPlanner.BuildingBlocks.Domain;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions.Rules;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System;

namespace SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions
{
    public class ResourceNodeExtraction : Entity, IAggregateRoot
    {
        public ResourceNodeExtractionId Id { get; private set; }

        private ResourceNodeId _resourceNodeId;

        private ResourceExtractorId _resourceExtractorId;

        private decimal _amount;

        private string _name;

        private ResourceNodeExtraction() { }

        private ResourceNodeExtraction(ResourceNode resourceNode, ResourceExtractor resourceExtractor, decimal amount, string name, 
            ResourceNodeExtraction existingResouceNodeExtraction)
        {
            CheckRule(new ResourceNodeCannotAlreadyBeExtractedRule(existingResouceNodeExtraction));
            CheckRule(new ResourceNodeMustBeExtractableByTheExtractorRule(resourceNode, resourceExtractor));
            CheckRule(new CannotExtractMoreThanTheAvailableResourcesRule(resourceNode, resourceExtractor, amount));

            Id = new ResourceNodeExtractionId(Guid.NewGuid());
            _resourceNodeId = resourceNode.Id;
            _resourceExtractorId = resourceExtractor.Id;
            _amount = amount;
            _name = name;
        }

        public static ResourceNodeExtraction ExtractNew(ResourceNode resourceNode, ResourceExtractor resourceExtractor, decimal amount, string name, 
            ResourceNodeExtraction existingResouceNodeExtraction)
            => new(resourceNode, resourceExtractor, amount, name, existingResouceNodeExtraction);
    }
}
