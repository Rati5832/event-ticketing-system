using EventTicketingSystem.Application.Features.Payments.Common;
using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment, CancellationToken cancellationToken = default);

        Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken);
    }
}
