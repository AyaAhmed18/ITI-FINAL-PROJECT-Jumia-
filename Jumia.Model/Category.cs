using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public virtual ICollection<SubCategory> SubCategory { get; set; }
        public Category()
        {
            SubCategory = new List<SubCategory>();

        }
    }
}
