using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {

        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService) 
        {
          _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var SubCategories = await _subCategoryService.GetAll(50, 1);

            return Ok(SubCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var SubCategory = await _subCategoryService.GetOne(id);
            if (SubCategory == null)
            {
                return Ok("not Found!");
            }


            return Ok(SubCategory);

        }

        [HttpGet("GetByCatId")]
        public async Task<ActionResult> GetByCatId(int CatId)
        {
            var SubCategories = await _subCategoryService.GetByCategoryId(CatId);

            return Ok(SubCategories.Entities);
        }








    }
}
