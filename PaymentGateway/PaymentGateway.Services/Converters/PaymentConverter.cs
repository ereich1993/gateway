using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Services.Converters
{
    public class PaymentConverter : IPaymentConverter
    {
       

        public PaymentDetails ConvertToPaymentDetails(PaymentModel payment, CardModel card)
        {
            var paymentDetails = new PaymentDetails
            {
                PaymentDate = payment.PaymentDate,
                MaskedCardNumber = HideCardNumber(card.CardNumber),
                PaymentReference = payment.PaymentId,
                SuccesfullPayment = payment.SucessfullPayment
            };
            return paymentDetails;
        }




        /// <summary>
        /// This method should return a masked card number in this case the last four digit
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        private string HideCardNumber(string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 5, 4);
        }
    }
}
