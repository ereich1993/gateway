using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Interfaces
{
    public interface IPaymentConverter
    {
        PaymentDetails ConvertToPaymentDetails(PaymentModel payment, CardModel card);
    }
}
