using Jumia.Application.Services;
using Jumia.Dtos.Order;
using Jumia.Dtos.Shippment;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippmentController : ControllerBase
    {
        private readonly ShippmentService _shippmentService;
       public ShippmentController(ShippmentService shippmentService)
        {
            _shippmentService = shippmentService;
        }
        // GET: api/<ShippmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShippmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShippmentController>
        [HttpPost]
        public async Task<IActionResult> Post( CreateOrUpdateShipmentDto createOrUpdateShipmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createOrUpdateShipmentDto == null && createOrUpdateShipmentDto.CustomerId != null)
                    {
                        await _shippmentService.Create(createOrUpdateShipmentDto);
                        return Created("http://localhost:5164/api/Shippment/" + createOrUpdateShipmentDto.Id, "Your Address Information Saved Successfully");

                    }
                }
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest(ModelState);
            }
            
        }

        // PUT api/<ShippmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CreateOrUpdateShipmentDto createOrUpdateShipmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createOrUpdateShipmentDto == null && createOrUpdateShipmentDto.CustomerId != null)
                    {
                        await _shippmentService.Update(createOrUpdateShipmentDto);
                        return Created("http://localhost:5164/api/Shippment/" + createOrUpdateShipmentDto.Id, "Your Address Information Updated Successfully");

                    }
                }
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<ShippmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
