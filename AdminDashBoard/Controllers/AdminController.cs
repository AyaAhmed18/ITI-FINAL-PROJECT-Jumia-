using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Application.Services.Services;
using Jumia.Context;
using Jumia.Dtos.User;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signinManager;
        private readonly RoleManager<UserRole> _roleManager;

        public AdminController(IRoleService roleService, UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<UserRole> roleManager)
        {

            _roleService = roleService;
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Admin()
        {
            var rolesResult = await _roleService.GetUsername();
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
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
                    //if (!string.IsNullOrEmpty(getAllUsers.RoleName) && await _roleManager.RoleExistsAsync(getAllUsers.RoleName))

                    //    await _userManager.AddToRoleAsync(user, getAllUsers.RoleName);


                    //return RedirectToAction("Index"); 

                    if (getAllUsers.SelectedRoles != null)
                    {
                        foreach (var roleName in getAllUsers.SelectedRoles)
                        {
                            if (!string.IsNullOrEmpty(roleName) && await _roleManager.RoleExistsAsync(roleName))
                            {
                                await _userManager.AddToRoleAsync(user, roleName);
                            }
                        }
                    }

                    return RedirectToAction("Index");

                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                  
                    ViewBag.Roles = _roleManager.Roles.ToList();

                    return View(getAllUsers);
                }
            }

           
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(getAllUsers);
        }
    }
}

