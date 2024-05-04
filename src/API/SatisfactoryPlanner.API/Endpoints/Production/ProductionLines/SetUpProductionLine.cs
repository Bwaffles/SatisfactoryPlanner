using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProductionLines.SetUpProductionLine;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Production.ProductionLines
{
    [ApiController]
    public class SetUpProductionLine(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.SetUpProductionLine)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/production-lines/set-up")]
        [SwaggerOperation(
            Summary = "Set up a new production line in the world.",
            Tags = [Tags.ProductionLines])]
        [SwaggerResponse(200, Type = typeof(SetUpProductionLineResponse))]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromBody] SetUpProductionLineRequest request)
        {
            var productionLineId = await module.ExecuteCommandAsync(new SetUpProductionLineCommand(worldId, request.Name));
            return Ok(new SetUpProductionLineResponse(productionLineId));
        }
    }

    public class SetUpProductionLineRequest
    {
        [BindRequired]
        public required string Name { get; set; }
    }

    public class SetUpProductionLineResponse(Guid productionLineId)
    {
        public Guid ProductionLineId { get; } = productionLineId;
    }
}
