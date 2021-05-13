using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Common.Interfaces
{
    public interface IPaymentQueryRepository
    {
        PaymentModel GetPayment(int paymentReference);
        CardModel GetCard(int cardId);
    }
}
