using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Outbound
{
    public class PaymentDetails
    {
        public string MaskedCardNumber { get; set; }
        public int PaymentReference { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool SuccesfullPayment { get; set; }
    }
}
