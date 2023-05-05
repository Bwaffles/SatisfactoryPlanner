using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Resources.Domain.Nodes;
using SatisfactoryPlanner.Modules.Resources.Domain.WorldNodes;
using SatisfactoryPlanner.Modules.Resources.Domain.Worlds;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate
{
    // ReSharper disable once UnusedMember.Global
    internal class DecreaseExtractionRateCommandHandler : ICommandHandler<DecreaseExtractionRateCommand>
    {
        private readonly IWorldNodeRepository _worldNodeRepository;

        public DecreaseExtractionRateCommandHandler(IWorldNodeRepository worldNodeRepository)
        {
            _worldNodeRepository = worldNodeRepository;
        }

        public async Task<Unit> Handle(DecreaseExtractionRateCommand command, CancellationToken cancellationToken)
        {
            var worldNode = await _worldNodeRepository.FindAsync(
                new WorldId(command.WorldId),
                new NodeId(command.NodeId));
            if (worldNode == null)
                throw new InvalidCommandException("World node must exist.");

            worldNode.DecreaseExtractionRate(ExtractionRate.Of(command.ExtractionRate));

            return Unit.Value;
        }
    }
}