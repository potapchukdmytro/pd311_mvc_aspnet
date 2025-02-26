using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.Models.Identity;
using pd311_mvc_aspnet.Repositories.Categories;
using pd311_mvc_aspnet.Repositories.Products;
using pd311_mvc_aspnet.Services.Cart;
using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, ICategoryRepository categoryRepository, ICartService cartService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartService = cartService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string? category = "", int page = 1)
        {
            int pageSize = 8;

            IQueryable<Product> products = !string.IsNullOrEmpty(category)
                ? _productRepository.GetByCategory(category).Include(p => p.Category)
                : _productRepository.Products;

            int pagesCount = (int)Math.Ceiling(products.Count() / (double)pageSize);
            page = page > pagesCount || page <= 0 ? 1 : page;
            products = products.Skip((page - 1) * pageSize).Take(pageSize);

            var cartItems = _cartService
                .GetItems()
                .Select(i => i.ProductId);

            var viewModel = new HomeProductsListVM
            {
                Products = products.Select(p => new HomeProductVM { Product = p, InCart = cartItems.Contains(p.Id) }),
                Categories = _categoryRepository.GetAll(),
                Category = category,
                Page = page,
                PagesCount = pagesCount
            };

            return View(viewModel);
        }

        [ActionName("Details")]
        public IActionResult ProductDetails(string? id)
        {
            if (id == null)
                return NotFound();

            return View("ProductDetails");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddToAdmin()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId = string.Empty;

            if (idClaim != null)
                userId = idClaim.Value;

            var user = _userManager.FindByIdAsync(userId).Result;

            if(user != null)
            {
                if (!_roleManager.RoleExistsAsync("admin").Result)
                {
                    _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "admin"
                    }).Wait();
                }
                _userManager.AddToRoleAsync(user, "admin").Wait();
            }

            return RedirectToAction("Index");
        }
    }
}
