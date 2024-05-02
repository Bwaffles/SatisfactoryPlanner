using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLineDetails;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    public class GetProductionLineDetails(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.GetProductionLineDetails)]
        [WorldAuthorization]
        [HttpGet("api/worlds/{worldId}/production-lines/{productionLineId}")]
        [SwaggerOperation(
            Summary = "Get the details of the production line.",
            Tags = [Tags.ProductionLines])]
        [SwaggerResponse(200, Type = typeof(ProductionLineDetailsDto))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromRoute] Guid productionLineId)
        {
            var productionLineDetails = await module.ExecuteQueryAsync(new GetProductionLineDetailsQuery(worldId, productionLineId));
            if (productionLineDetails is null)
                return NotFound();

            return Ok(productionLineDetails);
        }
    }
}
