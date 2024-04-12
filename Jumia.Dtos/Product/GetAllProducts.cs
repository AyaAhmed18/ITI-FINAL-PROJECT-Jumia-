using Microsoft.AspNetCore.Http;
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
        public string? NameAr { get; set; }
        public string? ShortDescription { get; set; }
        public int StockQuantity { get; set; }
        public decimal RealPrice { get; set; }
        public decimal? Discount { get; set; }
        public int? SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; } // Include for easier presentation
        public int? BrandID { get; set; }
        public string? BrandName { get; set; } // Include for easier presentation
        
        //public ICollection<IFormFile>? Images{ get; set; } // Simplified image representation
        public List<byte[]>? Images { get; set; }
        public GetAllProducts(Jumia.Model.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            NameAr=product.NameAr;
            StockQuantity = product.StockQuantity;
            ShortDescription = product.ShortDescription;
            RealPrice = product.RealPrice;
            Discount = product.Discount;
            SubCategoryID = product.SubCategoryId;
            BrandID = product.BrandId;
            
            // Include logic to populate SubCategoryName and BrandName based on your data access approach
            // (e.g., eager loading, separate queries)
            if (product.SubCategory != null)
            {
                SubCategoryName = product.SubCategory.Name;
            }
            if (product.Brand != null)
            {
                BrandName = product.Brand.Name;
            }
            Images = product.Images;
             }
        public GetAllProducts() { }
    }
}
