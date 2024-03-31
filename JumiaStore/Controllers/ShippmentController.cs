using Jumia.Application.IServices;
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
        private readonly IShippmentService _shippmentService;
       public ShippmentController(IShippmentService shippmentService)
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
        [HttpGet("{ordId}")]
        public async Task<IActionResult> Get(int ordId)
        {
            var shipping = (await _shippmentService.GetAll()).Where(i => i.OrderId == ordId).FirstOrDefault();
            if (shipping == null)
            {
                return NotFound("Not Found");
            }
            return Ok(shipping);
        }

        // POST api/<ShippmentController>
        [HttpPost]
        public async Task<IActionResult> Post( CreateOrUpdateShipmentDto createOrUpdateShipmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (createOrUpdateShipmentDto != null)
                    {
                        var ship=await _shippmentService.Create(createOrUpdateShipmentDto);
                        if (ship.IsSuccess)
                        {
                            return Created("http://localhost:5164/api/Shippment/" + createOrUpdateShipmentDto.Id, "Your Address Information Saved Successfully");

                        }
                        else
                        {
                            return Ok("something went wrong Please Try again");
                        }


                    }
                    return BadRequest("Form Should not be null");

                }
                else
                {
                    return BadRequest(ModelState.Values);
                }
               

            }
            catch
            {
                return BadRequest(ModelState);
            }
            
        }

        // PUT api/<ShippmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(CreateOrUpdateShipmentDto createOrUpdateShipmentDto)
        {
            try
            {
                var ship = await _shippmentService.GetOne(createOrUpdateShipmentDto.Id);
                if (ModelState.IsValid)
                {
                    if (ship != null && createOrUpdateShipmentDto.OrderId != null)
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
        public async Task<IActionResult> Delete(CreateOrUpdateShipmentDto shipmentDto)
        {
            var shipping = (await _shippmentService.GetOne(shipmentDto.Id));
            if (shipping != null)
            {
               await _shippmentService.Delete(shipmentDto);
                return Ok("Deleted");
            }
            return BadRequest("Enter Valid Id");
        }
    }
}
