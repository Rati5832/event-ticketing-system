using EventTicketingSystem.Application.Features.Payments.CreatePayment;
using EventTicketingSystem.Application.Features.Payments.GetPayments;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly CreatePaymentCommandHandler _createPaymentCommandHandler;
        private readonly GetPaymentsRequestHandler _getPaymentsRequestHandler;

        public PaymentsController(
            CreatePaymentCommandHandler createPaymentCommandHandler,
            GetPaymentsRequestHandler getPaymentsRequestHandler)
        {
            _createPaymentCommandHandler = createPaymentCommandHandler;
            _getPaymentsRequestHandler = getPaymentsRequestHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var paymentResponse = await _createPaymentCommandHandler.HandleAsync(command, cancellationToken);
            return StatusCode(201, paymentResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var payments = await _getPaymentsRequestHandler.HandleAsync(cancellationToken);
            return Ok(payments);
        }
    }
}
