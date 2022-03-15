using Application.ProductionLines.Queries.Export;
using Application.ProductionLines.Queries.GetItems;
using Application.ProductionLines.Queries.GetRecipes;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult Items()
        {
            var items = new GetItemsQuery()
                .Execute();

            return Json(items);
        }

        [HttpGet]
        public IActionResult Recipes(string itemId)
        {
            var recipes = new GetRecipesQuery()
                   .Execute(itemId);

            return Json(recipes);
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

        // TODO maybe this can be renamed to like get all inputs
        /// <summary>
        /// Get all the import items.
        /// </summary>
        /// <remarks>
        /// Called on a production line output when setting up where it exports the item to.
        /// Production Line Item Output => Exports To => Production Line Item Input
        /// </remarks>
        [HttpGet]
        public IActionResult ItemsToExportTo()
        {
            var data = new ExportItemQuery()
                .Execute();

            return Json(data);
        }
    }

    public class CreateProductionLineViewModel
    {
    }
}
