using Jumia.Application.IServices;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Product;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductServices productServices, ICategoryService categoryService)
        {
            _productServices = productServices;
            _categoryService = categoryService;


        }
        //[HttpGet]
        //public async Task<IActionResult> Getall()
        //{

        //    try
        //    {
        //        var products = await _productServices.GetAllPagination(10, 1);

        //        return Ok(products.Entities);
        //    }
        //    catch (Exception ex) { return StatusCode(500, ex.Message); }

        //}

        [HttpGet]
        public async Task<IActionResult> GetProducts(int pageSize, int pageNumber)
        {
            try
            {
                var products = await _productServices.GetAllPagination(pageSize, pageNumber);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetbyId(int id)
        {


            var productid = await _productServices.GetOne(id);
            if (productid == null)
            {
                return Ok("notfound");
            }
            else
            {
                return Ok(productid);
            }

        }
        [HttpGet]
        [Route("{Name:Alpha}")]
        public async Task<IActionResult> GetbyName(string name)

        {
            var productname = await _productServices.Getbyname(name);
            if (productname == null)
            {
                return Ok("NotFound");
            }
            else
            {
                return Ok(productname);
            }
        }
        [HttpGet("GetOrderedAsc")]
        public async Task<IActionResult> GetOrderedAsc()
        {
            var Prds = await _productServices.GetOrderedAsc();
            return Ok(Prds.Entities);
        }



        [HttpGet("GetOrderedDsc")]
        public async Task<IActionResult> GetOrderedDsc()
        {
            var Prds = await _productServices.GetOrderedDsc();
            return Ok(Prds.Entities);
        }

        [HttpGet("GetNewestArrivals")]
        public async Task<IActionResult> GetNewestArrivals()
        {
            var Prds = await _productServices.GetNewestArrivals();
            return Ok(Prds.Entities);
        }


        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByNameOrDesc(string PartialName)
        {
            var Prds = await _productServices.Search(PartialName);
            return Ok(Prds.Entities);
        }
        [HttpGet("FilterByPriceRange")]
        public async Task<IActionResult> FilterByPriceRange(int MinPrice, int MaxPrice)
        {
            var Prds = await _productServices.FilterByPriceRange(MinPrice, MaxPrice);
            return Ok(Prds);
        }
        [HttpGet("FilterByBrandName")]
        public async Task<IActionResult> FilterByBrandName(int BrandId)
        {
            var Prds = await _productServices.FilterByBrandName(BrandId);
            return Ok(Prds);
        }
        [HttpGet("FilterByBrandList")]
        public async Task<IActionResult> FilterByBrandList([FromQuery] string BrandList)
        {
            if (BrandList[BrandList.Length - 1] == ',') { BrandList = BrandList.Substring(0, BrandList.Length - 1); }

            List<int> brandIds = BrandList.Split(',').Select(int.Parse).ToList();

            var Prds = await _productServices.FilterByBrandList(brandIds);
            return Ok(Prds);
        }
        [HttpGet("FilterByDiscountRange")]
        public async Task<IActionResult> FilterByDiscountRange(int MinDisc)
        {
            var Prds = await _productServices.FilterByDiscountRange(MinDisc);
            return Ok(Prds);
        }
        [HttpGet("FilterByAll")]
        public async Task<IActionResult> FilterByAll([FromQuery] string? BrandList,int? MinPrice, int? MaxPrice, int? MinDisc)
        {
            List<int> brandIds = new List<int>();
            if (BrandList != null&& BrandList != "")
            {
                if (BrandList[BrandList.Length - 1] == ',') { BrandList = BrandList.Substring(0, BrandList.Length - 1); }
                brandIds = BrandList.Split(',').Select(int.Parse).ToList();

            }
            else
            {
                brandIds = null;
            }


            var Prds = await _productServices.FilterByAll(brandIds,MinPrice,MaxPrice,MinDisc);

            return Ok(Prds);
        }
        [HttpGet("FilterByAllWithPagination")]
        public async Task<IActionResult> FilterByAllWithPagination([FromQuery] string? BrandList, int? MinPrice, int? MaxPrice, int? MinDisc,int pageSize, int pageNumber)
        {
            List<int> brandIds = new List<int>();
            if (BrandList != null && BrandList != "")
            {
                if (BrandList[BrandList.Length - 1] == ',') { BrandList = BrandList.Substring(0, BrandList.Length - 1); }
                brandIds = BrandList.Split(',').Select(int.Parse).ToList();

            }
            else
            {
                brandIds = null;
            }


            var Prds = await _productServices.FilterByAll(brandIds, MinPrice, MaxPrice, MinDisc,pageSize, pageNumber);

            return Ok(Prds);
        }
        [HttpGet("GetbyCategoryId")]
        public async Task<IActionResult> GetbyCategoryId(int CatId, int pageSize, int pageNumber)
        {
            var productname = await _productServices.GetProductsByCategoryId(CatId, pageSize, pageNumber);
            if (productname == null)
            {
                return Ok("NotFound");
            }
            else
            {
                return Ok(productname);
            }
        }
        [HttpGet("GetbySubCategoryId")]
        public async Task<IActionResult> GetbySubCategoryId(int SubCatId, int pageSize, int pageNumber)
        {
            var productname = await _productServices.GetProductsBySubCategoryId(SubCatId, pageSize, pageNumber);
            if (productname == null)
            {
                return Ok("NotFound");
            }
            else
            {
                return Ok(productname);
            }
        }
    }
}
////Bahaa http://localhost:5094/api/Product