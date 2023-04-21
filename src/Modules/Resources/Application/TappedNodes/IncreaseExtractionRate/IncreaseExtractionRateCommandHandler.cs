using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.TappedNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.IncreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class IncreaseExtractionRateCommandHandler : ICommandHandler<IncreaseExtractionRateCommand>
    {
        private readonly ITappedNodeRepository _tappedNodeRepository;
        private readonly IExtractionRateCalculator _extractionRateCalculator;

        public IncreaseExtractionRateCommandHandler(ITappedNodeRepository tappedNodeRepository, IExtractionRateCalculator extractionRateCalculator)
        {
            _tappedNodeRepository = tappedNodeRepository;
            _extractionRateCalculator = extractionRateCalculator;
        }

        public async Task<Unit> Handle(IncreaseExtractionRateCommand command, CancellationToken cancellationToken)
        {
            var tappedNode = await _tappedNodeRepository.FindAsync(
                new WorldId(command.WorldId),
                new TappedNodeId(command.TappedNodeId));
            if (tappedNode == null)
                throw new InvalidCommandException("Tapped node must exist.");

            tappedNode.IncreaseExtractionRate(ExtractionRate.Of(command.NewExtractionRate), _extractionRateCalculator);

            return Unit.Value;
        }
    }
}