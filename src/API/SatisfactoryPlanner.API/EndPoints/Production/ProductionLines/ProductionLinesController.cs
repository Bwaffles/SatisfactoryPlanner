using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.GetWorldNodeDetails;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{

    [ApiController]
    [Route("api")]
    public class ProductionLinesController(IProductionModule module) : ControllerBase
    {
        /// <summary>
        ///     Set up a new production line in the world.
        /// </summary>
        [Authorize]
        [HasPermission(ProductionPermissions.SetUpProductionLine)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/[controller]/set-up")]
        [ProducesResponseType(typeof(SetUpProductionLineResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> SetUpProductionLine([FromRoute] Guid worldId, [FromBody] SetUpProductionLineRequest request)
        {
            var productionLineId = await module.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, request.Name));
            return Ok(new SetUpProductionLineResponse(productionLineId));
        }

        /// <summary>
        ///     Rename a production line.
        /// </summary>
        [Authorize]
        [HasPermission(ProductionPermissions.RenameProductionLine)]
        [WorldAuthorization]
        [HttpPost("worlds/{worldId}/[controller]/{productionLineId}/rename")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Rename([FromRoute] Guid worldId, [FromRoute] Guid productionLineId, [FromBody] RenameProductionLineRequest request)
        {
            await module.ExecuteCommandAsync(new RenameProductionLineCommand(worldId, productionLineId, request.Name));
            return Ok();
        }
    }
}
