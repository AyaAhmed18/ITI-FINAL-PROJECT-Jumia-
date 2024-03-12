using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;

namespace AdminDashBoard.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        public OrderController(IOrderService orderService,IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
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
            if (_orderItemService != null)
            {
                var ordersDetails = await _orderItemService.GetAllOrderItems();
                var items =  ordersDetails.Where(i => i.OrderId == id);
                return View(items);
            }
            return View();
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
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, CreateOrUpdateOrderDto Status)
        {
            var order = await _orderService.GetOrder(id);
            try
            {
                if (ModelState.IsValid)
                {
                    order.Status = Status.Status;
                    var Res = await _orderService.Update(order);
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
                           Selected = (status == order.Status) // Set the selected status based on the order
                       })
                       .ToList();

                    // Set the SelectListItems in ViewBag
                    ViewBag.OrderStatusOptions = orderStatusValues;
                    return View("Update", order);

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
                    Selected = (status == order.Status) 
                })
                .ToList();
                ViewBag.OrderStatusOptions = orderStatusValues;
                return View("Update");
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
