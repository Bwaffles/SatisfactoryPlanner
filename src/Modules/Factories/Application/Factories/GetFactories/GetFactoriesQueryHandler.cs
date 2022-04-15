using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.GetFactories
{
    internal class GetFactoriesQueryHandler : IQueryHandler<GetFactoriesQuery, List<FactoryDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetFactoriesQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<FactoryDto>> Handle(GetFactoriesQuery request, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<FactoryDto>(
                "SELECT " +
                $"factory.id AS {nameof(FactoryDto.Id)}, " +
                $"factory.name AS {nameof(FactoryDto.Name)}, " +
                $"factory.built_under_factory_id AS {nameof(FactoryDto.BuiltUnderFactoryId)} " +
                "FROM factories.factories AS factory"))
                .AsList();
        }
    }
}
