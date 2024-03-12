using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Customer { get; set; }
        public int Payment { get; set; }
        public int shippment { get; set; }


        public enum OrderStatus
        {
            Processing ,
            Shipped ,
            Delivered ,
            Canceled
        }
    }
}
