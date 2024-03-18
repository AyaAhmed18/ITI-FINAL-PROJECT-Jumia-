using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.IO;
using Jumia.Application.Services.IServices;

namespace AdminDashBoard.Controllers
{
    public class CategoryController : BaseController
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
        public async Task<ActionResult> Create(CreateOrUpdateCategoryDto CategoryDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null && Image.Length > 0)
                {
                   
                    var imageBytes = new byte[Image.Length];
                    using (var stream = Image.OpenReadStream())
                    {
                        await stream.ReadAsync(imageBytes, 0, imageBytes.Length);
                    }
                    CategoryDto.Image = imageBytes;
                }


                var res = await _categoryService.Create(CategoryDto, Image);

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
        public async Task<ActionResult> Update(CreateOrUpdateCategoryDto categoryDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                var category = await _categoryService.GetOne(categoryDto.Id);
                if (category == null)
                {
                    return NotFound(nameof(categoryDto));
                }

                if (Image != null && Image.Length > 0)
                {

                    var imageBytes = new byte[Image.Length];
                    using (var stream = Image.OpenReadStream())
                    {
                        await stream.ReadAsync(imageBytes, 0, imageBytes.Length);
                    }
                    categoryDto.Image = imageBytes;
                }
                else
                {
                    // If no new image is provided, retain the existing image
                    categoryDto.Image = category.Entity.Image;
                }
              
                await _categoryService.Update(categoryDto, Image);

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
