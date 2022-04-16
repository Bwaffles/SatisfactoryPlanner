using Dapper;
using SatisfactoryPlanner.BuildingBlocks.Application.Data;
using SatisfactoryPlanner.Modules.Resources.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Application.Resources.GetResourceDetails
{
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

            return await connection.QuerySingleAsync<ResourceDetailsDto>(
               "SELECT " +
               $"item.id AS {nameof(ResourceDetailsDto.Id)}, " +
               $"item.code AS {nameof(ResourceDetailsDto.Code)}, " +
               $"item.name AS {nameof(ResourceDetailsDto.Name)}, " +
               $"item.description AS {nameof(ResourceDetailsDto.Description)}, " +
               $"item.resource_form AS {nameof(ResourceDetailsDto.ResourceForm)}, " +
               $"item.stack_size AS {nameof(ResourceDetailsDto.StackSize)}, " +
               $"item.can_be_deleted AS {nameof(ResourceDetailsDto.CanBeDeleted)}, " +
               $"item.resource_sink_points AS {nameof(ResourceDetailsDto.ResourceSinkPoints)}, " +
               $"item.energy_value AS {nameof(ResourceDetailsDto.EnergyValue)}, " +
               $"item.radioactive_decay AS {nameof(ResourceDetailsDto.RadioactiveDecay)} " +
               "FROM resources.items AS item " +
               "WHERE item.type = 'Resource' " +
               "  AND item.id = @ResourceId",
               new
               {
                   query.ResourceId
               });
        }
    }
}
