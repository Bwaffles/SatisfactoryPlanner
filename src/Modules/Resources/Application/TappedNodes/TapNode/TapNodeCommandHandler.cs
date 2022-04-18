using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.ResourceNodeExtractions;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.TapNode
{
    internal class TapNodeCommandHandler : ICommandHandler<TapNodeCommand, Guid>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ITappedNodeRepository _tappedNodeRepository;
        private readonly ITappedNodeExistenceChecker _tappedNodeExistenceChecker;

        public TapNodeCommandHandler(
            IDbConnectionFactory dbConnectionFactory,
            ITappedNodeRepository tappedNodeRepository,
            ITappedNodeExistenceChecker tappedNodeExistenceChecker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _tappedNodeRepository = tappedNodeRepository;
            _tappedNodeExistenceChecker = tappedNodeExistenceChecker;
        }

        public async Task<Guid> Handle(TapNodeCommand command, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            var node = await NodeFactory.GetNode(connection, command.NodeId);
            if (node == null)
                throw new InvalidCommandException("Node to tap must exist.");

            var extractor = await ExtractorFactory.GetExtractor(connection, command.ExtractorId);
            if (extractor == null)
                throw new InvalidCommandException("Extractor to tap the node with must exist.");

            var tappedNode = node.Tap(
                extractor,
                command.AmountToExtract,
                command.Name,
                _tappedNodeExistenceChecker);
            await _tappedNodeRepository.AddAsync(tappedNode);

            return tappedNode.Id.Value;
        }
    }
}
