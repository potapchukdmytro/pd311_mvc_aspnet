using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsEnumerable();

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.AsEnumerable();

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
        public IActionResult Create([FromForm] CreateProductVM viewModel)
        {
            string? imagePath = null;

            if(viewModel.File != null)
            {
                imagePath = SaveImage(viewModel.File);
            }

            viewModel.Product.Image = imagePath;
            viewModel.Product.Id = Guid.NewGuid().ToString();
            _context.Products.Add(viewModel.Product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private string? SaveImage(IFormFile file)
        {
            var types = file.ContentType.Split("/");
            if (types[0] != "image")
            {
                return null;
            }

            string fileName = $"{Guid.NewGuid()}.{types[1]}";
            string imagesPath = Path.Combine(_environment.WebRootPath, "images", "products");
            string filePath = Path.Combine(imagesPath, fileName);
            using (var stream = file.OpenReadStream())
            {
                using (var fileStream = System.IO.File.Create(filePath))
                {
                    stream.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
