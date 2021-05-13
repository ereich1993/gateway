using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.ApiClientModels
{
    public class BankResponse
    {
        public int TransactionId { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime PaymentTime { get; set; }
    }
}
