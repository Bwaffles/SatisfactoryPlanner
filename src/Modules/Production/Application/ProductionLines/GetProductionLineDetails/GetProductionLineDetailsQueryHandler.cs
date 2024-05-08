using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLineDetails
{
    internal class GetProductionLineDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IQueryHandler<GetProductionLineDetailsQuery, ProductionLineDetailsDto?>
    {
        public async Task<ProductionLineDetailsDto?> Handle(GetProductionLineDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = dbConnectionFactory.GetOpenConnection();

            const string sql =
                "  SELECT " +
                $"        production_line.id AS {nameof(ProductionLineDetailsDto.Id)}, " +
                $"        production_line.name AS {nameof(ProductionLineDetailsDto.Name)} " +
                "    FROM production.production_lines AS production_line " +
                "   WHERE production_line.world_id = @WorldId " +
                "     AND production_line.id = @ProductionLineId;";

            var param = new
            {
                request.WorldId,
                request.ProductionLineId
            };

            return await connection.QuerySingleOrDefaultAsync<ProductionLineDetailsDto>(sql, param);
        }
    }
}
