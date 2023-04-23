using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Extractors;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Nodes.TapNode
{
    // ReSharper disable once UnusedMember.Global
    internal class TapNodeCommandHandler : ICommandHandler<TapNodeCommand>
    {
        private readonly IExtractorRepository _extractorRepository;
        private readonly ITappedNodeRepository _tappedNodeRepository;

        public TapNodeCommandHandler(IExtractorRepository extractorRepository,
            ITappedNodeRepository tappedNodeRepository)
        {
            _extractorRepository = extractorRepository;
            _tappedNodeRepository = tappedNodeRepository;
        }

        public async Task<Unit> Handle(TapNodeCommand command, CancellationToken cancellationToken)
        {
            var tappedNode = await _tappedNodeRepository.FindAsync(new WorldId(command.WorldId), new NodeId(command.NodeId));
            if (tappedNode == null)
                throw new InvalidCommandException("Tapped node must exist.");

            var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId));
            if (extractor == null)
                throw new InvalidCommandException("Extractor to tap the node with must exist.");

            tappedNode.Tap(extractor.Id);

            return Unit.Value;
        }
    }
}