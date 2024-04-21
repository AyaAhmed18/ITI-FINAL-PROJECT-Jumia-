using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Context.Migrations;
using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategory;
using Jumia.Dtos.SubCategorySpecifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
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






        public async Task <ActionResult> Index(int pageNumber = 1)
        {
            var pageSize = 10;
            var SubCategoryes = await _subCategoryService.GetAll(pageSize, pageNumber);

            return View(SubCategoryes);
        }


        public async Task<ActionResult> Create()
        {
            var Categories = await _categoryService.GetAll(30, 1);
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

                    TempData["SuccessMessage1"] = "SubCategory Created successfully.";
                    return RedirectToAction("Index", TempData["SuccessMessage1"]);
                }
            }
            var Categories = await _categoryService.GetAll(30, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;
            var spec = (await _specificationServices.GetAll()).ToList();
            ViewBag.spec = spec;
            TempData["SuccessMessage"] = "Failed.";
            return View(SubDto);
        }





        public async Task<ActionResult> Update(int id)
        {

            var Categories = await _categoryService.GetAll(30, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;
           
            var subSpec = (await _subCategorySpecificationsService.GetAll()).Where(i=>i.SubCategoryId==id).ToList();
           // var specName = subSpec.Select(s => s.SpecificationName);
            ViewBag.subSpec= subSpec;

           // var spec = (await _specificationServices.GetAll())
             //   .Where(s => !specName.Contains(s.Name)).ToList();
            //ViewBag.spec = spec;
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
                var res = await _subCategoryService.Update(SubDto, Image);
                if (res.IsSuccess)
                {
                    //  var selectedSpec= _subCategorySpecificationsService
                    /* foreach (var specItems in SubDto.SelectedSpecification)
                     {
                         var specName = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault();
                         subCategorySpecificationDto.specificationId = specName.Id;
                         subCategorySpecificationDto.SubCategoryId = SubDto.Id;
                         await _subCategorySpecificationsService.Create(subCategorySpecificationDto);
                     }*/

                    TempData["SuccessMessage2"] = "SubCategory Updated successfully.";
                    return RedirectToAction("Index", TempData["SuccessMessage1"]);
                }

                TempData["SuccessMessage"] = "Failed.";
                return View(SubDto);



            }
            var Categories = await _categoryService.GetAll(30, 1);
            var CategoryName = Categories.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.Category = CategoryName;
            TempData["SuccessMessage"] = "Failed.";
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
            var specs =( await _subCategorySpecificationsService.GetAll()).Where(s=>s.SubCategoryId == id).ToList();
            foreach (var spec in specs)
            {
                await _subCategorySpecificationsService.Delete(spec.Id);
            }
           var del= await _subCategoryService.Delete(SubCategoryToD);


            if (del.IsSuccess)
            {
                TempData["SuccessMessage3"] = "This SubCategory Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["SuccessMessage"] = "Sorry ,Failed to delete this SubCategory";
                return RedirectToAction(nameof(Index));
            }
        }



        public async Task<IActionResult> ExportToExcel()
        {
            var pageSize = 200;
            var SubCategoryes = await _subCategoryService.GetAll(pageSize, 1);

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("SubCategoryes");

            // Set column headers
            Worksheet.Cells[1, 1].Value = "Name";
            Worksheet.Cells[1, 2].Value = "Description";


            // Populate the Excel worksheet with data from SubCategoryes
            int row = 2;
            foreach (var subcategory in SubCategoryes.Entities)
            {
                Worksheet.Cells[row, 1].Value = subcategory.Name;
                Worksheet.Cells[row, 2].Value = subcategory.Description;



                row++;
            }

            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SubCategories.xlsx");
            }
        }
























    }
}
