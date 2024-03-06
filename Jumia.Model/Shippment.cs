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
        public DateTime DateTime { get; set; }
        public string DelivaryWay { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Cost { get; set; }
        [ForeignKey("Order")] ////////
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
