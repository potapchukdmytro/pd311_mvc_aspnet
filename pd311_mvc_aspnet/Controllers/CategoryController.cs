using Microsoft.AspNetCore.Mvc;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Controllers
{
    public class CategoryController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var categories = context.Categories.AsEnumerable();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid().ToString();

                context.Categories.Add(model);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public IActionResult Update(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category model)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Update(model);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public IActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category model)
        {
            context.Categories.Remove(model);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
