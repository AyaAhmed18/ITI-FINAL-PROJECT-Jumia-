using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Dtos.Order;
using Jumia.Dtos.Shippment;
using Jumia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data;

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
        public async Task<IActionResult> Get()
        {
            var order = (await _orderService.GetAllOrders())
       .Select(i => new
       {
           i.Customer,
           i.Status,
           PaymentStatus = i.GetPaymentStatusWord(),
           i.OrderDate,
           i.Discount,
           i.TotalOrderPrice
       })
       .ToList();
            return Ok(order);
        }

        // GET api/<OrderController>/5
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserOrders(int UserId)
        {
            try
            {
                var ord = (await _orderService.GetAllOrders());
                var cOrd = ord.Where(i => i.CustomerId == UserId);
                if (cOrd != null)
                {
                    return (Ok(cOrd));
                }
                else
                {
                    return NotFound("this Order Not Found");
                }
            }
            catch
            {
                return BadRequest("SomeThing went wrong");
            }
            
            
        }


        [HttpGet("user/{Id}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
            try
            {
                var ord = (await _orderService.GetOrder(Id));
                if (ord != null)
                {
                    return (Ok(ord));
                }
                else
                {
                    return NotFound("this Order Not Found");
                }
            }
            catch
            {
                return BadRequest();
            }


        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateOrUpdateOrderDto createOrderDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = await _orderService.Create(createOrderDto);
                    if (order.IsSuccess)
                    {
                        return (Ok(order));
                    }
                    else
                    {
                        return Ok("Invaliiiid");

                    }

                }
                return StatusCode(500, "Erroras");
                //url.link()
            }
            catch (Exception ex) { return Ok("this Is a problem here"); }
            //  return BadRequest(ModelState);
        }
        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(CreateOrUpdateOrderDto orderDto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var ord = await _orderService.GetOrder(orderDto.Id);
                    if (ord !=null)
                    {
                       var order = await _orderService.Update(orderDto);
                        if (order.IsSuccess)
                        {
                            return Created("http://localhost:5164/api/Order/" + orderDto.Id, "Your Address Information Updated Successfully");

                        }
                        else
                        {
                            return Ok("Enter valid Data");
                        }

                    }
                }
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
