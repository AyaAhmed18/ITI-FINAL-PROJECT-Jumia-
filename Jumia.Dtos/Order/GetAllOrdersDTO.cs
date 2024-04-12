using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Dtos.Order
{
    public class GetAllOrdersDTO
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public PaymentStatus paymentStatus { get; set; }
       // public string PaymentTStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public int? Discount { get; set; }
        public int? TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public enum PaymentStatus
        {
            Pending = 0,
            PayPall = 1,
            MobileMoney = 2,
            Cash = 3
        }
       /* public string GetPaymentStatusWord()
        {
            switch (paymentStatus)
            {
                case PaymentStatus.Pending:
                    return "Pending";
                case PaymentStatus.PayPall:
                    return "PayPall";
                case PaymentStatus.MobileMoney:
                    return "Mobile Money";
                case PaymentStatus.Cash:
                    return "Cash";
                default:
                    return "UNConfirmed";
            }
        }*/
        public string GetPaymentStatusWord()
        {
            switch (paymentStatus)
            {
                case 0:
                    return "Pending";
                case (PaymentStatus)1:
                    return "PayPall";
                case (PaymentStatus)2:
                    return "Mobile Money";
                case (PaymentStatus)3:
                    return "Cash";
                default:
                    return "UNConfirmed";
            }
        }
    }
}
