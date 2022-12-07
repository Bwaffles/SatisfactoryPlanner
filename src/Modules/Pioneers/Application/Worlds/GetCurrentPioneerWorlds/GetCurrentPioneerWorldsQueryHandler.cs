using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Worlds.GetCurrentPioneerWorlds
{
    internal class GetCurrentPioneerWorldsQueryHandler : IQueryHandler<GetCurrentPioneerWorldsQuery, List<PioneerWorldDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IExecutionContextAccessor _executionContextAccessor;

        public GetCurrentPioneerWorldsQueryHandler(IDbConnectionFactory dbConnectionFactory,
            IExecutionContextAccessor executionContextAccessor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _executionContextAccessor = executionContextAccessor;
        }

        public async Task<List<PioneerWorldDto>> Handle(GetCurrentPioneerWorldsQuery request,
            CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"SELECT world.id as {nameof(PioneerWorldDto.Id)}" +
                               $"     , world.name as {nameof(PioneerWorldDto.Name)} " +
                               "  FROM worlds.worlds as world " +
                               "  JOIN worlds.world_inhabitants as world_inhabitant ON world_inhabitant.world_id = world.id " +
                               " WHERE world_inhabitant.pioneer_id = @UserId;";

            var param = new
            {
                _executionContextAccessor.UserId
            };

            return (await connection.QueryAsync<PioneerWorldDto>(sql, param)).ToList();
        }
    }
}