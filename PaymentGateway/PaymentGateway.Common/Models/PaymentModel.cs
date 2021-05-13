using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Models
{
    public class PaymentModel
    {
        public int PaymentId { get; set; }
        public bool SucessfullPayment { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
