using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{
    public class PublicationController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.objEchangeLotList = BLL_Publication.GetAll();
            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            return View();
        }

        //GET
        public IActionResult Create()
        {

            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publication request)
        {
            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (ModelState.IsValid)
            {

                BLL_Publication.Insert(request);
                TempData["success"] = "Created successfully";
                return RedirectToAction("Index");
            }
            return View(request);
        }

        //GET
        public IActionResult Edit(long id)
        {
            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (id == 0)
            {
                return NotFound();
            }
            var EchangeLotFromDb = BLL_Publication.Get(id);

            if (EchangeLotFromDb == null)
            {
                return NotFound();
            }

            return View(EchangeLotFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publication obj)
        {
            ViewBag.objProduitList = BLL_Produit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (ModelState.IsValid)
            {
                BLL_Publication.Update(obj); 
                TempData["success"] = "Publication modifier avec succès";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete Publication
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var EchangeLotFromDb = BLL_Publication.Get(id);

            if (EchangeLotFromDb == null)
            {
                TempData["error"] = "Error while deleting";
                return Json(new { success = false, message = "Error while deleting" });
            }


            BLL_Publication.Delete(id);
            TempData["success"] = "Publication suprimer avec succès";
            return Json(new { success = true, message = "Supprimer avec succès" });
        }

      
    }
}
