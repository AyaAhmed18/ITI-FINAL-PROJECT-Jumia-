using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategory;
using Jumia.Dtos.SubCategorySpecifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    public class SubCategoryController : BaseController
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly ICategoryService _categoryService;
        private readonly ISpecificationServices _specificationServices;
        private readonly ISubCategorySpecificationsService _subCategorySpecificationsService;
        private readonly IMapper _mapper;





        public SubCategoryController(ISubCategoryService subCategoryService , ICategoryService categoryService ,IMapper mapper, ISpecificationServices specificationServices, ISubCategorySpecificationsService subCategorySpecificationsService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
            _specificationServices = specificationServices;
            _subCategorySpecificationsService = subCategorySpecificationsService;
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
            var spec = (await _specificationServices.GetAll()).ToList();
            ViewBag.spec = spec;
            return View();
        }

        
        [HttpPost]

        public async Task<ActionResult> Create(CreateOrUpdateSubDto SubDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {

                if (Image != null && Image.Length > 0 && SubDto.SelectedSpecification != null)
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
                    foreach (var specItems in SubDto.SelectedSpecification)
                    {
                        var specName = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault();
                        var subCategorySpecification = new CreateOrUpdateSubCategorySpecificationDto
                        {
                            SubCategoryId = res.Entity.Id,
                            specificationId = specName.Id
                        };
                         await _subCategorySpecificationsService.Create(subCategorySpecification);
                    }
                 
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
            var spec = (await _specificationServices.GetAll()).ToList();
            ViewBag.spec = spec;
            var res = await _subCategoryService.GetOne(id);

            if (res == null)
            {
                return NotFound();

            }
            var SubCategoryDto = _mapper.Map<CreateOrUpdateSubDto>(res.Entity);


            return View(SubCategoryDto);


        }

     

        [HttpPost]
        public async Task<ActionResult> Update(CreateOrUpdateSubDto SubDto, IFormFile Image,CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto)
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
                var res = await _subCategoryService.Update(SubDto, Image);
                if (res.IsSuccess)
                {
                  //  var selectedSpec= _subCategorySpecificationsService
                    foreach (var specItems in SubDto.SelectedSpecification)
                    {
                        var specName = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault();
                        subCategorySpecificationDto.specificationId = specName.Id;
                        subCategorySpecificationDto.SubCategoryId = SubDto.Id;
                        await _subCategorySpecificationsService.Update(subCategorySpecificationDto);
                    }

                    return RedirectToAction("Index");
                }

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
