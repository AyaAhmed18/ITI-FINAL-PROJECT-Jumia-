using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly UserManager<UserIdentity> _userManager;
        public OrderItemsController(IOrderService orderService, IOrderItemService orderItemService, UserManager<UserIdentity> userManager)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _userManager = userManager;
        }


        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetOrderItems(int OrderId)
        {
            try
            {
                var ord = (await _orderItemService.GetAllOrderItems()).Where(i => i.OrderId == OrderId).ToList();
                if (ord != null)
                {
                    return (Ok(ord));
                }
                else
                {
                    return NotFound("this Order Items Not Found");
                }
            }
            catch
            {
                return BadRequest();
            }


        }

     //  //
        [HttpPost]
        public async Task<IActionResult> Post(CreatOrUpdateOrderItemsDto creatOrUpdateOrderItems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = await _orderItemService.Create(creatOrUpdateOrderItems);
                    if (order.IsSuccess)
                    {
                        return Created("http://localhost:5164/api/Order/" + creatOrUpdateOrderItems.Id, "Order Saved Successfully");

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

    }
}
