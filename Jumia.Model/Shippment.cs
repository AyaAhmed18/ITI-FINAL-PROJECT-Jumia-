using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Jumia.Model
{
    public class Shippment : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AdressInformation { get; set; }
        public string Regin { get; set; }
        public string City { get; set; }
        public decimal Cost { get; set; }
        [ForeignKey("Order")] ////////
        public int OrderId { get; set; }
        //public virtual ICollection<Order> Order { get; set; }
       

    }
    }
