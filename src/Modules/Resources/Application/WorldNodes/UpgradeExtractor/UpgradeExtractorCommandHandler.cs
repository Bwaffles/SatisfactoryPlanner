﻿using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.UpgradeExtractor
{
    // ReSharper disable once UnusedMember.Global
    internal class UpgradeExtractorCommandHandler : ICommandHandler<UpgradeExtractorCommand>
    {
        private readonly IExtractorRepository _extractorRepository;
        private readonly IWorldNodeRepository _worldNodeRepository;
        private readonly INodeRepository _nodeRepository;

        public UpgradeExtractorCommandHandler(IExtractorRepository extractorRepository,
            IWorldNodeRepository worldNodeRepository,
            INodeRepository nodeRepository)
        {
            _extractorRepository = extractorRepository;
            _worldNodeRepository = worldNodeRepository;
            _nodeRepository = nodeRepository;
        }

        public async Task<Unit> Handle(UpgradeExtractorCommand command, CancellationToken cancellationToken)
        {
            var nodeId = new NodeId(command.NodeId);
            var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), nodeId);
            if (worldNode == null)
                throw new InvalidCommandException("World node must exist.");

            var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId));
            if (extractor == null)
                throw new InvalidCommandException("Extractor must exist.");

            var resourceId = (await _nodeRepository.GetByIdAsync(nodeId)).GetResourceId();
            var currentExtractor = await _extractorRepository.FindByIdAsync(worldNode.GetExtractorId());

            worldNode.UpgradeExtractor(extractor, resourceId, currentExtractor);

            return Unit.Value;
        }
    }
}