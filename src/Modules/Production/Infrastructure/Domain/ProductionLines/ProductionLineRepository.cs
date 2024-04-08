using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Infrastructure.Domain.ProductionLines
{
    internal class ProductionLineRepository(ProductionContext context) : IProductionLineRepository
    {
        public async Task AddAsync(ProductionLine productionLine) => await context.ProductionLines.AddAsync(productionLine);
    }
}
