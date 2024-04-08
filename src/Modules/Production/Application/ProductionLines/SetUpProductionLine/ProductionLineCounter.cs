using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Production.Domain.ProductionLines;

namespace SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine
{
    public class ProductionLineCounter(IDbConnectionFactory dbConnectionFactory) : IProductionLineCounter
    {
        public int CountProductionLinesWithName(WorldId worldId, ProductionLineName name)
        {
            var connection = dbConnectionFactory.GetOpenConnection();
            const string sql =
                "SELECT COUNT(*) " +
                "  FROM production.production_lines AS production_line " +
                " WHERE production_line.world_id = @WorldId " +
                "   AND lower(production_line.name) = lower(@Name)";
            var param = new
            {
                WorldId = worldId.Value,
                Name = name.Value.Value
            };

            return connection.QuerySingle<int>(sql, param);
        }
    }
}
