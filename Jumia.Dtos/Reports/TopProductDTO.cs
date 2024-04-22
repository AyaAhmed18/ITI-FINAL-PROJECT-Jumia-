using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Reports
{
    public class TopProductDTO
    {
        public string ProductName { get; set; }
        public int UnitsSold { get; set; }
        public string OrderStatus { get; set; }
    }
}
