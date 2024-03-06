using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public  class Review:BaseEntity
    {
        public string Title {  get; set; }
        public string review { get; set; }
        public int? Rate { get; set; }
        [ForeignKey("Customer")] ////////
        public int CustomerId { get; set; }
        public UserIdentity Customer { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
