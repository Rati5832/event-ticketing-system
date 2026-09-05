using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Application.Features.Payments.Common;

namespace EventTicketingSystem.Application.Features.Payments.GetPayments
{
    public class GetPaymentsRequestHandler
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentsRequestHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllPaymentsAsync(cancellationToken);
            return payments.Select(payment => new PaymentResponse
            {
                Id = payment.Id,
                BookingId = payment.BookingId,
                Amount = payment.Amount,
                Status = payment.Status,
                Provider = payment.Provider,
                TransactionId = payment.TransactionId,
                CreatedAt = payment.CreatedAt,
                CompletedAt = payment.CompletedAt
            });
        }
    }
}
