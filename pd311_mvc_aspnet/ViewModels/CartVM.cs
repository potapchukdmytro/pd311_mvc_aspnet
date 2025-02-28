namespace pd311_mvc_aspnet.ViewModels
{
    public class CartVM
    {
        public IEnumerable<ProductCartVM> Items { get; set; } = [];
        public int TotalPrice { get; set; } = 0;
    }
}
