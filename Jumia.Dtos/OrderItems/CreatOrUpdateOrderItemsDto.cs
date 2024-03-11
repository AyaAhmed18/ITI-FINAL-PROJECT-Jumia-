using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.OrderItems
{
    public class CreatOrUpdateOrderItemsDto
    {
        public int Id { get; set; }
       // public string? CreatedBy { get; set; }
        public int ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
        [ForeignKey("Product")] ////////
        public int ProductId { get; set; }
    }
}
