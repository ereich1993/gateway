using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentGateway.Api.Controllers;
using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Outbound;
using Xunit;

namespace PaymentGateway.Api.Tests
{
    public class PaymentsControllerShould
    {
        [Fact]
        public async Task ProcessPaymentWhenPaymentDetailsAreValid()
        {
            var builder = new Builder();
            var paymentRequest = builder.CreateNew<PaymentRequest>().Build();
            var paymentResponse = builder.CreateNew<PaymentResponse>().Build();

            var paymentServiceMock = new Mock<IPaymentService>();
            paymentServiceMock.Setup(a => a.ValidatePaymentDetails(paymentRequest)).Returns(true).Verifiable();
            paymentServiceMock.Setup(a => a.ProcessPayment(paymentRequest)).ReturnsAsync(paymentResponse).Verifiable();

            var paymentsController = new PaymentsController(paymentServiceMock.Object);

            var response = await paymentsController.ProcessPayment(paymentRequest);

            paymentServiceMock.VerifyAll();
            var dtoObject = Assert.IsType<OkObjectResult>(response);
            var paymentResults = Assert.IsType<PaymentResponse>(dtoObject.Value);
            paymentResults.Should().BeEquivalentTo(paymentResponse);

        }

        [Fact]
        public async Task ProcessPaymentReturnsBadRequestWhenDetailsAreInvalid()
        {
            var builder = new Builder();
            var paymentRequest = builder.CreateNew<PaymentRequest>().Build();
            var paymentServiceMock = new Mock<IPaymentService>();
            paymentServiceMock.Setup(a => a.ValidatePaymentDetails(paymentRequest)).Returns(false).Verifiable();

            var paymentsController = new PaymentsController(paymentServiceMock.Object);

            var response = await paymentsController.ProcessPayment(paymentRequest);
            paymentServiceMock.Verify();
            Assert.IsType<BadRequestObjectResult>(response);

        }

        [Fact]
        public async Task ProcessPaymentReturnsBadRequestWhenDetailsNull()
        {
            var builder = new Builder();
            var paymentRequest = builder.CreateNew<PaymentRequest>().Build();
            var paymentServiceMock = new Mock<IPaymentService>();

            var paymentsController = new PaymentsController(paymentServiceMock.Object);

            var response = await paymentsController.ProcessPayment(null);
            paymentServiceMock.Verify();
            Assert.IsType<BadRequestObjectResult>(response);

        }

        [Fact]
        public void GetPaymentByReference()
        {
            var reference = 123;
            var builder = new Builder();
            var paymentDetails = builder.CreateNew<PaymentDetails>().Build();
            var paymentServiceMock = new Mock<IPaymentService>();
            paymentServiceMock.Setup(a => a.GetPaymentByReference(reference)).Returns(paymentDetails).Verifiable();

            var paymentsController = new PaymentsController(paymentServiceMock.Object);

            var response = paymentsController.GetPaymentDetailsByReference(reference);
            paymentServiceMock.VerifyAll();
            var dtoObject = Assert.IsType<OkObjectResult>(response);
            var paymentResponse = Assert.IsType<PaymentDetails>(dtoObject.Value);
            paymentResponse.Should().BeEquivalentTo(paymentDetails);


        }
    }
}
