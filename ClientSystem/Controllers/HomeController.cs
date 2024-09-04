using Microsoft.AspNetCore.Mvc;

namespace ClientSystem.Controllers
{
    public class HomeController : Controller
    {
        // Action for the Home page (index)
        public IActionResult Index()
        {
            return View();
        }

        // Action for the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Action for handling errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}