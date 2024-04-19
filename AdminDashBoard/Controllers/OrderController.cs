using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Model;
using Jumia.Model.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml;
using System.Security.Cryptography;

namespace AdminDashBoard.Controllers
{
    //[Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> ExportToExcel()
        {
            
            var orders = await _orderService.GetAllOrders();

            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("orders");

            // Set column headers
            Worksheet.Cells[1, 1].Value = "Id";
            Worksheet.Cells[1, 2].Value = "Customer";
            Worksheet.Cells[1, 3].Value = "Status";
            Worksheet.Cells[1, 4].Value = "Payment";
            Worksheet.Cells[1, 5].Value = "CreatedDate";
            Worksheet.Cells[1, 6].Value = "TotalPrice";
            Worksheet.Cells[1, 7].Value = "Discount";




            // Populate the Excel worksheet with data from Categoryes
            int row = 2;
            foreach (var order in orders)
            {
                Worksheet.Cells[row, 1].Value = order.CustomerId;
                Worksheet.Cells[row, 2].Value = order.Customer;
                Worksheet.Cells[row, 3].Value = order.Status;
                Worksheet.Cells[row, 4].Value = order.paymentStatus;
                Worksheet.Cells[row, 5].Value = order.OrderDate;
                Worksheet.Cells[row, 6].Value = order.TotalOrderPrice;
                Worksheet.Cells[row, 7].Value = order.Discount;



                row++;
            }

            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
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
            var user = order.CustomerId;

            var userName = _userManager.Users.FirstOrDefault(u => u.Id == order.CustomerId)?.UserName;
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
            ViewBag.UserName = userName;
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(CreateOrUpdateOrderDto OrderDto)
        {
            var user = OrderDto.CustomerId;
            var userName = _userManager.Users.FirstOrDefault(u => u.Id == OrderDto.CustomerId)?.UserName;
            try
            {
                var order = await _orderService.GetOrder(OrderDto.Id);
               
                order.Status = OrderDto.Status;
                if (ModelState.IsValid)
                {
                    
                    var Res = await _orderService.Update(order);
                    if (Res.IsSuccess)
                    {
                        TempData["SuccessMessage1"] = "successfully.";
                        return RedirectToAction("Index", TempData["SuccessMessage1"]);
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
                        ViewBag.User = user;
                        ViewBag.UserName = userName;
                        TempData["SuccessMessage"] = "Failed";
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
                    ViewBag.User = user;
                    ViewBag.UserName = userName;
                    TempData["SuccessMessage"] = "Failed.";
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
                ViewBag.User = user;
                ViewBag.UserName = userName;
                TempData["SuccessMessage"] = "Failed";
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
