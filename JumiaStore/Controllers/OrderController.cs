using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Dtos.Order;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
           // _orderItemService = orderItemService;
            //_userManager = userManager;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [Authorize (Roles ="user")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrUpdateOrderDto createOrderDto)
        {
            if (ModelState.IsValid)
            {
                await _orderService.Create(createOrderDto);
                //url.link()
                return Created("http://localhost:5164/api/Order/" + createOrderDto.Id, "Order Saved Successfully");
            }
            return BadRequest(ModelState);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
