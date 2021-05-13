using PaymentGateway.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Common.Interfaces
{
    public interface IPaymentCommandRepository
    {
        Task<int> SavePaymentDetails(PaymentModel paymentDetails);
        Task<int> SaveCardDetails(CardModel cardDetails);
    }
}
