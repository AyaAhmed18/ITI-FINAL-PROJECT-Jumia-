using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Model
{
    public class Payment:BaseEntity
    {
        public string Type { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? PaymentDateTime { get; set; }
    }
}
