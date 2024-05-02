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
