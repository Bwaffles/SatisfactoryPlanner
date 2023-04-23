using MediatR;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.SpawnWorldNodes
{
    // ReSharper disable once UnusedMember.Global
    internal class SpawnWorldNodesCommandHandler : ICommandHandler<SpawnWorldNodesCommand>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly ITappedNodeRepository _tappedNodeRepository;

        public SpawnWorldNodesCommandHandler(INodeRepository nodeRepository,
            ITappedNodeRepository tappedNodeRepository)
        {
            _nodeRepository = nodeRepository;
            _tappedNodeRepository = tappedNodeRepository;
        }
        public async Task<Unit> Handle(SpawnWorldNodesCommand request, CancellationToken cancellationToken)
        {
            var allNodes = await _nodeRepository.GetAllAsync();

            var worldId = new WorldId(request.WorldId);
            foreach (var node in allNodes)
            {
                var tappedNode = TappedNode.Spawn(worldId, node.Id);
                await _tappedNodeRepository.AddAsync(tappedNode);
            }

            return Unit.Value;
        }
    }
}
