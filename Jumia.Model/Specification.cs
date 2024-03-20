using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Specification:BaseEntity
    {
        /*public int itemquantity { get; set; }
        public float? pro_weight { get; set; }
        public string? pro_size { get; set; }
        public string? color { get; set; }
        public int? storge { get; set; }  //1gb ....
        public hashset<byte[]>? images { get; set; }
        [foreignkey("product")]
        public int pro_id { get; set; }
        public product product { get; set; }*/
        [MaxLength(100)]
        public string Name { get; set; }
        //public virtual ICollection<Product> products { get; set; }
        //public virtual ICollection<SubCategory> SubCategory { get; set; }
        //[ForeignKey(nameof(SpecificationSubCategory))]
        //public int SpecificationSubCategoryId { get; set; }
        public virtual ICollection<SubCategory> SubCategory { get; set; }


    }
}
