using ChimieProject.Models.BLL;
using ChimieProject.Models.Entities;
using ChimieProject.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChimieProject.Controllers
{
    public class LaboratoireController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        //My controller
        public LaboratoireController(IConfiguration configuration, IJwtAuthenticationService jwtAuthenticationService)
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
            IEnumerable<Laboratoire> _listofLab = BLL_Laboratoire.GetAll();
            return View(_listofLab);
        }


        [HttpGet]
        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inscription(LaboratoireDto request)
        {
            Laboratoire IsObjectEmailExist = BLL_Laboratoire.GetElementByEmail(request.Email);

            if (IsObjectEmailExist != null)
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                Laboratoire _lab = new Laboratoire();

                string EncryptedPassword = _jwtAuthenticationService.Encrypt(request.Password);

                _lab.Nom = request.Nom;
                _lab.Email = request.Email;
                _lab.Acronyme = request.Acronyme;
                _lab.Password = EncryptedPassword;
                _lab.Statut = 0;
                _lab.Responsable = request.Responsable;
                _lab.Tel = request.Tel;
                _lab.Etablissement = request.Etablissement;
                _lab.Role = "user";

                BLL_Laboratoire.Insert(_lab);
                TempData["success"] = "Registered successfully";
                return RedirectToAction("Inscription");
            }

            return View(request);
          
        }



        //----------[Create a Login Action After <ith Token]-------------------//

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LaboratoireDto request)
        {

            if (string.IsNullOrEmpty(request.Nom) || string.IsNullOrEmpty(request.Password))
            {
                return RedirectToAction("Error", "Home");
            }

            // _ = Unauthorized();

            Laboratoire objLab = _jwtAuthenticationService.Authenticate(request.Nom);



            if (objLab != null)
            {
                //Decrypt PasswordHash
                string DecryptedPassword = _jwtAuthenticationService.Decrypt(objLab.Password);

                if (DecryptedPassword == request.Password && objLab.Nom == request.Nom && objLab.Role=="Admin")// added email
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
            return Json(new { data = BLL_Laboratoire.GetAll() });
        }


        //Decline Compte
        [HttpDelete]
        public IActionResult Decline(int id)
        {
            var LabFromDb = BLL_Laboratoire.Get(id);

            if (LabFromDb == null)
            {
                return Json(new { success = false, message = "Error while declining" });
            }


            BLL_Laboratoire.Delete(id);

            return Json(new { success = true, message = "Decline successful" });
        }


        //Accept 
        [HttpPost]
        public IActionResult Accept(int id)
        {
            var LabFromDb = BLL_Laboratoire.Get(id);
            if (LabFromDb == null)
            {
                return Json(new { success = false, message = "Error" });
            }

            LabFromDb.Statut = 1;

            BLL_Laboratoire.Update(LabFromDb);

            return Json(new { success = true, message = "accepted" });
        }
        #endregion
    }
}
