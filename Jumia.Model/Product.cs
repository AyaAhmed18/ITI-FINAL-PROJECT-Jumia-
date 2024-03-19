using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string? LongDescription { get; set; }
        public string? ShortDescription { get; set; }
        public int StockQuantity { get; set; }
        public decimal RealPrice { get; set; }
        public decimal? Discount { get; set; }
        public HashSet<byte[]>? Images { get; set; }
        
        [ForeignKey("SubCategory")]
        public int SubCategoryID {  get; set; }
        public virtual SubCategory SubCategory { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<OrderItems>? OrderItems { get; set; }
        public virtual ICollection<ProductItems>? Items { get; set; }
        public Product() 
        {
           // Images= new List<string>();
            Reviews = new List<Review>();
            OrderItems = new List<OrderItems>();
        }
    }
}
