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

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DecreaseExtractionRate;

public class DecreaseExtractionRateCommand(Guid worldId, Guid nodeId, decimal extractionRate) : CommandBase
{
    public Guid WorldId { get; } = worldId;
    public Guid NodeId { get; } = nodeId;
    public decimal ExtractionRate { get; } = extractionRate;
}

internal class DecreaseExtractionRateCommandHandler(IWorldNodeRepository worldNodeRepository) : ICommandHandler<DecreaseExtractionRateCommand>
{
    private readonly IWorldNodeRepository _worldNodeRepository = worldNodeRepository;

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