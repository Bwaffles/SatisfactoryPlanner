using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Production.Application.Factories.BuildFactory;
using SatisfactoryPlanner.Modules.Production.Application.Factories.BuildSubFactory;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.Factories.GetFactories;

namespace SatisfactoryPlanner.API.Modules.Production.Factories
{
    [ApiController]
    [Route("api/production/[controller]")]
    public class FactoriesController(IFactoriesModule module) : ControllerBase
    {
        /// <summary>
        ///     Get a list of factories.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<FactoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFactories()
        {
            var factories = await module.ExecuteQueryAsync(new GetFactoriesQuery());
            return Ok(factories);
        }

        /// <summary>
        ///     Build a new factory.
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BuildFactory([FromBody] BuildFactoryRequest request)
        {
            await module.ExecuteCommandAsync(new BuildFactoryCommand
            (
                request.Name
            ));

            return Ok();
        }

        /// <summary>
        ///     Build a factory under another factory.
        /// </summary>
        [HttpPost("{factoryId}/sub-factories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BuildSubFactory([FromRoute] Guid factoryId,
            [FromBody] BuildSubFactoryRequest request)
        {
            await module.ExecuteCommandAsync(new BuildSubFactoryCommand(
                factoryId,
                request.Name
            ));

            return Ok();
        }
    }
}