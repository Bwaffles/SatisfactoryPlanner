using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Factories.Domain.ResourceNodes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.ResourceNodeExtractions.ExtractResourceNode
{
    internal class ExtractResourceNodeCommandHandler : ICommandHandler<ExtractResourceNodeCommand, Guid>
    {
        private readonly IResourceExtractorRepository _resourceExtractorRepository;
        private readonly IResourceNodeExtractionRepository _resourceNodeExtractionRepository;
        private readonly IResourceNodeRepository _resourceNodeRepository;

        public ExtractResourceNodeCommandHandler(
            IResourceNodeRepository resourceNodeRepository,
            IResourceExtractorRepository resourceExtractorRepository,
            IResourceNodeExtractionRepository resourceNodeExtractionRepository
            )
        {
            _resourceNodeRepository = resourceNodeRepository;
            _resourceExtractorRepository = resourceExtractorRepository;
            _resourceNodeExtractionRepository = resourceNodeExtractionRepository;
        }

        public async Task<Guid> Handle(ExtractResourceNodeCommand command, CancellationToken cancellationToken)
        {
            var resourceNode = await _resourceNodeRepository.GetByIdAsync(new ResourceNodeId(command.ResourceNodeId));
            if (resourceNode == null)
                throw new InvalidCommandException("Resource node to extract from must exist.");

            var resourceExtractor = await _resourceExtractorRepository.GetByIdAsync(new ResourceExtractorId(command.ResourceExtractorId));
            if (resourceExtractor == null)
                throw new InvalidCommandException("Resource extractor to extract with must exist.");

            var existingResouceNodeExtraction = await _resourceNodeExtractionRepository.GetByResourceNodeIdAsync(resourceNode.Id);

            var resourceNodeExtraction = ResourceNodeExtraction.ExtractNew(
                resourceNode,
                resourceExtractor,
                command.Amount,
                command.Name,
                existingResouceNodeExtraction);
            await _resourceNodeExtractionRepository.AddAsync(resourceNodeExtraction);

            return resourceNodeExtraction.Id.Value;
        }
    }
}
