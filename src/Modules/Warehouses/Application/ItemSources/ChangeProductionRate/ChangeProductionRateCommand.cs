using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.ChangeProductionRate;

[method: JsonConstructor]
internal record ChangeProductionRateCommand(Guid Id, Guid WorldId, Guid SourceId, string ItemId, decimal Rate) : InternalCommandBase(Id);

internal class ChangeProductionRateCommandHandler(IItemSourcesRepository itemSourcesRepository) : ICommandHandler<ChangeProductionRateCommand>
{
    private readonly IItemSourcesRepository _itemSourcesRepository = itemSourcesRepository;

    public async Task<Unit> Handle(ChangeProductionRateCommand request, CancellationToken cancellationToken)
    {
        var itemSource = await _itemSourcesRepository.FindAsync(new WorldId(request.WorldId), new SourceId(request.SourceId))
            ?? throw new InvalidCommandException("Item source must exist.");

        var item = Item.GetById(request.ItemId);

        itemSource.ChangeProductionRate(item, Rate.Of(request.Rate));

        return Unit.Value;
    }
}