using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Application.Services.Services;
using Jumia.Context;
using Jumia.Dtos.AccountDtos;
using Jumia.Dtos.Category;
using Jumia.Dtos.User;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signinManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IRoleService roleService, UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<UserRole> roleManager , IUserService userService,IMapper mapper)
        {

            _roleService = roleService;
            _userManager = userManager;
            _signinManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;

            _mapper= mapper;

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

                if (res.Succeeded && res.Succeeded && getAllUsers.Password == getAllUsers.Confirmpass)
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

                    return RedirectToAction("Admin");

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
        public async Task<ActionResult> Delete(int id)
        {
            var res = await _userService.GetOne(id);
            if (res == null)
            {
                return NotFound();
            }

            var UserDto = _mapper.Map<GetAllUsers>(res.Entity);
            var del = await _userService.Delete(UserDto);
            if (del.IsSuccess)
            {
                TempData["SuccessMessage3"] = "User Deleted Successfully";
                return RedirectToAction(nameof(Admin));
            }
            else
            {
                TempData["SuccessMessage"] = "Sorry,Failed to Delete this User";
                return RedirectToAction(nameof(Admin));
            }
        }


        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

       
            var roles = await _roleManager.Roles.ToListAsync();

          
            ViewBag.Roles = roles;

           
            var userDto = new GetAllUsers
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.PasswordHash,
               // Confirmpass = user.PasswordHash
            };

         var selectedRoleIds = roles.Where(r => userRoles.Contains(r.Name)).Select(r => r.Id.ToString()).ToList();
             userDto.SelectedRoles = selectedRoleIds;
            return View(userDto);
        }


        [HttpPost]
     public async Task<IActionResult> EditUser(int id, GetAllUsers userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
                user.PhoneNumber = userDto.PhoneNumber;
                user.PasswordHash = userDto.Password;
                 var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
                foreach (var roleId in userDto.SelectedRoles)
                {
                    var role = await _roleManager.FindByIdAsync(roleId);
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                await _userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Admin));
            }

            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;

            return View(userDto);
        }


    }
}

