using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Resources;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceExtractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    internal class ExtractResourceNodeCommandHandler : ICommandHandler<ExtractResourceNodeCommand, Guid>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IResourceExtractorRepository _resourceExtractorRepository;
        private readonly IResourceNodeExtractionRepository _resourceNodeExtractionRepository;

        public ExtractResourceNodeCommandHandler(
            IDbConnectionFactory dbConnectionFactory,
            IResourceExtractorRepository resourceExtractorRepository,
            IResourceNodeExtractionRepository resourceNodeExtractionRepository
            )
        {
            _dbConnectionFactory = dbConnectionFactory;
            _resourceExtractorRepository = resourceExtractorRepository;
            _resourceNodeExtractionRepository = resourceNodeExtractionRepository;
        }

        public async Task<Guid> Handle(ExtractResourceNodeCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            var resourceNode = await ResourceNodeFactory.GetResourceNode(connection, command.ResourceNodeId);
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
