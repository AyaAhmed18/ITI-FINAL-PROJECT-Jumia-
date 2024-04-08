using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class SubCategorySpecification
    {
        public int Id { get; set; }
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(Specification))]
        public int specificationId { get; set; }
        public SubCategory SubCategory { get; set; }
        public Specification Specification { get; set; }

        public virtual ICollection<ProductSpecificationSubCategory> ProductSpecificationSubCategory { get; set; }



    }
}
