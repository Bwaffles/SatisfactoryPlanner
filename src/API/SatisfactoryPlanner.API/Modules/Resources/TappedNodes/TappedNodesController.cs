using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.TappedNodes.IncreaseExtractionRate;
using System;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.TappedNodes
{
    [ApiController]
    [Route("api/resources/[controller]")]
    public class TappedNodesController : Controller
    {
        private readonly IResourcesModule _module;

        public TappedNodesController(IResourcesModule module)
        {
            _module = module;
        }

        /// <summary>
        ///     Increase the extraction rate of resources from a tapped node.
        /// </summary>
        [Authorize]
        [HasPermission(ResourcesPermissions.IncreaseNodeExtractionRate)]
        [WorldAuthorization(typeof(IncreaseExtractionRateRequest))]
        [HttpPost("{tappedNodeId}/increase-extraction-rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseExtractionRate([FromRoute] Guid tappedNodeId,
            [FromBody] IncreaseExtractionRateRequest request)
        {
            await _module.ExecuteCommandAsync(new IncreaseExtractionRateCommand(
                request.WorldId,
                tappedNodeId,
                request.NewExtractionRate
            ));

            return Ok();
        }
    }
}