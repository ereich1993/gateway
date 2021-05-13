using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Common.Inbound;
using PaymentGateway.Common.Interfaces;
using PaymentGateway.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest details)
        {
            if (details == null || !_paymentService.ValidatePaymentDetails(details))
            {
                return BadRequest(ModelState);
            }
            var result = await _paymentService.ProcessPayment(details);

            return Ok(result);
        }

        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("GetPayment/{paymentRefrence}")]
        public  IActionResult GetPaymentDetailsByReference(int paymentRefrence)
        {
           var result = _paymentService.GetPaymentByReference(paymentRefrence);
            return Ok(result);
        }
    }
}
