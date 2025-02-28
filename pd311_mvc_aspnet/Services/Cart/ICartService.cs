using pd311_mvc_aspnet.ViewModels;

namespace pd311_mvc_aspnet.Services.Cart
{
    public interface ICartService
    {
        void AddToCart(CartItemVM viewModel);
        void RemoveFromCart(CartItemVM viewModel);
        IEnumerable<CartItemVM> GetItems();
        IEnumerable<string> GetItemsAsString();
        void IncreaseQuaintity(string productId);
        void DecreaseQuaintity(string productId);
    }
}
