using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Context;
using Jumia.Dtos.AccountDtos;
using Jumia.Dtos.User;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminDashBoard.Controllers
{
    public class _ِAdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signinManager;
        private readonly RoleManager<UserRole> _roleManager;

        public _ِAdminController(IRoleService roleService, IUserService userService, UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<UserRole> roleManager)
        {

            _roleService = roleService;
            _userService = userService;
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;

        }
        public async Task<IActionResult> Admin()
        {
            var rolesResult = await _roleService.GetUsername();

            return View(rolesResult.ToList());
        }



        public IActionResult Adduser()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adduser(GetAllUsers getAllUsers)
        {
            if (ModelState.IsValid)
            {
                var user = new UserIdentity()
                {
                    UserName = getAllUsers.UserName,
                    Email = getAllUsers.Email,
                    PhoneNumber = getAllUsers.PhoneNumber
                };

                IdentityResult res = await _userManager.CreateAsync(user, getAllUsers.Password);

                if (res.Succeeded)
                {
                    if (!string.IsNullOrEmpty(getAllUsers.RoleName) && await _roleManager.RoleExistsAsync(getAllUsers.RoleName))

                        await _userManager.AddToRoleAsync(user, getAllUsers.RoleName);
                    

                    return RedirectToAction("Index"); // Redirect to the desired action
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    // Repopulate the roles dropdown in case of validation errors
                    ViewBag.Roles = _roleManager.Roles.ToList();

                    return View(getAllUsers);
                }
            }

            // Repopulate the roles dropdown in case of validation errors
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(getAllUsers);
        }
    }
}