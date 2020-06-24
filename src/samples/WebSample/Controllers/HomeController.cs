using Microsoft.AspNetCore.Mvc;

namespace WebSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}