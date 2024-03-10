using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Context;
using Jumia.Dtos.User;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class _ِAdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public _ِAdminController(IRoleService roleService, IUserService userService) {

            _roleService= roleService;
            _userService = userService;

        }
        public async Task<IActionResult>Admin()
        {
            var rolesResult = await _roleService.GetUsername();

            return View( rolesResult.ToList());  
        }

        public async Task<IActionResult> AddUser()
        {
            var Role = await _roleService.GetAll();
            ViewBag.Role = Role.Entities;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(GetAllUsers getAllUsers)
        {
            try
            {
                var res = await _userService.CreateAsync(getAllUsers);
                if (res.IsSuccess == true)
                {
                    return RedirectToAction("Admin");
                }
                else
                {
                    ViewBag.Error = res.Message;
                    return View(getAllUsers);
                }
            }
            catch
            {
                return View();
            }
        }


    }
}
