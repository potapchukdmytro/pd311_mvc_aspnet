using Microsoft.AspNetCore.Mvc;
using pd311_mvc_aspnet.Repositories.Products;
using pd311_mvc_aspnet.Services.Cart;
using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartController(ICartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetItems();

            var products = _productRepository
                .GetAll()
                .Where(p => cartItems.Select(i => i.ProductId).Contains(p.Id))
                .ToList();

            var items = cartItems.Select(i => new ProductCartVM
            {
                Quantity = i.Quaintity,
                Product = products.First(p => p.Id == i.ProductId)
            });

            var viewModel = new CartVM
            {
                Items = items,
                TotalPrice = items.Select(i => i.Price).Aggregate(0, (sum, i) => sum + i)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.AddToCart(viewModel);
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] CartItemVM viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.ProductId))
                return BadRequest();

            _cartService.RemoveFromCart(viewModel);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddQuaintity([FromBody] CartItemVM viewModel)
        {
            var product = await _productRepository.FindByIdAsync(viewModel.ProductId);
            if (product == null)
                return BadRequest();

            if (viewModel.Quaintity < product.Amount)
            {
                _cartService.IncreaseQuaintity(viewModel.ProductId);
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult MinusQuaintity([FromBody] CartItemVM viewModel)
        {
            if (viewModel.Quaintity > 1)
            {
                _cartService.DecreaseQuaintity(viewModel.ProductId);
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
    }
}
