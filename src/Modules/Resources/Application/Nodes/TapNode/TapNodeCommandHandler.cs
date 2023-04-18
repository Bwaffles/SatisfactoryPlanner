using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Extractors;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
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
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ITappedNodeExistenceChecker _tappedNodeExistenceChecker;
        private readonly ITappedNodeRepository _tappedNodeRepository;

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
                new WorldId(command.WorldId),
                extractor,
                _tappedNodeExistenceChecker);
            await _tappedNodeRepository.AddAsync(tappedNode);

            return tappedNode.Id.Value;
        }
    }
}