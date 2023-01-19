using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Acceuil()
        {
            return View();
        }
    }
}
