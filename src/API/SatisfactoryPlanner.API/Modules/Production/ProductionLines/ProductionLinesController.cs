using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    [Route("api")]
    public class ProductionLinesController(IProductionModule module) : ControllerBase
    {
        /// <summary>
        ///     Get the production lines set up in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns results as a list of <see cref="ProductionLineDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ProductionPermissions.GetProductionLines)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/[controller]")]
        [ProducesResponseType(typeof(List<ProductionLineDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResources([FromRoute] Guid worldId)
        {
            var productionLines = await module.ExecuteQueryAsync(new GetProductionLinesQuery(worldId));
            return Ok(productionLines);
        }
    }
}
