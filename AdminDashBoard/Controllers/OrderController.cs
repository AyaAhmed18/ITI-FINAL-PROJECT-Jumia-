using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography;

namespace AdminDashBoard.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly UserManager<UserIdentity> _userManager;
        public OrderController(IOrderService orderService,IOrderItemService orderItemService, UserManager<UserIdentity> userManager)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
            _userManager = userManager;
        }

        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            if (_orderService != null)
            {
                var orders = await _orderService.GetAllOrders();
                // var ord = orders.ToList();
                return View(orders);
            }
            else
            {
                return View();
            }
            
           
        }

        // GET: OrderController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            { 
            if (_orderItemService != null)
            {
                var ordersDetails = await _orderItemService.GetAllOrderItems();
                var items =  ordersDetails.Where(i => i.OrderId == id).ToList();
                ViewBag.orderId = id;
                return View(items);
            }
            return View();
            }
            catch
            {
                return Content("SomeThing Went Wrong, Try again");
            }
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           
            var order = await _orderService.GetOrder(id);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == order.CustomerId)?.UserName;
            /*if (user != null)
            {
                order.Customer = user;
            }*/
            var orderStatusValues = Enum.GetValues(typeof(CreateOrUpdateOrderDto.OrderStatus))
            .Cast<CreateOrUpdateOrderDto.OrderStatus>()
            .Select(status => new SelectListItem
            {
                Value = status.ToString(),
                Text = status.ToString(),
                Selected = (status == order.Status) // Set the selected status based on the order
            })
            .ToList();

            // Set the SelectListItems in ViewBag
            ViewBag.OrderStatusOptions = orderStatusValues;
            ViewBag.User = user;
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(CreateOrUpdateOrderDto OrderDto)
        {
            
            try
            {
                var order = await _orderService.GetOrder(OrderDto.Id);
               
                order.Status = OrderDto.Status;
                if (ModelState.IsValid)
                {
                    
                    var Res = await _orderService.Update(order);
                    if (Res.IsSuccess)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var orderStatusValues = Enum.GetValues(typeof(CreateOrUpdateOrderDto.OrderStatus))
                       .Cast<CreateOrUpdateOrderDto.OrderStatus>()
                       .Select(status => new SelectListItem
                       {
                           Value = status.ToString(),
                           Text = status.ToString(),
                           Selected = (status == OrderDto.Status)
                       })
                       .ToList();
                                ViewBag.OrderStatusOptions = orderStatusValues;
                        var user = _userManager.Users.FirstOrDefault(u => u.Id == order.CustomerId)?.UserName;
                        ViewBag.User = user;
                        return  View("Edit", OrderDto); ;
                    }
                   
                }
                else
                {
                    var orderStatusValues = Enum.GetValues(typeof(CreateOrUpdateOrderDto.OrderStatus))
                       .Cast<CreateOrUpdateOrderDto.OrderStatus>()
                       .Select(status => new SelectListItem
                       {
                           Value = status.ToString(),
                           Text = status.ToString(),
                           Selected = (status == OrderDto.Status) // Set the selected status based on the order
                       })
                       .ToList();

                    // Set the SelectListItems in ViewBag
                    ViewBag.OrderStatusOptions = orderStatusValues;
                    return View("Edit",OrderDto);

                }
            }
            catch
            {
                var orderStatusValues = Enum.GetValues(typeof(CreateOrUpdateOrderDto.OrderStatus))
                .Cast<CreateOrUpdateOrderDto.OrderStatus>()
                .Select(status => new SelectListItem
                {
                    Value = status.ToString(),
                    Text = status.ToString(),
                    Selected = (status == OrderDto.Status) 
                })
                .ToList();
                ViewBag.OrderStatusOptions = orderStatusValues;
                return View("Edit",OrderDto);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
