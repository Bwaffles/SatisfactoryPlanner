using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.RenameProductionLine;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    public class RenameProductionLine(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.RenameProductionLine)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/production-lines/{productionLineId}/rename")]
        [SwaggerOperation(
            Summary = "Rename a production line.",
            Tags = [Tags.ProductionLines])]
        [SwaggerResponse(200)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromRoute] Guid productionLineId, [FromBody] RenameProductionLineRequest request)
        {
            await module.ExecuteCommandAsync(new RenameProductionLineCommand(worldId, productionLineId, request.Name));
            return Ok();
        }
    }

    public class RenameProductionLineRequest
    {
        [BindRequired]
        public required string Name { get; set; }
    }
}
