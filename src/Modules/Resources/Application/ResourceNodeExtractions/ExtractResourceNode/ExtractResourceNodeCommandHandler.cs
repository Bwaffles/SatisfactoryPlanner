using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.ResourceNodeExtractions.ExtractResourceNode
{
    internal class ExtractResourceNodeCommandHandler : ICommandHandler<ExtractResourceNodeCommand, Guid>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IExtractorRepository _extractorRepository;
        private readonly IResourceNodeExtractionRepository _resourceNodeExtractionRepository;

        public ExtractResourceNodeCommandHandler(
            IDbConnectionFactory dbConnectionFactory,
            IExtractorRepository extractorRepository,
            IResourceNodeExtractionRepository resourceNodeExtractionRepository
            )
        {
            _dbConnectionFactory = dbConnectionFactory;
            _extractorRepository = extractorRepository;
            _resourceNodeExtractionRepository = resourceNodeExtractionRepository;
        }

        public async Task<Guid> Handle(ExtractResourceNodeCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            var node = await NodeFactory.GetNode(connection, command.NodeId);
            if (node == null)
                throw new InvalidCommandException("Node to extract from must exist.");

            var extractor = await _extractorRepository.GetByIdAsync(new ExtractorId(command.ExtractorId));
            if (extractor == null)
                throw new InvalidCommandException("Extractor to extract with must exist.");

            var existingResouceNodeExtraction = await _resourceNodeExtractionRepository.GetByNodeIdAsync(node.Id);

            var resourceNodeExtraction = ResourceNodeExtraction.ExtractNew(
                node,
                extractor,
                command.Amount,
                command.Name,
                existingResouceNodeExtraction);
            await _resourceNodeExtractionRepository.AddAsync(resourceNodeExtraction);

            return resourceNodeExtraction.Id.Value;
        }
    }
}
