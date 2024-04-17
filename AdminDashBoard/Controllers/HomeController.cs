using AdminDashBoard.Models;
using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services.IServices;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoleService _roleService;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IProductServices _productService;
        private readonly IBrandService _brandService;
        private readonly IOrderService _orderService;
        public HomeController(ILogger<HomeController> logger,
            IRoleService roleService, RoleManager<UserRole> roleManager,
            ICategoryService categoryService, ISubCategoryService subCategoryService,
            IProductServices productService,
            IBrandService brandService,IOrderService orderService)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _productService = productService;
            _orderService = orderService;
            _brandService = brandService;
            _logger = logger;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
