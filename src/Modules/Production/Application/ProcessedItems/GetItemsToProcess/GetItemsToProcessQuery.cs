using SatisfactoryPlanner.Modules.Production.Application.Configuration.Queries;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Domain.ProcessedItems;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess
{
    public class GetItemsToProcessQuery() : IQuery<List<ItemToProcessDto>> { }

    internal class GetItemsToProcessQueryHandler : IQueryHandler<GetItemsToProcessQuery, List<ItemToProcessDto>>
    {
        public async Task<List<ItemToProcessDto>> Handle(GetItemsToProcessQuery request, CancellationToken cancellationToken)
        {
            return await GetItems();
        }

        private static Task<List<ItemToProcessDto>> GetItems()
        {
            var items = Item.All
                .ConvertAll(item => new ItemToProcessDto
                {
                    Category = new ItemCategoryDto
                    {
                        Id = item.Category.ToString(),
                        Name = item.Category.ToString()
                    },
                    Id = item.Id,
                    Name = item.Name
                });

            // TODO filter down to items that are ingredients in recipes

            return Task.Run(() => items);
        }
    }
}
