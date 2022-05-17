using System.Diagnostics;
using FluiTec.AppFx.Localization.WebSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluiTec.AppFx.Localization.WebSample.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(new TestModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}