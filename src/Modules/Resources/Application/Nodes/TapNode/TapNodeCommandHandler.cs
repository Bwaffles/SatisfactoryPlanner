using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode
{
    // ReSharper disable once UnusedMember.Global
    internal class TapNodeCommandHandler : ICommandHandler<TapNodeCommand>
    {
        private readonly IExtractorRepository _extractorRepository;
        private readonly IWorldNodeRepository _worldNodeRepository;

        public TapNodeCommandHandler(IExtractorRepository extractorRepository,
            IWorldNodeRepository worldNodeRepository)
        {
            _extractorRepository = extractorRepository;
            _worldNodeRepository = worldNodeRepository;
        }

        public async Task<Unit> Handle(TapNodeCommand command, CancellationToken cancellationToken)
        {
            var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), new NodeId(command.NodeId));
            if (worldNode == null)
                throw new InvalidCommandException("World node must exist.");

            var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId));
            if (extractor == null)
                throw new InvalidCommandException("Extractor to tap the node with must exist.");

            worldNode.Tap(extractor.Id);

            return Unit.Value;
        }
    }
}