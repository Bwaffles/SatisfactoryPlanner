using MediatR;
using Newtonsoft.Json;
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
            var source = Source.Node(new SourceId(request.NodeId), request.NodeName);
            var itemSource = ItemSource.Register(new WorldId(request.WorldId), source);

            var item = Item.GetById(request.ItemId);
            itemSource.Produces(item, Rate.Of(0));

            await _itemSourcesRepository.AddAsync(itemSource);

            return Unit.Value;
        }
    }
}
