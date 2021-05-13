using Newtonsoft.Json;
using PaymentGateway.Common.ApiClientModels;
using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.ApiClient
{
    public class PaymentApiClient : IPaymentApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public PaymentApiClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<BankResponse> SendPayment(PaymentRequest payment)
        {
            var content = JsonConvert.SerializeObject(payment);
            var client = _clientFactory.CreateClient();
            var result = await client.PostAsync("https://localhost:44361/api/PaymentResponse/GetPaymentResult", new StringContent(content, Encoding.UTF8, "application/json"));

            var ctn = JsonConvert.DeserializeObject<BankResponse>(await result.Content.ReadAsStringAsync());

            return ctn;

        }
    }
}
