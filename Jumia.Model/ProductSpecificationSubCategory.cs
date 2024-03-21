using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class ProductSpecificationSubCategory
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(subCategorySpecification))]
        public int SubSpecId { get; set; }
        public string Value { get; set; }
        public virtual Product Product { get; set; }
        public virtual SubCategorySpecification subCategorySpecification { get; set; }
        // [ForeignKey(nameof(Specification))]
        //public int SpecificationId { get; set; }
        //  public virtual SubCategory SubCategory { get; set; }
        //[ForeignKey(nameof(SubCategory))]
        //public int SubCategoryId { get; set; }
        //public virtual SubCategory SubCategory { get; set; }
    }
}
