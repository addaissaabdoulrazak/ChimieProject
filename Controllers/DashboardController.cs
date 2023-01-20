using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
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
            IEnumerable<EchangeLot> ListPublication = BLL_EchangeLot.GetAll();


            return View(ListPublication);
            
        }
    }
}
