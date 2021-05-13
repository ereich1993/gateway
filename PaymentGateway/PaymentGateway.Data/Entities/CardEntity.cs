using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Data.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
