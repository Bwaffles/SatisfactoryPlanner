using Application.Pods.Queries.Create;
using Microsoft.AspNetCore.Mvc;
using Services.SFGame;
using System.Linq;

namespace UI.Controllers
{
    public class ProductionLineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateProductionLineViewModel
            {

            };
            return View(model);
        }

        public IActionResult Items() // TODO should be in like an ItemController
        {
            var query = new CreatePodQuery();
            var data = query.Execute();
            return Json(data.Items);
        }

        [HttpGet]
        public IActionResult Recipes(string itemCode)
        {
            var sfGameService = new SFGameService();
            var gameData = sfGameService.GetGameData();
            return Json(
                gameData
                .Recipes
                .Where(recipe => recipe.Products.Any(product => product.Item?.ClassName == itemCode))
                .Select(recipe => new
                {
                    Id = recipe.FullName,
                    Name = recipe.DisplayName,
                    Ingredients = recipe.Ingredients.Select(ingredient => new
                    {
                        Id = ingredient.Item.ClassName,
                        Name = ingredient.Item.DisplayName,
                        Amount = ingredient.Amount,
                        ItemsPerMinute = (60 / recipe.ManufacturingDuration) * ingredient.Amount
                    }),
                    Products = recipe.Products.Select(product => new
                    {
                        Id = product.Item.ClassName,
                        Name = product.Item.DisplayName,
                        Amount = product.Amount,
                        ItemsPerMinute = (60 / recipe.ManufacturingDuration) * product.Amount
                    })
                })
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProductionLineViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //watchMovieCommand.Execute(model);
            //return Json(new { redirectToUrl = Url.Action("details", "movies", new { id = model.MovieId }) });
            return RedirectToAction("Index");
        }
    }

    public class CreateProductionLineViewModel
    {
    }
}
