using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class SubCategory:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public SubCategory()
        {
           // Images = new List<string>();
            Products = new List<Product>();

        }
    }
}
