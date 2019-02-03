using CurrencyConverter.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurrencyConverter.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Amount"] = "10";
            ViewData["Currency"] = "USD";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Convert()
        {
            ViewData["Amount"] = HttpContext.Request.Form["Amount"];
            ViewData["Currency"] = HttpContext.Request.Form["Currency"];
            ViewData["ConvertedAmount"] = HttpContext.Request.Form["Amount"] + "+1";

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
