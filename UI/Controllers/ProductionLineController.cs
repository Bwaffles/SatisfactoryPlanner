using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
