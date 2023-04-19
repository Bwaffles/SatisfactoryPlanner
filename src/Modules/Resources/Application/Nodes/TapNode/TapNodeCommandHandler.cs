using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode
{
    // ReSharper disable once UnusedMember.Global
    internal class TapNodeCommandHandler : ICommandHandler<TapNodeCommand, Guid>
    {
        private readonly IExtractorRepository _extractorRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly ITappedNodeExistenceChecker _tappedNodeExistenceChecker;
        private readonly ITappedNodeRepository _tappedNodeRepository;

        public TapNodeCommandHandler(INodeRepository nodeRepository,
            IExtractorRepository extractorRepository,
            ITappedNodeRepository tappedNodeRepository,
            ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            _nodeRepository = nodeRepository;
            _extractorRepository = extractorRepository;
            _tappedNodeRepository = tappedNodeRepository;
            _tappedNodeExistenceChecker = tappedNodeExistenceChecker;
        }

        public async Task<Guid> Handle(TapNodeCommand command, CancellationToken cancellationToken)
        {
            var node = await _nodeRepository.FindByIdAsync(new NodeId(command.NodeId));
            if (node == null)
                throw new InvalidCommandException("Node to tap must exist.");

            var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId));
            if (extractor == null)
                throw new InvalidCommandException("Extractor to tap the node with must exist.");

            var tappedNode = node.Tap(
                new WorldId(command.WorldId),
                extractor,
                _tappedNodeExistenceChecker);
            await _tappedNodeRepository.AddAsync(tappedNode);

            return tappedNode.Id.Value;
        }
    }
}