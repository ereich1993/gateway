using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Inbound
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
