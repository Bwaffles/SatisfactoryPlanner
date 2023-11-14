using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor
{
    // ReSharper disable once UnusedMember.Global
    internal class DismantleExtractorCommandHandler : ICommandHandler<DismantleExtractorCommand>
    {
        private readonly IWorldNodeRepository _worldNodeRepository;

        public DismantleExtractorCommandHandler(IWorldNodeRepository worldNodeRepository)
        {
            _worldNodeRepository = worldNodeRepository;
        }

        public async Task<Unit> Handle(DismantleExtractorCommand command, CancellationToken cancellationToken)
        {
            var nodeId = new NodeId(command.NodeId);
            var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), nodeId);
            if (worldNode == null)
                throw new InvalidCommandException("World node must exist.");

            worldNode.DismantleExtractor();

            return Unit.Value;
        }
    }
}