using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using PaymentGateway.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Access
{
    public class PaymentCommandRepository : IPaymentCommandRepository
    {
        private readonly PaymentsGatewayDbContext _dbContext;

        public PaymentCommandRepository(PaymentsGatewayDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SavePaymentDetails(PaymentModel paymentDetails)
        {
            var paymentEntity = new PaymentEntity
            {
                PaymentDate = paymentDetails.PaymentDate,
                Amount = paymentDetails.Amount,
                SucessfullPayment = paymentDetails.SucessfullPayment,
                CardId = paymentDetails.CardId,
                Currency = paymentDetails.Currency,
                PaymentId = paymentDetails.PaymentId,
            };
            _dbContext.PaymentDetails.Add(paymentEntity);

            await _dbContext.SaveChangesAsync();
            return paymentEntity.Id;
        }

        public async Task<int> SaveCardDetails(CardModel cardDetails)
        {
            var cardEntity = new CardEntity
            {
                CardNumber = cardDetails.CardNumber,
                ExpiryDate = cardDetails.ExpiryDate,
                Cvv = cardDetails.Cvv,

            };
            _dbContext.CardDetails.Add(cardEntity);

            await _dbContext.SaveChangesAsync();
            return cardEntity.Id;
        }

    }
}

