using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment, CancellationToken cancellationToken);

        Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken);

        Task<Payment?> GetByIdAsync(int paymentId, CancellationToken cancellationToken);

        Task<bool> HasPendingPaymentAsync(int bookingId, CancellationToken cancellationToken);
    }
}
