using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLineDetails;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.GetProductionLines;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    [Route("api")]
    public class ProductionLinesController(IProductionModule module) : ControllerBase
    {
        /// <summary>
        ///     Get the production lines in the world.
        /// </summary>
        /// <response code="200">
        ///     Returns results as a list of <see cref="ProductionLineDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ProductionPermissions.GetProductionLines)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/[controller]")]
        [ProducesResponseType(typeof(List<ProductionLineDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductionLines([FromRoute] Guid worldId)
        {
            var productionLines = await module.ExecuteQueryAsync(new GetProductionLinesQuery(worldId));
            return Ok(productionLines);
        }

        /// <summary>
        ///     Get the details of the production line.
        /// </summary>
        /// <response code="200">
        ///     Returns results as a <see cref="ProductionLineDetailsDto" />.
        /// </response>
        /// <response code="404">
        ///     A production line with the given id does not exist.
        /// </response>
        [Authorize]
        [HasPermission(ProductionPermissions.GetProductionLineDetails)]
        [WorldAuthorization]
        [HttpGet("worlds/{worldId}/[controller]/{productionLineId}")]
        [ProducesResponseType(typeof(ProductionLineDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductionLineDetails([FromRoute] Guid worldId, [FromRoute] Guid productionLineId)
        {
            var productionLineDetails = await module.ExecuteQueryAsync(new GetProductionLineDetailsQuery(worldId, productionLineId));
            if (productionLineDetails is null)
                return NotFound();

            return Ok(productionLineDetails);
        }

        /// <summary>
        ///     Set up a new production line in the world.
        /// </summary>
        [Authorize]
        [HasPermission(ProductionPermissions.SetUpProductionLine)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/[controller]/set-up")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SetUpProductionLine([FromRoute] Guid worldId, [FromBody]SetUpProductionLineRequest request)
        {
            await module.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, request.Name));
            return NoContent();
        }
    }
}
