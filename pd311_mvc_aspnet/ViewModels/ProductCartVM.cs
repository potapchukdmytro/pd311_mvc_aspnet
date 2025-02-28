using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.ViewModels
{
    public class ProductCartVM
    {
        public Product Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public int Price { get => (int)Product.Price * Quantity; }
    }
}
