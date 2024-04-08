using Jumia.Application.IServices;
using Jumia.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandServices;

        public BrandController(IBrandService brandService)
        {
            _brandServices = brandService;

        }
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var Brands = await _brandServices.GetAll();
                return Ok(Brands.Entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
