using Jumia.Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecficationController : ControllerBase
    {
        private IProductSpecificationSubCategoryServices _services;
        public SpecficationController(IProductSpecificationSubCategoryServices services)
        {
            _services=services;
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductSpecfications(int Id)
        {
            try
            {
                var spec = (await _services.GetAll()).Entities.Where(p=>p.ProductId==Id);
                if (spec != null)
                {
                    return (Ok(spec));
                }
                else
                {
                    return NotFound("this specs Not Found");
                }
            }
            catch
            {
                return BadRequest();
            }


        }

    }
}
