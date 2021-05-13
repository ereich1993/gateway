using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Models
{
    public class CardModel
    {
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
