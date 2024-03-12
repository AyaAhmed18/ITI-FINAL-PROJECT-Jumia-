using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;



        public CategoryController(ICategoryService categoryService, IMapper mapper )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        public async Task <ActionResult> Index()
        {
            var Categoryes = await _categoryService.GetAll(10 , 1);

            return View(Categoryes);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateCategoryDto CategoryDto)
        {
            if (ModelState.IsValid)
            {
                var res = await _categoryService.Create(CategoryDto);

                if (res.IsSuccess)
                {

                    return RedirectToAction("Index");
                }
               

            }
            return View(CategoryDto);



        }


        public async Task<ActionResult> Update(int id)
        {
            var res = await _categoryService.GetOne(id);

            if (res == null)
            {
                return NotFound();

            }

            var categoryDto = _mapper.Map<CreateOrUpdateCategoryDto>(res.Entity);
            return View(categoryDto);
        }



        [HttpPost]
        public async Task<ActionResult> Update(CreateOrUpdateCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetOne(categoryDto.Id);
                if (category == null)
                {
                    return NotFound(nameof(categoryDto));
                }


                await _categoryService.Update(categoryDto);

                return RedirectToAction(nameof(Index));

            }

            return View(categoryDto);

        }





        public async Task<ActionResult> Delete(int id)
        {
            var res = await _categoryService.GetOne(id);
            if (res == null)
            {
                return NotFound();
            }

            var CategoryToD = _mapper.Map<CreateOrUpdateCategoryDto>(res.Entity);
            await _categoryService.Delete(CategoryToD);


            return RedirectToAction(nameof(Index));
        }


























    }
}
