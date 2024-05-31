using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Warehouses.Application.Contracts;
using SatisfactoryPlanner.Modules.Warehouses.Application.Items.GetItemStats;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Endpoints.Warehouses
{
    [ApiController]
    public class GetItemStats(IWarehousesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(WarehousesPermissions.GetItemStats)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/warehouse/item-stats")]
        [SwaggerOperation(
            Summary = "Get stats for the items in the warehouse.",
            Tags = [Tags.Warehouse])]
        [SwaggerResponse(200, Type = typeof(GetItemStatsResponse))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId)
        {
             var itemStats = await module.ExecuteQueryAsync(new GetItemStatsQuery(worldId));
            return Ok(new GetItemStatsResponse(itemStats));
        }
    }

    public record GetItemStatsResponse(ItemStatsResult Data);
}
