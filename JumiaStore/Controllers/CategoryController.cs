using Jumia.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }



        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll(50, 1);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(int id)
        {
            var Category = await _categoryService.GetOne(id);
            if (Category == null)
            {
                return Ok("not Found!");
            }


            return Ok(Category);

        }












    }
}
