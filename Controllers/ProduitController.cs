
﻿using ChimieProject.Models.BLL;
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
}
