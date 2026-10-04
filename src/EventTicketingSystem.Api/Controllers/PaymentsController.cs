using EventTicketingSystem.Application.Features.Payments.CreatePayment;
using EventTicketingSystem.Application.Features.Payments.GetPayments;
using EventTicketingSystem.Application.Features.Payments.ProcessPayment;
using Microsoft.AspNetCore.Mvc;

namespace EventTicketingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly CreatePaymentCommandHandler _createPaymentCommandHandler;
        private readonly GetPaymentsRequestHandler _getPaymentsRequestHandler;
        private readonly ProcessPaymentCommandHandler _processPaymentCommandHandler;

        public PaymentsController(
            CreatePaymentCommandHandler createPaymentCommandHandler,
            GetPaymentsRequestHandler getPaymentsRequestHandler,
            ProcessPaymentCommandHandler processPaymentCommandHandler)
        {
            _createPaymentCommandHandler = createPaymentCommandHandler;
            _getPaymentsRequestHandler = getPaymentsRequestHandler;
            _processPaymentCommandHandler = processPaymentCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var paymentResponse = await _createPaymentCommandHandler.HandleAsync(command, cancellationToken);
            return StatusCode(201, paymentResponse);
        }

        [HttpPost("{paymentId:int}/process")]
        public async Task<IActionResult> Process(int paymentId, ProcessPaymentRequest request, CancellationToken cancellationToken)
        { 
            var process =  await _processPaymentCommandHandler.HandleAsync(paymentId, request, cancellationToken);
            return Ok(process);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var payments = await _getPaymentsRequestHandler.HandleAsync(cancellationToken);
            return Ok(payments);
        }
    }
}
