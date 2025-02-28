using Microsoft.EntityFrameworkCore.Diagnostics;
using pd311_mvc_aspnet.Services.Session;
using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(CartItemVM viewModel)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;
            var session = context.Session;

            var items = GetItems().ToList();
            items.Add(viewModel);
            session.Set<IEnumerable<CartItemVM>>(Settings.SessionCartKey, items);
        }

        public IEnumerable<CartItemVM> GetItems()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return new List<CartItemVM>();

            var session = context.Session;

            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);
            items ??= new List<CartItemVM>();

            return items;
        }

        public static int GetCount(HttpContext? context)
        {
            if (context == null)
                return 0;

            var session = context.Session;
            var items = session.Get<IEnumerable<CartItemVM>>(Settings.SessionCartKey);

            return items == null ? 0 : items.Count();
        }

        public void RemoveFromCart(CartItemVM viewModel)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;
            var session = context.Session;

            var items = GetItems();

            items = items.Where(i => i.ProductId != viewModel.ProductId);
            session.Set(Settings.SessionCartKey, items);
        }

        public IEnumerable<string> GetItemsAsString()
        {
            return GetItems().Select(i => i.ProductId);
        }

        public void IncreaseQuaintity(string productId)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quaintity++;
                var context = _httpContextAccessor.HttpContext;
                if (context == null)
                    return;
                var session = context.Session;

                session.Set(Settings.SessionCartKey, items);
            }
        }

        public void DecreaseQuaintity(string productId)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quaintity--;
                var context = _httpContextAccessor.HttpContext;
                if (context == null)
                    return;
                var session = context.Session;

                session.Set(Settings.SessionCartKey, items);
            }
        }
    }
}
