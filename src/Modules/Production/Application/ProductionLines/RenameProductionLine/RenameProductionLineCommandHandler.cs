using MediatR;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Commands;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.RenameProductionLine
{
    internal class RenameProductionLineCommandHandler(IProductionLineRepository productionLineRepository, IProductionLineCounter productionLineCounter) : ICommandHandler<RenameProductionLineCommand>
    {
        public async Task<Unit> Handle(RenameProductionLineCommand request, CancellationToken cancellationToken)
        {
            var productionLine = await productionLineRepository.FindAsync(new WorldId(request.WorldId), new ProductionLineId(request.ProductionLineId))
                ?? throw new InvalidCommandException("Production line not found in the world.");

            productionLine.Rename(ProductionLineName.As(request.Name), productionLineCounter);

            return Unit.Value;
        }
    }
}
