using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;
using System.IO;
using Jumia.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;



        public CategoryController(ICategoryService categoryService, IMapper mapper )
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        
        public async Task <ActionResult> Index(int pageNumber = 1)
        {
            var pageSize = 10;
            var Categoryes = await _categoryService.GetAll(pageSize, pageNumber);

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
                    TempData["SuccessMessage1"] = "Category Created successfully.";
                    return RedirectToAction("Index", TempData["SuccessMessage1"]);
                }
               

            }
            TempData["SuccessMessage"] = "Failed";
            return View(CategoryDto);



        }


        public async Task<ActionResult> Update(int id)
        {
            var res = await _categoryService.GetOne(id);

            if (res == null)
            {
                return NotFound();

            }

           // var categoryDto = _mapper.Map<CreateOrUpdateCategoryDto>(res.Entity);
            return View(res.Entity);
        }



        [HttpPost]
        public async Task<ActionResult> Update(CreateOrUpdateCategoryDto categoryDto, IFormFile Image)
        {
            try
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

                    var update = await _categoryService.Update(categoryDto, Image);
                    if (update.IsSuccess)
                    {
                        TempData["SuccessMessage2"] = "Category updated successfully.";
                        return RedirectToAction(nameof(Index), TempData["SuccessMessage2"]);
                    }


                }
                TempData["SuccessMessage"] = "Failed";
                return View(categoryDto);

            }
            catch
            {
                TempData["SuccessMessage"] = "Failed";
                return View(categoryDto);
            }
           

        }





        public async Task<ActionResult> Delete(int id)
        {
            var res = await _categoryService.GetOne(id);
            if (res == null)
            {
                return NotFound();
            }

            var CategoryToD = _mapper.Map<CreateOrUpdateCategoryDto>(res.Entity);
           var del= await _categoryService.Delete(CategoryToD);
            if(del.IsSuccess){
                TempData["SuccessMessage3"] = "Category Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["SuccessMessage"] = "Sorry,Failed to Delete this Category";
                return RedirectToAction(nameof(Index));
            }

            
        }



        public async Task<IActionResult> ExportToExcel()
        {
            var pageSize = 200;
            var Categoryes = await _categoryService.GetAll(pageSize, 1);

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("Categoryes");

            // Set column headers
            Worksheet.Cells[1, 1].Value = "Name";
            Worksheet.Cells[1, 2].Value = "Description";


            // Populate the Excel worksheet with data from Categoryes
            int row = 2;
            foreach (var category in Categoryes.Entities)
            {
                Worksheet.Cells[row, 1].Value = category.Name;
                Worksheet.Cells[row, 2].Value = category.Description;



                row++;
            }

            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Categories.xlsx");
            }
        }























    }
}
