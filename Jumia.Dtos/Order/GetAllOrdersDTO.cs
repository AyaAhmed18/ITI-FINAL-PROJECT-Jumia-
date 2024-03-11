using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Order
{
    public class GetAllOrdersDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
    }
}
