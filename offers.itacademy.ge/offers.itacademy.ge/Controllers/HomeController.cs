using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using offers.itacademy.ge.Web.Controllers;
using offers.itacademy.ge.Web.Models;

namespace offers.itacademy.ge.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Optional: get user type and redirect
                return RedirectToAction("Dashboard", "Buyer");
            }

            return View(); // show welcome view for guestreturn View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
