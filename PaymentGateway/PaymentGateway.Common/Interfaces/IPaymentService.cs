using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Common.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequest paymentRequest);
        PaymentDetails GetPaymentByReference(int paymentReference);

        bool ValidatePaymentDetails(PaymentRequest paymentRequest);
    }
}
