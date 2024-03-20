using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class ProductSpecificationSubCategory:BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(Specification))]

        public int SpecificationId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public string Value { get; set; }
        //[ForeignKey(nameof(SubCategory))]
        //public int SubCategoryId { get; set; }
        //public virtual SubCategory SubCategory { get; set; }
    }
}
