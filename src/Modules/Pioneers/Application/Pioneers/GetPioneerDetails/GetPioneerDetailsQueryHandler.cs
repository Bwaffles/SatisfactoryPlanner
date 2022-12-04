using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Worlds.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Worlds.Application.Pioneers.GetPioneerDetails
{
    internal class GetPioneerDetailsQueryHandler : IQueryHandler<GetPioneerDetailsQuery, PioneerDetailsDto?>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetPioneerDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory) =>
            _dbConnectionFactory = dbConnectionFactory;

        public async Task<PioneerDetailsDto?> Handle(GetPioneerDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"  SELECT pioneer.id AS {nameof(PioneerDetailsDto.Id)} " +
                               "     FROM worlds.pioneers AS pioneer " +
                               $"   WHERE pioneer.id = @{nameof(GetPioneerDetailsQuery.PioneerId)}";
            return await connection.QuerySingleOrDefaultAsync<PioneerDetailsDto>(
                sql,
                new
                {
                    query.PioneerId
                });
        }
    }
}