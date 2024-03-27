using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.ProductSpecificationSubCategory
{
    public class CreateOrUpdateProductSpecificationSubCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SubSpecId { get; set; }
       // public int SpecificationId { get; set; }
        public string Value { get; set; }

        //public string SpecificationName { get; set; }
        //public string SubCategoryName { get; set; }
    }
}
