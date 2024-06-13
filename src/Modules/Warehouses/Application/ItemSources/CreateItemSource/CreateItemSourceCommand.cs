using MediatR;
using SatisfactoryPlanner.Modules.Warehouses.Application.Configuration;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;

namespace SatisfactoryPlanner.Modules.Warehouses.Application.ItemSources.CreateItemSource
{
    public record CreateItemSourceCommand : CommandBase;

    internal class CreateItemSourceCommandHandler : ICommandHandler<CreateItemSourceCommand>
    {
        public Task<Unit> Handle(CreateItemSourceCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
