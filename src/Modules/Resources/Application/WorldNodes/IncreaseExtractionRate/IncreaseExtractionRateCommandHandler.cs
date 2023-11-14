using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.IncreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class IncreaseExtractionRateCommandHandler : ICommandHandler<IncreaseExtractionRateCommand>
    {
        private readonly IExtractionRateCalculator _extractionRateCalculator;
        private readonly IWorldNodeRepository _worldNodeRepository;

        public IncreaseExtractionRateCommandHandler(IWorldNodeRepository worldNodeRepository,
            IExtractionRateCalculator extractionRateCalculator)
        {
            _worldNodeRepository = worldNodeRepository;
            _extractionRateCalculator = extractionRateCalculator;
        }

        public async Task<Unit> Handle(IncreaseExtractionRateCommand command, CancellationToken cancellationToken)
        {
            var worldNode = await _worldNodeRepository.FindAsync(
                new WorldId(command.WorldId),
                new NodeId(command.NodeId));
            if (worldNode == null)
                throw new InvalidCommandException("World node must exist.");

            worldNode.IncreaseExtractionRate(ExtractionRate.Of(command.ExtractionRate), _extractionRateCalculator);

            return Unit.Value;
        }
    }
}