using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;

namespace Jumia.Context
{
    
    public class JumiaContext : IdentityDbContext<UserIdentity, UserRole, int>
{
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<Shippment> Shippments { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        //public virtual DbSet<SpecificationSubCategory> SpecificationSubCategories { get; set; }
        public virtual DbSet<ProductSpecificationSubCategory> ProductSpecificationSubCategory { get; set; }
        public virtual DbSet<SubCategorySpecification> SubCategorySpecifications { get; set; }

        public JumiaContext(DbContextOptions<JumiaContext> dbContextOptions) : base(dbContextOptions) { }


        /*
           public JumiaContext()
         {
         }
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlServer("Data Source=DESKTOP-C1VVAHL\\SQLEXPRESS;Initial Catalog=JumiaStore;Integrated Security=True;Encrypt=False");//.UseLazyLoadingProxies();
             optionsBuilder.EnableSensitiveDataLogging();
         }*/
    }
}
