using AutoMapper;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.Product;
using Jumia.Dtos.ProductSpecificationSubCategory;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductServices _productService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IMapper _mapper;
        private readonly ISpecificationServices _specificationServices;
        private readonly ISubCategorySpecificationsService _subCategorySpecificationsService;
        private readonly IProductSpecificationSubCategoryServices _productSpecificationSubCategoryServices;


        public ProductController(IProductServices productService,
            IMapper mapper, ISubCategoryService subCategoryService,
            ISpecificationServices specificationServices,
            ISubCategorySpecificationsService subCategorySpecificationsService,
            IProductSpecificationSubCategoryServices productSpecificationSubCategoryServices)
        {
            _productService = productService;
            _mapper = mapper;
            _subCategoryService = subCategoryService;
            _specificationServices = specificationServices;
            _subCategorySpecificationsService = subCategorySpecificationsService;
            _productSpecificationSubCategoryServices = productSpecificationSubCategoryServices;

        }
        // GET: ProductController
        public async Task<ActionResult> GetPagination()
        {
            var Prds = await _productService.GetAllPagination(5, 1);
            return View(Prds);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {

            var subCategory = await _subCategoryService.GetAll(5, 1);
            var subCatName = subCategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.SubCategory = subCatName;
            var subCategorySpec = (await _subCategorySpecificationsService.GetAll()).ToList();
            ViewBag.subCategorySpecs = subCategorySpec;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateOrUpdateProductDto ProductDto,List<IFormFile> Images, CreateOrUpdateProductSpecificationSubCategory prdSubCategorySpecDto)
        {
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
                    foreach (var specItems in ProductDto.subCategorySpecification)
                    {
                        var specName = (await _specificationServices.GetAll()).Where(s => s.Name == specItems).FirstOrDefault();
                        var subCategorySpecification = new CreateOrUpdateProductSpecificationSubCategory
                        {
                            ProductId = ProductDto.Id,
                            SubSpecId=prdSubCategorySpecDto.Id,
                            Value=prdSubCategorySpecDto.Value
                        };
                        await _productSpecificationSubCategoryServices.Create(subCategorySpecification);
                    }
                    return RedirectToAction("GetPagination");
                }


            }
            //var subcategory = await _subcategoryservice.getall(5, 1);
            //var subcatname = subcategory.entities.select(a => new { a.id, a.name }).tolist();
            //viewbag.subcategory = subcatname;
            return View(ProductDto);



        }
        //public async Task<ActionResult> CreateWithSpecifications()
        //{
        //    var spec = _specificationServices.GetAll();
        //    var specName = spec.Entities.Select(a => new { a.Id, a.Name }).ToList();
        //    ViewBag.Specifications = specName;
        //    return View();

        //}

        [Authorize(Roles = "Admin")]
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
            var productDto = _mapper.Map<CreateOrUpdateProductDto>(res.Entity);
            return View(productDto);
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(CreateOrUpdateProductDto productDto, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {

                var res  = await  _productService.Update(productDto, Image);
                return RedirectToAction(nameof(GetPagination));

            }
            var subCategory = await _subCategoryService.GetAll(5, 1);
            var subCatName = subCategory.Entities.Select(a => new { a.Id, a.Name }).ToList();
            ViewBag.SubCategory = subCatName;
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
            await _productService.Delete(ProductToDelete);


            return RedirectToAction(nameof(GetPagination));
        }

    }
}
