using ChimieProject.Models;
using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using session = ChimieProject.Models.session;

namespace ChimieProject.Controllers
{
    public class DashboardController : Controller
    {
      
        //Data Binding Property
        //[BindProperty]
        // public Publication? Publication { get; set; }
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult Acceuil()
        {
            

            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.ListPublication = BLL_Publication.GetAll();

            ViewBag.currentUser = HttpContext.Session.Get<Structure>("currentUser");



            return View();
        }


        [HttpGet]
        public IActionResult Upsert(long? id)
        {

            //Get Current user
            ViewBag.currentUser = HttpContext.Session.Get<Structure>("currentUser");


            ViewBag.objProduitList = BLL_Produit.GetAll();

            
            //Book = new Book();
            Publication objPublication = new Publication();
            if (id == null)
            {
                //create
                return View(objPublication);
            }
            //update
            //Book = _db.Books.FirstOrDefault(u => u.Id == id);
            objPublication = BLL_Publication.Get((long)id);
            if (objPublication == null)
            {
                return NotFound();
            }
            return View(objPublication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publication publication)
        {
            Structure currentUser = HttpContext.Session.Get<Structure>("currentUser");
            ViewBag.currentUser = currentUser;
            if (ModelState.IsValid)
            {
                if (publication.Id == 0)
                {
                    //create
                    BLL_Publication.Insert(publication);
                }
                else
                {
                    BLL_Publication.Update(publication);
                }
                // _db.SaveChanges();
                TempData["success"] = "success";
                return RedirectToAction("Acceuil");
            }
            return View(publication);
        }
    }
}
