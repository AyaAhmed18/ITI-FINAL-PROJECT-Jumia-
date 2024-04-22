using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Reports
{
    public class OrdersPerMonthDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
