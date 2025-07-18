using MediatR;
using Newtonsoft.Json;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.GameData.GameData;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Domain.ItemSources;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.RegisterNode
{
    [method: JsonConstructor]
    public record RegisterNodeCommand(Guid Id, Guid WorldId, Guid NodeId, string NodeName, string ItemId) : InternalCommandBase(Id);

    internal class RegisterNodeCommandHandler(IItemSourcesRepository itemSourcesRepository) : ICommandHandler<RegisterNodeCommand>
    {
        private readonly IItemSourcesRepository _itemSourcesRepository = itemSourcesRepository;

        public async Task<Unit> Handle(RegisterNodeCommand request, CancellationToken cancellationToken)
        {
            var worldId = new WorldId(request.WorldId);
            var sourceId = new SourceId(request.NodeId);

            var itemSource = await _itemSourcesRepository.FindAsync(worldId, sourceId);
            if (itemSource != null)
                return Unit.Value;

            var source = Source.Node(sourceId, request.NodeName);
            itemSource = ItemSource.Register(worldId, source);

            var item = Item.FindById(request.ItemId) ?? throw new InvalidCommandException("Item must exist.");
            itemSource.Produces(item, Rate.Of(0));

            await _itemSourcesRepository.AddAsync(itemSource);

            return Unit.Value;
        }
    }
}
