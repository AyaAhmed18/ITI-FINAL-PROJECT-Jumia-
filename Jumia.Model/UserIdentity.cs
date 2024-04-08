using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class UserIdentity : IdentityUser<int>
    {
        public virtual ICollection<Order>? orders { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Shippment>? Shippments { get; set; }
        //public string Adress {  get; set; }
        public UserIdentity()
        {
            orders=new List<Order>();
            Reviews = new List<Review>();
            Shippments=new List<Shippment>();
        }
    }
   
}
