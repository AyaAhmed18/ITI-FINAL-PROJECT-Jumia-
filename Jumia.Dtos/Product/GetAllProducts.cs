using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Product
{
    public class GetAllProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
    }
}
