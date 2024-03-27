using Jumia.Model.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Product: LocalizableEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string? LongDescription { get; set; }
        public string? ShortDescription { get; set; }
        public int StockQuantity { get; set; }
        public decimal RealPrice { get; set; }
        public decimal? Discount { get; set; }
        public List<byte[]>? Images { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        //[ForeignKey(nameof(ProductSpecificationSubCategory))]
        //public int ProductSpecificationSubCategoryId { get; set; }
        public virtual ICollection<ProductSpecificationSubCategory> ProductSpecificationSubCategory { get; set; }
       //  public virtual ICollection<SubCategorySpecification>? SubCategorySpecifications { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<OrderItems>? OrderItems { get; set; }
       
        public Product() 
        {
           // Images= new List<string>();
            Reviews = new List<Review>();
            OrderItems = new List<OrderItems>();
        }
    }
}
