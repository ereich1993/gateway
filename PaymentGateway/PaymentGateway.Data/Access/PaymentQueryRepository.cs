using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentGateway.Data.Access
{
    public class PaymentQueryRepository : IPaymentQueryRepository
    {
        private readonly PaymentsGatewayDbContext _dbContext;

        public PaymentQueryRepository(PaymentsGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CardModel GetCard(int cardId)
        {
            var cardEntity = _dbContext.CardDetails.FirstOrDefault(p => p.Id == cardId);
            return new CardModel
            {
                CardNumber = cardEntity.CardNumber,
                ExpiryDate = cardEntity.ExpiryDate,
                Cvv = cardEntity.Cvv
            };
        }

        public PaymentModel GetPayment(int PaymentId)
        {
            var paymentEntity = _dbContext.PaymentDetails.FirstOrDefault(p => p.Id == PaymentId);
            return new PaymentModel
            {
                Amount = paymentEntity.Amount,
                SucessfullPayment = paymentEntity.SucessfullPayment,
                PaymentDate = paymentEntity.PaymentDate,
                CardId = paymentEntity.CardId,
                Currency = paymentEntity.Currency,
                PaymentId = paymentEntity.Id
            };
        }
    }
}
