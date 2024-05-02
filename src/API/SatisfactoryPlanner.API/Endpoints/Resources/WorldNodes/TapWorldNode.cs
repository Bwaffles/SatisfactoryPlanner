using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.API.Endpoints;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.WorldNodes.TapWorldNode;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Modules.Resources.WorldNodes
{
    [ApiController]
    public class TapWorldNode(IResourcesModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ResourcesPermissions.TapWorldNode)]
        [WorldAuthorization]
        [HttpPost("api/worlds/{worldId}/nodes/{nodeId}/tap")]
        [SwaggerOperation(
            Summary = "Tap the world node with an extractor.",
            Tags = [Tags.WorldNodes])]
        [SwaggerResponse(200)]
        public async Task<IActionResult> HandleAsync([FromRoute] Guid worldId, [FromRoute] Guid nodeId,
            [FromBody] TapWorldNodeRequest request)
        {
            await module.ExecuteCommandAsync(new TapWorldNodeCommand(
                worldId,
                nodeId,
                request.ExtractorId
            ));

            return Ok();
        }
    }

    public class TapWorldNodeRequest
    {
        /// <summary>
        ///     The id of the extractor being used to extract the resources.
        /// </summary>
        [BindRequired]
        public Guid ExtractorId { get; set; }
    }
}