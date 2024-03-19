﻿using Jumia.Model.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Order: LocalizableEntity
    {
        public int TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
        public string Status { get; set; } //= "Processing";
        public bool? Shipped { get; set; }
        public DateTime? ShippedDate { get; set;}
        public bool? Delivered { get; set;}
        public DateTime? DeliveredDate { get; set; }
        public bool? CancelOrder { get; set; }

        [ForeignKey("Customer")] ////////
        public int CustomerId { get; set; }
        public UserIdentity Customer { get; set; }
        [ForeignKey("payment")] ////////
        public int PaymentId { get; set; }
        public Payment payment { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
        public virtual Shippment Shipping { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItems>();
            Status = "Processing";
        }

        /* public enum OrderStatus
         {
             Processing ,
             Shipped ,
             Delivered ,
             Canceled
         }*/

    }
}
