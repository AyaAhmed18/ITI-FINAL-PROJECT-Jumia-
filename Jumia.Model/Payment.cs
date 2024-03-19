using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Payment:BaseEntity
    {
        public PaymentType Type { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? PaymentDate { get; set; }
        [ForeignKey("Order")] ////////
        public int OrderId { get; set; }
    }
    public enum PaymentType
    {
        CreditCard,
        Cash,
        MobileMoney
    }
}
