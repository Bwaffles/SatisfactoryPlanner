using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using System;
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

namespace SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.DowngradeExtractor;

public class DowngradeExtractorCommand(Guid worldId, Guid nodeId, Guid extractorId) : CommandBase
{
    public Guid WorldId { get; } = worldId;
    public Guid NodeId { get; } = nodeId;
    public Guid ExtractorId { get; } = extractorId;
}

internal class DowngradeExtractorCommandHandler(IExtractorRepository extractorRepository,
    IWorldNodeRepository worldNodeRepository,
    INodeRepository nodeRepository,
    IExtractionRateCalculator extractionRateCalculator) : ICommandHandler<DowngradeExtractorCommand>
{
    private readonly IExtractionRateCalculator _extractionRateCalculator = extractionRateCalculator;
    private readonly IExtractorRepository _extractorRepository = extractorRepository;
    private readonly INodeRepository _nodeRepository = nodeRepository;
    private readonly IWorldNodeRepository _worldNodeRepository = worldNodeRepository;

    public async Task<Unit> Handle(DowngradeExtractorCommand command, CancellationToken cancellationToken)
    {
        var nodeId = new NodeId(command.NodeId);
        var worldNode = await _worldNodeRepository.FindAsync(new WorldId(command.WorldId), nodeId);
        if (worldNode == null)
            throw new InvalidCommandException("World node must exist.");

        var extractor = await _extractorRepository.FindByIdAsync(new ExtractorId(command.ExtractorId));
        if (extractor == null)
            throw new InvalidCommandException("Extractor must exist.");

        var resourceId = (await _nodeRepository.GetByIdAsync(nodeId)).GetResourceId();
        var currentExtractor = await _extractorRepository.FindByIdAsync(worldNode.GetExtractorId());

        worldNode.DowngradeExtractor(extractor, resourceId, currentExtractor, _extractionRateCalculator);

        return Unit.Value;
    }
}