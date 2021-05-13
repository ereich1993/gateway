using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Common.ApiClientModels;
using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Models;
using PaymentGateway.Common.Outbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.BankSimulatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentResponseController : ControllerBase
    {
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("GetPaymentResult")]
        public  IActionResult GetPaymentResult([FromBody] PaymentRequest details)
        {
            Random rnd = new Random();
            var response = new BankResponse
            {
                IsSuccess = true,
                TransactionId = rnd.Next(1,100),
                PaymentTime = DateTime.Now
            };

            return Ok(response);
        }
    }
}
