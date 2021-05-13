using PaymentGateway.Common.ApiClientModels;
using PaymentGateway.Common.Inbound;
using System.Threading.Tasks;

namespace PaymentGateway.Common.Interfaces
{
    public interface IPaymentApiClient
    {
        Task<BankResponse> SendPayment(PaymentRequest payment);
    }
}
