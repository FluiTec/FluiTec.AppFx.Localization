using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebSample.Models;

namespace WebSample.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IStringLocalizer<TestModel> test)
        {
            Test = test;
        }

        public IStringLocalizer<TestModel> Test { get; }

        public IActionResult Index()
        {
            var strings = Test.GetAllStrings();
            return View();
        }
    }
}