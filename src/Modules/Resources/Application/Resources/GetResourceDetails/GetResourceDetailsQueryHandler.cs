﻿using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails
{
    // ReSharper disable once UnusedMember.Global
    internal class GetResourceDetailsQueryHandler : IQueryHandler<GetResourceDetailsQuery, ResourceDetailsDto>
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GetResourceDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<ResourceDetailsDto> Handle(GetResourceDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _dbConnectionFactory.GetOpenConnection();

            const string sql = $"SELECT resource.id AS {nameof(ResourceDetailsDto.Id)}" +
                               $"     , resource.code AS {nameof(ResourceDetailsDto.Code)}" +
                               $"     , resource.name AS {nameof(ResourceDetailsDto.Name)}" +
                               $"     , resource.description AS {nameof(ResourceDetailsDto.Description)} " +
                               "   FROM resources.resources AS resource " +
                               "  WHERE resource.id = @ResourceId";
            var param = new
            {
                query.ResourceId
            };
            return await connection.QuerySingleAsync<ResourceDetailsDto>(sql, param);
        }
    }
}