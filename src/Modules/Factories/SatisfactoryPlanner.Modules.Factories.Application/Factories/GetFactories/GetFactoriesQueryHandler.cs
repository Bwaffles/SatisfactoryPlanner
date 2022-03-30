using SatisfactoryPlanner.Modules.Factories.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Factories.Application.Factories.GetFactories
{
    internal class GetFactoriesQueryHandler : IQueryHandler<GetFactoriesQuery, List<FactoryDto>>
    {
        public async Task<List<FactoryDto>> Handle(GetFactoriesQuery request, CancellationToken cancellationToken)
        {
            return await GetFakeData();
        }

        private async Task<List<FactoryDto>> GetFakeData()
        {
            return new List<FactoryDto>
            {
                new FactoryDto
                {
                    Name = "Circuit Board West"
                },
                new FactoryDto
                {
                    Name = "Main Copper"
                }
            };
        }
    }
}
