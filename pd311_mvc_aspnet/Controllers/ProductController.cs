using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.Repositories.Categories;
using pd311_mvc_aspnet.Repositories.Products;
using pd311_mvc_aspnet.Services.Image;
using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Controllers
{
    public class ProductController
        (IProductRepository productRepository, ICategoryRepository categoryRepository, IImageService imageService) 
        : Controller
    {
        public IActionResult Index()
        {
            var products = productRepository.Products;
            return View(products);
        }

        public IActionResult Create()
        {
            var categories = categoryRepository.Categories;

            var viewModel = new CreateProductVM
            {
                Product = new Product(),
                Categories = categories.Select(c =>
                new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProductVM viewModel)
        {
            string? imagePath = null;

            if(viewModel.File != null)
            {
                imagePath = await imageService.SaveImageAsync(viewModel.File, Settings.ProductsImagesPath);
            }

            viewModel.Product.Image = imagePath;
            viewModel.Product.Id = Guid.NewGuid().ToString();
            await productRepository.CreateAsync(viewModel.Product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await productRepository.FindByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(Product model)
        {
            string? imageName = model.Image;

            if (model.Id == null)
                return NotFound();

            var res = await productRepository.DeleteAsync(model.Id);

            if (res && imageName != null)
            {
                imageService.DeleteFile(Path.Combine(Settings.ProductsImagesPath, imageName));
            }

            return RedirectToAction("Index");
        }
    }
}
