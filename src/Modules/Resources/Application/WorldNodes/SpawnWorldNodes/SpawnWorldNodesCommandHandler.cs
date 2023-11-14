using MediatR;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.SpawnWorldNodes
{
    // ReSharper disable once UnusedMember.Global
    internal class SpawnWorldNodesCommandHandler : ICommandHandler<SpawnWorldNodesCommand>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IWorldNodeRepository _worldNodeRepository;

        public SpawnWorldNodesCommandHandler(INodeRepository nodeRepository,
            IWorldNodeRepository worldNodeRepository)
        {
            _nodeRepository = nodeRepository;
            _worldNodeRepository = worldNodeRepository;
        }

        public async Task<Unit> Handle(SpawnWorldNodesCommand request, CancellationToken cancellationToken)
        {
            var allNodes = await _nodeRepository.GetAllAsync();

            var worldId = new WorldId(request.WorldId);
            foreach (var node in allNodes)
            {
                var tappedNode = WorldNode.Spawn(worldId, node.Id);
                await _worldNodeRepository.AddAsync(tappedNode);
            }

            return Unit.Value;
        }
    }
}