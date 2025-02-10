using Microsoft.AspNetCore.Mvc;

namespace pd311_mvc_aspnet.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
