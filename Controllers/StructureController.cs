﻿using ChimieProject.Models;
using ChimieProject.Models.BLL;
using ChimieProject.Models.DAL;
using ChimieProject.Models.Entities;
using ChimieProject.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{
    public class StructureController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        //Use DataBinding
        [BindProperty]
        public Structure Structure { get; set; }

        //My controller
        public StructureController(IConfiguration configuration, IJwtAuthenticationService jwtAuthenticationService)
        {
            _configuration = configuration;

            _jwtAuthenticationService = jwtAuthenticationService;
        }

        //Only if Role == Administrateur (After added this) 
        [Authorize]
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return (RedirectToAction("Index", "Home"));
            }
            if (!_jwtAuthenticationService.IsTokenValid(_configuration.GetSection("Jwt:Key").Value.ToString(), _configuration.GetSection("Jwt:Issuer").Value.ToString(), token))
            {
                return (RedirectToAction("Index", "Home"));
            }
            IEnumerable<Structure> _listofLab = BLL_Structure.GetAll();
            return View(_listofLab);
        }

//-------------------------------------------------[Register]---------------------------------------------------------

        [HttpGet]
        public IActionResult Inscription()
        {
            StructureDto dt =new StructureDto();
            return View(dt);
           
            return View();
        }


        [HttpPost]
        public IActionResult Inscription(StructureDto request)
        {
           
                Structure _Struc = new Structure();

                string EncryptedPassword = _jwtAuthenticationService.Encrypt(request.Password);

                _Struc.Nom = request.Nom;
                _Struc.Type=request.Type;
                _Struc.Email = request.Email;
                _Struc.Acronyme = request.Acronyme;
                _Struc.Password = EncryptedPassword;
                _Struc.Statut = 0;
                _Struc.Responsable = request.Responsable;
                _Struc.Tel = request.Tel;
                _Struc.Etablissement = request.Etablissement;
                _Struc.Role = Roles.USER.ToString();

                BLL_Structure.Insert(_Struc);
                TempData["success"] = "Registered successfully";
                return RedirectToAction("Inscription");


        }
//-------------------------------------------------[End Register]---------------------------------------------------------


      //----------[Create a Login Action After <ith Token]-------------------//

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(StructureDto request)
        {

            if (string.IsNullOrEmpty(request.Nom) || string.IsNullOrEmpty(request.Password))
            {
                return RedirectToAction("Error", "Home");
            }

            // _ = Unauthorized();

            Structure objLab = _jwtAuthenticationService.Authenticate(request.Nom);



            if (objLab != null)
            {
                //Decrypt PasswordHash
                string DecryptedPassword = _jwtAuthenticationService.Decrypt(objLab.Password);

                if (DecryptedPassword == request.Password && objLab.Nom == request.Nom && objLab.Role==Roles.ADMISTRATOR.ToString())
                    // added email
                {
                    //string Token = _jwtAuthenticationService.CreateToken(user);
                    string Token = _jwtAuthenticationService.BuildToken(_configuration.GetSection("Jwt:Key").Value.ToString(), _configuration.GetSection("Jwt:Issuer").Value.ToString(), objLab);

                    if (Token != null)
                    {
                        HttpContext.Session.SetString("Token", Token);

                    }
                    return RedirectToAction("Acceuil");

                }

            }
            return RedirectToAction("Error", "Home");

        }

        [Authorize]
        [Route("Acceuil")]
        [HttpGet]
        public IActionResult Acceuil()
        {
            string token = HttpContext.Session.GetString("Token");
            if (token == null)
            {
                return (RedirectToAction("Index", "Home"));
            }
            if (!_jwtAuthenticationService.IsTokenValid(_configuration.GetSection("Jwt:Key").Value.ToString(), _configuration.GetSection("Jwt:Issuer").Value.ToString(), token))
            {
                return (RedirectToAction("Index", "Home"));
            }


            return View();
        }




        //----------------------------------[Setting Management User]--------------------------------------//

        [Authorize]
        [Route("SettingManagement")]
        [HttpGet]
        public IActionResult SettingManagement()
        {
          
            return View();
        }


        [HttpGet]
        public IActionResult Upsert(long? id)
        {
            
            if (id == null)
            {
                //create
                return View();
            }
            //update
            Structure objStructure= BLL_Structure.Get((long)id);
            if (objStructure == null)
            {
                return NotFound();
            }
            return View(objStructure);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Structure.Id == 0)
                {
                    //create
                    BLL_Structure.Insert(Structure);
                }
                else
                {
                    BLL_Structure.Update(Structure);
                }
                
                return RedirectToAction("SettingManagement");
            }
            return View(Structure);
        }

        //-------------------------------------[End]--------------------------------------//



 //--------------------------------------------------------------[o Check Permission Within Action Method]------------------------------------------------------------------//




        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Inscription");
        }


        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = BLL_Structure.GetAll() });
        }


        //Decline Compte
        [HttpDelete]
        public IActionResult Decline(int id)
        {
            var LabFromDb = BLL_Structure.Get(id);

            if (LabFromDb == null)
            {
                return Json(new { success = false, message = "Error while declining" });
            }


            BLL_Structure.Delete(id);

            return Json(new { success = true, message = "Decline successful" });
        }


        //Accept 
        [HttpPost]
        public IActionResult Accept(int id)
        {
            var LabFromDb = BLL_Structure.Get(id);
            if (LabFromDb == null)
            {
                return Json(new { success = false, message = "Error" });
            }

            LabFromDb.Statut = 1;

            BLL_Structure.Update(LabFromDb);

            return Json(new { success = true, message = "accepted" });
        }
        #endregion
    }
}
