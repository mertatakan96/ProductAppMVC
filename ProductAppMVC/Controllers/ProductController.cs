using Microsoft.AspNetCore.Mvc;

namespace ProductAppMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
