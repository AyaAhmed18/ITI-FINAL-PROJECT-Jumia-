using Jumia.Dtos.AccountDtos;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace AdminDashBoard.Controllers
{
    public class AccountController : Controller
    {
            private UserManager<UserIdentity> _userManager;
            private SignInManager<UserIdentity> _signinManager;

            public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager)
            {
                _userManager = userManager;
                _signinManager = signInManager;
            }
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
            public async Task<IActionResult> Register(RegisterDtos registerDtos)
            {
         
            if (ModelState.IsValid)
                {


                    var user = new UserIdentity() { UserName = registerDtos.Username, Email = registerDtos.Email, PhoneNumber = registerDtos.Phone };
                    IdentityResult res = await _userManager.CreateAsync(user, registerDtos.Password);

                        if (res.Succeeded && registerDtos.Password == registerDtos.Confirmpass)
                        {

                            await _signinManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Login");

                        }
                        else
                        {
                            foreach (var i in res.Errors)
                            {
                                ModelState.AddModelError("Error", i.Description);
                            }
                            return View(registerDtos);
                        }
                    }
        
                else
                {
                    return View(registerDtos);
                }
            }





        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDtos loginDtos)
        {

            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(loginDtos.Username, loginDtos.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                    return View("Login", loginDtos);
                }
            }

            return View("Login", loginDtos);
        }




        public async Task<IActionResult> logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


    }

    }
