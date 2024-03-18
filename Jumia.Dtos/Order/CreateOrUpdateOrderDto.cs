using Localization.Shared_Recources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Jumia.Dtos.Order
{
    public class CreateOrUpdateOrderDto
    {
        public int Id { get; set; }
        public int TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? Discount { get; set; }
        public OrderStatus Status { get; set; }  //for admin only
        public bool? Shipped { get; set; } //for admin only
        public DateTime? ShippedDate { get; set; } 
        public bool? Delivered { get; set; } //for admin only
        public DateTime? DeliveredDate { get; set; } 
        public bool? CancelOrder { get; set; }
        public int CustomerId { get; set; }
       // public string Customer { get; set; }
        public int PaymentId { get; set; }
        public int shippment { get; set; }

        private readonly IStringLocalizer<SharedRecources> _localizer;
        public enum OrderStatus
        {
            Processing ,
            Shipped ,
            Delivered ,
            Canceled,
            Returned
        }
        public string GetLocalizedStatus()
        {
            switch (Status)
            {
                case OrderStatus.Processing:
                    return _localizer["Processing"];
                case OrderStatus.Shipped:
                    return _localizer["Shipped"];
                case OrderStatus.Delivered:
                    return _localizer["Delivered"];
                case OrderStatus.Canceled:
                    return _localizer["Canceled"];
                case OrderStatus.Returned:
                    return _localizer["Returned"];
                default:
                    return "";
            }
        }
    }
}
