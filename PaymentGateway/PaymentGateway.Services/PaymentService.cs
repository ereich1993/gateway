using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentCommandRepository _commandRepo;
        private readonly IPaymentQueryRepository _queryRepository;
        private readonly IPaymentApiClient _paymentApiClient;
        private readonly IPaymentConverter _paymentConverter;

        public PaymentService(IPaymentCommandRepository commandRepo, IPaymentQueryRepository queryRepository, IPaymentApiClient paymentApiClient, IPaymentConverter paymentConverter)
        {
            _commandRepo = commandRepo;
            _queryRepository = queryRepository;
            _paymentApiClient = paymentApiClient;
            _paymentConverter = paymentConverter;
        }

        public PaymentDetails GetPaymentByReference(int paymentReference)
        {
            var payment =  _queryRepository.GetPayment(paymentReference);
            var card = _queryRepository.GetCard(payment.CardId);

           var paymentDetails = _paymentConverter.ConvertToPaymentDetails(payment, card);
           return paymentDetails;
        }

        public async Task<PaymentResponse> ProcessPayment(PaymentRequest payment)
        {
            var response = await _paymentApiClient.SendPayment(payment);
            
            var cardId = await _commandRepo.SaveCardDetails(new CardModel {ExpiryDate = payment.ExpiryDate, CardNumber = payment.CardNumber, Cvv = payment.Cvv });
            var PaymentId = await _commandRepo.SavePaymentDetails(new PaymentModel {SucessfullPayment = response.IsSuccess, Amount = payment.Amount, CardId = cardId, Currency = payment.Currency, PaymentId = response.TransactionId, PaymentDate = response.PaymentTime });
            

            var responseMessage = new PaymentResponse
            {
                PaymentReference = PaymentId,
                SucessfullTransaction = response.IsSuccess
            };

            return responseMessage;
        }

        public bool ValidatePaymentDetails(PaymentRequest paymentRequest)
        {
            int cardLength = 16;
            //CVV check
            if (paymentRequest.Cvv?.Length != 3 || !paymentRequest.Cvv.All(char.IsDigit))
            {
                return false;
            }
            //CardCheck
            if (paymentRequest.CardNumber?.Length != cardLength || !paymentRequest.CardNumber.All(char.IsDigit))
            {
                return false;
            }
            return true;
        }
    }
}
