using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Context.Migrations;
using Jumia.Dtos.Category;
using Jumia.Dtos.Product;
using Jumia.Dtos.ProductSpecificationSubCategory;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace AdminDashBoard.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProductController : BaseController
    {
        private readonly IProductServices _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        private readonly ISpecificationServices _specificationServices;
        private readonly ISubCategorySpecificationsService _subCategorySpecificationsService;
        private readonly IProductSpecificationSubCategoryServices _productSpecificationSubCategoryServices;


        public ProductController(IProductServices productService,
            IBrandService brandService,
            IMapper mapper, ISubCategoryService subCategoryService,
            ISpecificationServices specificationServices,
            ISubCategorySpecificationsService subCategorySpecificationsService,
            IProductSpecificationSubCategoryServices productSpecificationSubCategoryServices)
        {
            _productService = productService;
            _mapper = mapper;
            _brandService = brandService;
            _subCategoryService = subCategoryService;
            _specificationServices = specificationServices;
            _subCategorySpecificationsService = subCategorySpecificationsService;
            _productSpecificationSubCategoryServices = productSpecificationSubCategoryServices;

        }
        // GET: ProductController
        public async Task<ActionResult> GetPagination(int pageNumber = 1)
        {
            var pageSize = 10;
            var Prds = await _productService.GetAllPagination(pageSize, pageNumber);
            return View(Prds);
        }

        public async Task<IActionResult> ExportToExcel()
        {
            var pageSize = 200;
            var Prds = await _productService.GetAllPagination(pageSize, 1);

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("Prds");

            // Set column headers
            Worksheet.Cells[1, 1].Value = "English Name";
            Worksheet.Cells[1, 2].Value = "Arabic Name";
            Worksheet.Cells[1, 3].Value = "Description";
            Worksheet.Cells[1, 4].Value = "Quantity";
            Worksheet.Cells[1, 5].Value = "Price";
            Worksheet.Cells[1, 6].Value = "Brand";


            // Populate the Excel worksheet with data from Categoryes
            int row = 2;
            foreach (var product in Prds.Entities)
            {
                Worksheet.Cells[row, 1].Value = product.Name;
                Worksheet.Cells[row, 2].Value = product.NameAr;
                Worksheet.Cells[row, 3].Value = product.ShortDescription;
                Worksheet.Cells[row, 4].Value = product.StockQuantity;
                Worksheet.Cells[row, 5].Value = product.RealPrice;
                Worksheet.Cells[row, 6].Value = product.BrandName;



                row++;
            }

            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
            }
        }


        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {

            var subCategory = await _subCategoryService.GetAll(30, 1);
            var subCatName = subCategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.SubCategory = subCatName;

            var subCategorySpec = (await _subCategorySpecificationsService.GetAll()).ToList();
            ViewBag.subCategorySpecs = subCategorySpec;

            var brand = (await _brandService.GetAll()).Entities.Select(a => new { a.BrandID, a.Name }).ToList();
            ViewBag.brand = brand;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateOrUpdateProductDto ProductDto,List<IFormFile> Images, CreateOrUpdateProductSpecificationSubCategory prdSubCategorySpecDto, int selectedSubCategoryId)
        {
            var subCategorySpec = (await _subCategorySpecificationsService.GetAll()).Where(i => i.SubCategoryId == selectedSubCategoryId).ToList();

            if (ModelState.IsValid)
            {
               
                if (Images != null)
                {
                    foreach (var img in Images)
                    {
                        var imageBytes = new byte[img.Length];
                        using (var stream = img.OpenReadStream())
                        {
                            await stream.ReadAsync(imageBytes, 0, imageBytes.Length);
                        }
                        ProductDto.Images.Add(imageBytes);
                    }
                }


                var res = await _productService.Create(ProductDto, Images);

                if (res.IsSuccess)
                {
                    if(prdSubCategorySpecDto != null) {
                               foreach (var specItems in subCategorySpec)
                    {
                       // var specName = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault();
                        var subCategorySpecification = new CreateOrUpdateProductSpecificationSubCategory
                        {
                            ProductId = res.Entity.Id,
                            SubSpecId= specItems.Id,
                            Value=prdSubCategorySpecDto.Value
                        };
                        await _productSpecificationSubCategoryServices.Create(subCategorySpecification);
                    }
                        TempData["SuccessMessage1"] = "Category Created successfully.";
                        return RedirectToAction("GetPagination", TempData["SuccessMessage1"]);

                    }

                }


            }
            var subcategory = await _subCategoryService.GetAll(55, 1);
            var subcatname = subcategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.subcategory = subcatname;
            var brand = (await _brandService.GetAll()).Entities.Select(a => new { a.BrandID, a.Name }).ToList();
            ViewBag.brand = brand;
            TempData["SuccessMessage"] = "Failed";
            ViewBag.subCategorySpecs = subCategorySpec;
            return View(ProductDto);



        }
        [HttpGet]
        public async Task<IActionResult> Action(int selectedSubCategoryId)
        {
            var subCategorySpec = (await _subCategorySpecificationsService.GetAll()).Where(i => i.SubCategoryId == selectedSubCategoryId).ToList(); 
            ViewBag.subCategorySpecs = subCategorySpec;
            return PartialView("_SubCategorySpecsPartial");
        }

        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromRoute]int id)
        {
            var res = await _productService.GetOne(id);

            if (res == null)
            {
                return NotFound();

            }
            var subCategory = await _subCategoryService.GetAll(5, 1);
            var subCatName = subCategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.SubCategory = subCatName;
            var brand = (await _brandService.GetAll()).Entities.Select(a => new { a.BrandID, a.Name }).ToList();
            ViewBag.brand = brand;
            var productDto = _mapper.Map<CreateOrUpdateProductDto>(res.Entity);
            return View(productDto);
        }



        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(CreateOrUpdateProductDto productDto, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {

                var res  = await  _productService.Update(productDto, Image);
                TempData["SuccessMessage1"] = "Product Created successfully.";
                return RedirectToAction("GetPagination", TempData["SuccessMessage1"]);


            }
            var subCategory = await _subCategoryService.GetAll(5, 1);
            var subCatName = subCategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.SubCategory = subCatName;
            var brand = (await _brandService.GetAll()).Entities.Select(a => new { a.BrandID, a.Name }).ToList();
            ViewBag.brand = brand;
            TempData["SuccessMessage"] = "Failed.";
             return View(productDto);

        }





        public async Task<ActionResult> Delete(int id)
        {
            var res = await _productService.GetOne(id);
            if (res == null)
            {
                return NotFound();
            }

            var ProductToDelete = _mapper.Map<CreateOrUpdateProductDto>(res.Entity);
           var del= await _productService.Delete(ProductToDelete);
            if (del.IsSuccess)
            {
                TempData["SuccessMessage1"] = "Successed";
                return RedirectToAction(nameof(GetPagination));
            }
            else
            {
                TempData["SuccessMessage"] = "Failed";
                return RedirectToAction(nameof(GetPagination));
            }

            
        }

    }
}
