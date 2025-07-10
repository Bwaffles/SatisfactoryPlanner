using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DismantleExtractor
{
    public class DismantleExtractorCommand(Guid worldId, Guid nodeId) : CommandBase
    {
        public Guid WorldId { get; } = worldId;

        public Guid NodeId { get; } = nodeId;
    }

    internal class DismantleExtractorCommandHandler(IWorldNodeRepository worldNodeRepository) : ICommandHandler<DismantleExtractorCommand>
    {
        private readonly IWorldNodeRepository _worldNodeRepository = worldNodeRepository;

        public async Task<Unit> Handle(DismantleExtractorCommand command, CancellationToken cancellationToken)
        {
            var nodeId = new NodeId(command.NodeId);
            var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), nodeId) ?? throw new InvalidCommandException("World node must exist.");

            worldNode.DismantleExtractor();

            return Unit.Value;
        }
    }
}