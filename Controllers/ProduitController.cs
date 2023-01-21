using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{

    public class ProduitController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Produits = BLL_Produit.GetAll();
            return View();
        }
        public IActionResult Add(Produit produit)
        {
            return Json(BLL_Produit.Insert(produit));
        }

        public IActionResult Delete(int id)
        {

            return Json(BLL_Produit.Delete(id));
        }
        public IActionResult Getbyid(int id)
        {
            return Json(BLL_Produit.Get(id));
        }
        public IActionResult Update(Produit produit)
        {
            return Json(BLL_Produit.Update(produit));
        }
        public IActionResult Rechercher(string Nom, string Formule, string Cas, string EtatPhysique)
        {
            return Json(BLL_Produit.rechercher(Nom, Formule, Cas, EtatPhysique));
        }
    }
    //public class ProduitController : Controller
    //{
    //    public IActionResult Index()
    //    {
    //        IEnumerable<Category> objCategoryList = _db.Categories;
    //        return View(objCategoryList);
    //    }

    //    //GET
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    //POST
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult Create(Category obj)
    //    {
    //        if (obj.Name == obj.DisplayOrder.ToString())
    //        {
    //            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
    //        }
    //        if (ModelState.IsValid)
    //        {
    //            _db.Categories.Add(obj);
    //            _db.SaveChanges();
    //            TempData["success"] = "Category created successfully";
    //            return RedirectToAction("Index");
    //        }
    //        return View(obj);
    //    }

    //    //GET
    //    public IActionResult Edit(int? id)
    //    {
    //        if (id == null || id == 0)
    //        {
    //            return NotFound();
    //        }
    //        var categoryFromDb = _db.Categories.Find(id);
    //        //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
    //        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

    //        if (categoryFromDb == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(categoryFromDb);
    //    }

    //    //POST
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult Edit(Category obj)
    //    {
    //        if (obj.Name == obj.DisplayOrder.ToString())
    //        {
    //            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
    //        }
    //        if (ModelState.IsValid)
    //        {
    //            _db.Categories.Update(obj);
    //            _db.SaveChanges();
    //            TempData["success"] = "Category updated successfully";
    //            return RedirectToAction("Index");
    //        }
    //        return View(obj);
    //    }

    //    public IActionResult Delete(int? id)
    //    {
    //        if (id == null || id == 0)
    //        {
    //            return NotFound();
    //        }
    //        var categoryFromDb = _db.Categories.Find(id);
    //        //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
    //        //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

    //        if (categoryFromDb == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(categoryFromDb);
    //    }

    //    //POST
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public IActionResult DeletePOST(int? id)
    //    {
    //        var obj = _db.Categories.Find(id);
    //        if (obj == null)
    //        {
    //            return NotFound();
    //        }

    //        _db.Categories.Remove(obj);
    //        _db.SaveChanges();
    //        TempData["success"] = "Category deleted successfully";
    //        return RedirectToAction("Index");

    //    }
    //}
}
