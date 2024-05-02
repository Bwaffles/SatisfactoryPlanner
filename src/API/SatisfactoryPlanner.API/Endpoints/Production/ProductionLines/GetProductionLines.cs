using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    public class GetProductionLines(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.GetProductionLines)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/production-lines")]
        [SwaggerOperation(
            Summary = "Get the production lines in the world.",
            Tags = [Tags.ProductionLines])]
        [SwaggerResponse(200, Type = typeof(List<ProductionLineDto>))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId)
        {
            var productionLines = await module.ExecuteQueryAsync(new GetProductionLinesQuery(worldId));
            return Ok(productionLines);
        }
    }
}
