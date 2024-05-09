using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Modules.Production;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetItemsToProcess;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Endpoints.Production.ProcessedItems
{
    [ApiController]
    public class GetItemsToProcess(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.GetItemsToProcess)]
        [HttpGet("api/processed-items/items-to-process")]
        [SwaggerOperation(
            Summary = "Get a list of items that can be processed.",
            Tags = [Tags.ProcessedItems])]
        [SwaggerResponse(200, Type = typeof(GetItemsToProcessReponse))]
        public async Task<IActionResult> HandleAsync()
        {
            var itemsToProcess = await module.ExecuteQueryAsync(new GetItemsToProcessQuery());
            return Ok(new GetItemsToProcessReponse(itemsToProcess));
        }
    }

    public record GetItemsToProcessReponse(List<ItemToProcessDto> Items);
}
