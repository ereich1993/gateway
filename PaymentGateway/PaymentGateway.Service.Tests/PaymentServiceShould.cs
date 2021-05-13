using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using System.Threading.Tasks;
using PaymentGateway.Common.Inbound;
using Moq;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.ApiClientModels;
using PaymentGateway.Common.Models;
using PaymentGateway.Services;
using Xunit;
using PaymentGateway.Common.Outbound;

namespace PaymentGateway.Service.Tests
{
    public class PaymentServiceShould
    {
        [Fact]
        public async Task ProcessPayment()
        {
            var builder = new Builder();
            var paymentRequest = builder.CreateNew<PaymentRequest>().Build();
            var bankResponse = builder.CreateNew<BankResponse>().Build();

            var apiClientMock = new Mock<IPaymentApiClient>();
            apiClientMock.Setup(p => p.SendPayment(It.IsAny<PaymentRequest>())).ReturnsAsync(bankResponse).Verifiable();

            var commandRepoMock = new Mock<IPaymentCommandRepository>();
            commandRepoMock.Setup(a => a.SaveCardDetails(It.IsAny<CardModel>())).ReturnsAsync(1).Verifiable();
            commandRepoMock.Setup(a => a.SavePaymentDetails(It.IsAny<PaymentModel>())).ReturnsAsync(5).Verifiable();

            var queryRepositoryMock = new Mock<IPaymentQueryRepository>();
            var paymentConverterMock = new Mock<IPaymentConverter>();

            var paymentService = new PaymentService(commandRepoMock.Object, queryRepositoryMock.Object, apiClientMock.Object, paymentConverterMock.Object);

            var result = await paymentService.ProcessPayment(paymentRequest);

            apiClientMock.Verify();
            commandRepoMock.VerifyAll();

            var paymentResults = Assert.IsType<PaymentResponse>(result);
            Assert.Equal(5, paymentResults.PaymentReference);
        }

        [Fact]
        public void GetsPaymentByReference()
        {
            var paymentReference = 5;
            var builder = new Builder();
            var paymentModel = builder.CreateNew<PaymentModel>().Build();
            var cardModel = builder.CreateNew<CardModel>().Build();
            var paymentDetails = builder.CreateNew<PaymentDetails>().Build();

            var apiClientMock = new Mock<IPaymentApiClient>();
            var commandRepoMock = new Mock<IPaymentCommandRepository>();
            var queryRepositoryMock = new Mock<IPaymentQueryRepository>();
            var paymentConverterMock = new Mock<IPaymentConverter>();

            queryRepositoryMock.Setup(a => a.GetPayment(paymentReference)).Returns(paymentModel).Verifiable();
            queryRepositoryMock.Setup(a => a.GetCard(paymentModel.CardId)).Returns(cardModel).Verifiable();

            paymentConverterMock.Setup(a => a.ConvertToPaymentDetails(It.IsAny<PaymentModel>(), It.IsAny<CardModel>())).Returns(paymentDetails).Verifiable();

            var paymentService = new PaymentService(commandRepoMock.Object, queryRepositoryMock.Object, apiClientMock.Object, paymentConverterMock.Object);

            var result = paymentService.GetPaymentByReference(paymentReference);

            queryRepositoryMock.VerifyAll();
            paymentConverterMock.Verify();

            Assert.IsType<PaymentDetails>(result);
        }


        [Fact]
        public void ValidatePaymentDetailsWhenValidDetails()
        {
            var validCardNumberOf16Characters = "1234567891123456";
            var builder = new Builder();
            var paymentModel = builder.CreateNew<PaymentRequest>().With(a => a.CardNumber = validCardNumberOf16Characters)
                .With(a => a.Cvv = "123").Build();

            var apiClientMock = new Mock<IPaymentApiClient>();
            var commandRepoMock = new Mock<IPaymentCommandRepository>();
            var queryRepositoryMock = new Mock<IPaymentQueryRepository>();
            var paymentConverterMock = new Mock<IPaymentConverter>();

            var paymentService = new PaymentService(commandRepoMock.Object, queryRepositoryMock.Object, apiClientMock.Object, paymentConverterMock.Object);

            var result = paymentService.ValidatePaymentDetails(paymentModel);

            Assert.True(result);
        }

        [Theory]
        [InlineData("X234567891123456", "123")]
        [InlineData("891123456", "123")]
        [InlineData("1234567891123456", "1a3")]
        [InlineData("1234567891123456", "1232")]
        [InlineData("1234567891123456", null)]
        [InlineData(null, "123")]
        public void ValidatePaymentDetailsWhenInvalidDetails(string cardNumber, string cvv)
        {
            var builder = new Builder();
            var paymentModel = builder.CreateNew<PaymentRequest>().With(a => a.CardNumber = cardNumber)
                .With(a => a.Cvv = cvv).Build();

            var apiClientMock = new Mock<IPaymentApiClient>();
            var commandRepoMock = new Mock<IPaymentCommandRepository>();
            var queryRepositoryMock = new Mock<IPaymentQueryRepository>();
            var paymentConverterMock = new Mock<IPaymentConverter>();

            var paymentService = new PaymentService(commandRepoMock.Object, queryRepositoryMock.Object, apiClientMock.Object, paymentConverterMock.Object);

            var result = paymentService.ValidatePaymentDetails(paymentModel);

            Assert.False(result);
        }
    }
}
