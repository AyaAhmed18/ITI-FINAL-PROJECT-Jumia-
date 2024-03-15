using Jumia.Dtos.User;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AdminDashBoard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<UserRole> _roleManager;

        public RoleController(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRole()
        {
            return View();
        }



        //[HttpPost]
        //public async Task<IActionResult> CreateRole(RoleDtos roleDtos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!await _roleManager.RoleExistsAsync(roleDtos.RoleName))
        //        {
        //            var role = new UserRole { Name = roleDtos.RoleName };
        //            await _roleManager.CreateAsync(role);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Role already exists.");
        //        }
        //    }

        //    return View(roleDtos);
        //}


        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDtos roleDTO)
        {
            if (string.IsNullOrEmpty(roleDTO.RoleName))
            {
                ModelState.AddModelError("", "Role name is required");
                return View();
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleDTO.RoleName);
            if (roleExist)
            {
                ModelState.AddModelError("", "Role name already exists");
                return View();
            }

            var result = await _roleManager.CreateAsync(new UserRole { Name = roleDTO.RoleName });
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
        }
    }
}