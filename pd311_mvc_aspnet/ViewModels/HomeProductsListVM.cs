using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.ViewModels
{
    public class HomeProductsListVM
    {
        public IEnumerable<HomeProductVM> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
        public string? Category { get; set; } = "";
        public int Page { get; set; } = 1;
        public int PagesCount { get; set; } = 1;
    }
}
