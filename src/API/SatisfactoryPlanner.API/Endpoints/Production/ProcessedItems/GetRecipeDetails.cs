using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SatisfactoryPlanner.API.Configuration.Authorization.Permissions;
using SatisfactoryPlanner.API.Modules.Production;
using SatisfactoryPlanner.Modules.Production.Application.Contracts;
using SatisfactoryPlanner.Modules.Production.Application.ProcessedItems.GetRecipeDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace SatisfactoryPlanner.API.Endpoints.Production.ProcessedItems
{
    [ApiController]
    public class GetRecipeDetails(IProductionModule module) : ControllerBase
    {
        [Authorize]
        [HasPermission(ProductionPermissions.GetRecipeDetails)]
        [HttpGet("api/processed-items/recipes/{recipeId}")]
        [SwaggerOperation(
            Summary = "Get the details of a recipe.",
            Tags = [Tags.ProcessedItems])]
        [SwaggerResponse(200, Type = typeof(GetRecipeDetailsResponse))]
        public async Task<IActionResult> HandleAsync([FromRoute, SwaggerParameter("The id of the recipe.", Required = true)] string recipeId)
        {
            var recipeDetails = await module.ExecuteQueryAsync(new GetRecipeDetailsQuery(recipeId));
            return Ok(new GetRecipeDetailsResponse(recipeDetails));
        }
    }

    public record GetRecipeDetailsResponse(RecipeDetailsDto Data);
}
