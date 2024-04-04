using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines
{
    internal class GetProductionLinesQueryHandler(IDbConnectionFactory dbConnectionFactory) 
        : IQueryHandler<GetProductionLinesQuery, List<ProductionLineDto>>
    {
        public async Task<List<ProductionLineDto>> Handle(GetProductionLinesQuery request, CancellationToken cancellationToken)
        {
            var connection = dbConnectionFactory.GetOpenConnection();

            const string sql = 
                "  SELECT " + 
                $"        production_line.id AS {nameof(ProductionLineDto.Id)}, " + 
                $"        production_line.name AS {nameof(ProductionLineDto.Name)} " + 
                "    FROM production.production_lines AS production_line " +
                "   WHERE production_line.world_id = @WorldId " +
                "ORDER BY production_line.name;";

            var param = new
            {
                request.WorldId
            };

            return (await connection.QueryAsync<ProductionLineDto>(sql, param)).AsList();
        }
    }
}
