using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Extractors.GetExtractors
{
    internal class GetExtractorsQueryHandler : IQueryHandler<GetExtractorsQuery, List<ExtractorDto>>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetExtractorsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<ExtractorDto>> Handle(GetExtractorsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();
            return await ExtractorFactory.GetAvailableExtractors(connection, query.ResourceId);
        }
    }
}
