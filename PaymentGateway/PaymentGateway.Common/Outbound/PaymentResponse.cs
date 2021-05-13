using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Outbound
{
    public class PaymentResponse
    {
        public bool SucessfullTransaction { get; set; }
        public int PaymentReference { get; set; }
    }
}
