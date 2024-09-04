using Microsoft.AspNetCore.Mvc;

namespace ClientSystem.Controllers
{
    public class SPEController : Controller
    {
        // Action to simulate dataset access in the Secure Processing Environment
        public IActionResult Access(int id)
        {
            ViewBag.DatasetId = id;
            return View();
        }
    }
}