//using AuthenticationProject.Models.BLL;
//using ChimieProject.Models.BLL;
//using ChimieProject.Models.Entities;
//using ChimieProject.Models.Service;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ChimieProject.Controllers
//{
//    public class AuthenticationController : Controller
//    {

//        private readonly IConfiguration _configuration;

//        private readonly IJwtAuthenticationService _jwtAuthenticationService;

//        //My Cntroller
//        public AuthenticationController(IConfiguration configuration, IJwtAuthenticationService jwtAuthenticationService)
//        {
//            _configuration = configuration;

//            _jwtAuthenticationService = jwtAuthenticationService;

//        }


//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }



//        [HttpPost]
//        public IActionResult Register(UserDto request)
//        {
//            IEnumerable<User> objUserList = BLL_User.GetAll();

//            foreach ( var value in objUserList)
//            {
//                if (value.Email == request.Email)
//                {
//                    ModelState.AddModelError("name", "this email already exists");
//                }
//            }

//            if (ModelState.IsValid)
//            {
//                User user = new User();

//                string EncryptedPassword = _jwtAuthenticationService.Encrypt(request.Password);

//                string EncryptedConfirmPassword = _jwtAuthenticationService.Encrypt(request.ConfirmPassword);

//                user.Username = request.Username;

//                user.PasswordHash = EncryptedPassword;

//                user.ConfirmPassword = EncryptedConfirmPassword;

//                user.Email = request.Email;

//                user.Role = "user";

//                BLL_User.Add(user);
//                TempData["success"] = "Registered successfully";
//                return RedirectToAction("Home", "Home");
//            }
//            return View(request);
           
//        }


//        //----------------[Login]---------------------------//

        
//        [AllowAnonymous]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Login(UserDto request)
//        {

//            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
//            {
//                return RedirectToAction("Error", "Home");
//            }

//           // _ = Unauthorized();

//            Laboratoire objLab = _jwtAuthenticationService.Authenticate(request);

//            string DecryptedPassword = _jwtAuthenticationService.Decrypt(user.PasswordHash);
//            //User user = BLL_User.GetUserByName(request.Username);

//            if (user != null)
//            {
//                if (DecryptedPassword == request.Password && user.Username == request.Username)// added email
//                {
//                    //string Token = _jwtAuthenticationService.CreateToken(user);
//                    string Token = _jwtAuthenticationService.BuildToken(_configuration.GetSection("Jwt:Key").Value.ToString(), _configuration.GetSection("Jwt:Issuer").Value.ToString(), user);

//                    if (Token != null)
//                    {
//                        HttpContext.Session.SetString("Token", Token);
                       
//                    }
//                    return RedirectToAction("Acceuil", "Authentication");

//                }

//            }
//            return RedirectToAction("Error", "Home");
                  
//        }


//        //



//        [Authorize]
//        [Route("Acceuil")]
//        [HttpGet]
//        public IActionResult Acceuil()
//        {
//            string token = HttpContext.Session.GetString("Token");
//            if (token == null)
//            {
//                return (RedirectToAction("Index", "Home"));
//            }
//            if (!_jwtAuthenticationService.IsTokenValid(_configuration.GetSection("Jwt:Key").Value.ToString(), _configuration.GetSection("Jwt:Issuer").Value.ToString(), token))
//            {
//                return (RedirectToAction("Index", "Home"));
//            }


//            return View();
//        }

//        public IActionResult LogOut()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Register");
//        }
//    }
//}
