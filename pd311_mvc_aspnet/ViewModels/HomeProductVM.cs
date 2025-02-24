using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.ViewModels
{
    public class HomeProductVM
    {
        public Product Product { get; set; } = new();
        public bool InCart { get; set; } = false;
    }
}
