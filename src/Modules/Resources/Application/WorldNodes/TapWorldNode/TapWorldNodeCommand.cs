using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode
{
    public class TapWorldNodeCommand(Guid worldId, Guid nodeId, Guid extractorId) : CommandBase
    {
        public Guid WorldId { get; } = worldId;
        public Guid NodeId { get; } = nodeId;
        public Guid ExtractorId { get; } = extractorId;
    }

    internal class TapWorldNodeCommandHandler(
        IExtractorRepository extractorRepository,
        IWorldNodeRepository worldNodeRepository,
        INodeRepository nodeRepository) : ICommandHandler<TapWorldNodeCommand>
    {
        private readonly IExtractorRepository _extractorRepository = extractorRepository;
        private readonly INodeRepository _nodeRepository = nodeRepository;
        private readonly IWorldNodeRepository _worldNodeRepository = worldNodeRepository;

        public async Task<Unit> Handle(TapWorldNodeCommand command, CancellationToken cancellationToken)
        {
            var nodeId = new NodeId(command.NodeId);
            var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), nodeId) ?? throw new InvalidCommandException("World node must exist.");
            var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId)) ?? throw new InvalidCommandException("Extractor must exist.");

            var resourceId = (await _nodeRepository.GetByIdAsync(nodeId)).GetResourceId();

            worldNode.Tap(extractor, resourceId);

            return Unit.Value;
        }
    }
}