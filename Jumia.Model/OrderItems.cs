using Jumia.Model.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class OrderItems:LocalizableEntity
    {
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
        [ForeignKey("Product")] ////////
        public int ProductId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


    }
}
