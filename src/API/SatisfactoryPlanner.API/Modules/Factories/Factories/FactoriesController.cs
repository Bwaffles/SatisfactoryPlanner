using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.Modules.Factories.Application.Contracts;
using SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildFactory;
using SatisfactoryPlanner.Modules.Factories.Application.Factories.BuildSubFactory;
using SatisfactoryPlanner.Modules.Factories.Application.Factories.GetFactories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.API.Modules.Factories.Factories
{
    [ApiController]
    [Route("api/factories/factories")]
    public class FactoriesController : ControllerBase
    {
        private readonly IFactoriesModule factoriesModule;

        public FactoriesController(IFactoriesModule factoriesModule)
        {
            this.factoriesModule = factoriesModule;
        }

        /// <summary>
        ///     Get a list of factories.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<FactoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFactories()
        {
            var factories = await factoriesModule.ExecuteQueryAsync(new GetFactoriesQuery());
            return Ok(factories);
        }

        /// <summary>
        ///     Build a new factory.
        /// </summary>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BuildFactory([FromBody] BuildFactoryRequest request)
        {
            await factoriesModule.ExecuteCommandAsync(new BuildFactoryCommand
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
        public async Task<IActionResult> BuildSubFactory([FromRoute] Guid factoryId, [FromBody] BuildSubFactoryRequest request)
        {
            await factoriesModule.ExecuteCommandAsync(new BuildSubFactoryCommand(
                factoryId,
                request.Name
            ));

            return Ok();
        }
    }
}
