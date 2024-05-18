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
    public class GetItemRecipes(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.GetItemRecipes)]
        [HttpGet("api/processed-items/items-to-process/{itemId}/recipes")]
        [SwaggerOperation(
            Summary = "Get a list of recipes that use the item.",
            Tags = [Tags.ProcessedItems])]
        [SwaggerResponse(200, Type = typeof(GetItemRecipesResponse))]
        public async Task<IActionResult> HandleAsync([FromRoute]string itemId)
        {
            var itemRecipes = await module.ExecuteQueryAsync(new GetItemRecipesQuery(itemId));
            return Ok(new GetItemRecipesResponse(itemRecipes));
        }
    }

    public record GetItemRecipesResponse(ItemRecipesDto Data);
}
