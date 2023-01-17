using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{
    public class EchangeLotController : Controller
    {

        public IActionResult Index()
        {
           IEnumerable<EchangeLot> objEchangeLotList = BLL_EchangeLot.GetAll();

            return View(objEchangeLotList);
        }

        //GET
        public IActionResult Create()
        {

            ViewBag.objProduitList = BLLProduit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EchangeLot request)
        {
            ViewBag.objProduitList = BLLProduit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (ModelState.IsValid)
            {

                BLL_EchangeLot.Insert(request);
                TempData["success"] = "Created successfully";
                return RedirectToAction("Index");
            }
            return View(request);
        }

        //GET
        public IActionResult Edit(long id)
        {
            ViewBag.objProduitList = BLLProduit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (id == 0)
            {
                return NotFound();
            }
            var EchangeLotFromDb = BLL_EchangeLot.Get(id);

            if (EchangeLotFromDb == null)
            {
                return NotFound();
            }

            return View(EchangeLotFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EchangeLot obj)
        {
            ViewBag.objProduitList = BLLProduit.GetAll();

            ViewBag.objLaboratoireList = BLL_Structure.GetAll();

            if (ModelState.IsValid)
            {
                BLL_EchangeLot.Update(obj); 
                TempData["success"] = "EchangeLot updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete Compte
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var EchangeLotFromDb = BLL_EchangeLot.Get(id);

            if (EchangeLotFromDb == null)
            {
                TempData["error"] = "Error while deleting";
                return Json(new { success = false, message = "Error while deleting" });
            }


            BLL_EchangeLot.Delete(id);
            TempData["success"] = "EchangeLot deleted successfully";
            return Json(new { success = true, message = "deleted successful" });
        }

      
    }
}
