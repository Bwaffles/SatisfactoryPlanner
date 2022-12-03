using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Pioneers.Application.Configuration.Queries;

namespace SatisfactoryPlanner.Modules.Pioneers.Application.Pioneers.GetPioneerDetails
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
                               "     FROM pioneers.pioneers AS pioneer " +
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