using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Configuration.Authorization.Worlds;
using SatisfactoryPlanner.Modules.Resources.Application.Contracts;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodeDetails;
using SatisfactoryPlanner.Modules.Resources.Application.Nodes.GetNodes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Resources.Nodes
{
    [ApiController]
    [Route("api/resources/[controller]")]
    public class NodesController : Controller
    {
        private readonly IResourcesModule _module;

        public NodesController(IResourcesModule module)
        {
            _module = module;
        }

        /// <summary>
        ///     Get nodes.
        /// </summary>
        /// <response code="200">
        ///     Returns the nodes as a list of <see cref="NodeDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetNodes)]
        [WorldAuthorization]
        [HttpGet("")]
        [ProducesResponseType(typeof(List<NodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNodes([FromQuery] GetNodesRequest request)
        {
            var nodes = await _module.ExecuteQueryAsync(new GetNodesQuery(request.WorldId, request.ResourceId));
            return Ok(nodes);
        }

        /// <summary>
        ///     Get the details of a node.
        /// </summary>
        /// <response code="200">
        ///     Returns the node as a <see cref="NodeDetailsDto" />.
        /// </response>
        [Authorize]
        [HasPermission(ResourcesPermissions.GetNodeDetails)]
        [WorldAuthorization]
        [HttpGet("{nodeId}")]
        [ProducesResponseType(typeof(NodeDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNodeDetails([FromRoute] Guid nodeId, [FromQuery] GetNodeDetailsRequest request)
        {
            var nodeDetails = await _module.ExecuteQueryAsync(new GetNodeDetailsQuery(nodeId, request.WorldId));
            return Ok(nodeDetails);
        }
    }
}
