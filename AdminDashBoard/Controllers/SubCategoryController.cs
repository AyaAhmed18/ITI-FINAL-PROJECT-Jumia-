using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategory;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class SubCategoryController : BaseController
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;





        public SubCategoryController(ISubCategoryService subCategoryService , ICategoryService categoryService ,IMapper mapper )
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
            _mapper = mapper;


        }






        public async Task <ActionResult> Index()
        {
            var SubCategoryes = await _subCategoryService.GetAll(10, 1);

            return View(SubCategoryes);
        }


        public async Task<ActionResult> Create()
        {
            var Categories = await _categoryService.GetAll(5, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;
            return View();
        }

        
        [HttpPost]

        public async Task<ActionResult> Create(CreateOrUpdateSubDto SubDto, IFormFile Image)
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
                    SubDto.Image = imageBytes;
                }


                var res = await _subCategoryService.Create(SubDto, Image);

                if (res.IsSuccess)
                {

                    return RedirectToAction("Index");
                }

            }
            return View(SubDto);



        }





        public async Task<ActionResult> Update(int id)
        {

            var Categories = await _categoryService.GetAll(5, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;

            var res = await _subCategoryService.GetOne(id);

            if (res == null)
            {
                return NotFound();

            }

      

            var SubCategoryDto = _mapper.Map<CreateOrUpdateSubDto>(res.Entity);


            return View(SubCategoryDto);


        }

     

        [HttpPost]
        public async Task<ActionResult> Update(CreateOrUpdateSubDto SubDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
               

                var SubCategory = await _subCategoryService.GetOne(SubDto.Id);
                if (SubCategory == null)
                {
                    return NotFound(nameof(SubDto));
                }


                if (Image != null && Image.Length > 0)
                {

                    var imageBytes = new byte[Image.Length];
                    using (var stream = Image.OpenReadStream())
                    {
                        await stream.ReadAsync(imageBytes, 0, imageBytes.Length);
                    }
                    SubDto.Image = imageBytes;
                }




                await _subCategoryService.Update(SubDto, Image);

                return RedirectToAction(nameof(Index));

                

            }
            var Categories = await _categoryService.GetAll(5, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;

            return View(SubDto);

        }






        public async Task<ActionResult> Delete(int id)
        {
            var res = await _subCategoryService.GetOne(id);
            if (res == null)
            {
                return NotFound();
            }

            var SubCategoryToD = _mapper.Map<CreateOrUpdateSubDto>(res.Entity);
            await _subCategoryService.Delete(SubCategoryToD);


            return RedirectToAction(nameof(Index));
        }



























    }
}
