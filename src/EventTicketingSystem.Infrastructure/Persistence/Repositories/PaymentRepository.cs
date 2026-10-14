using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PaymentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken)
        {
            await _dbContext.Payments.AddAsync(payment, cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Payment?> GetByIdAsync(int paymentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Payments
                .Include(p => p.Booking)
                .ThenInclude(p => p.Reservation)
                .ThenInclude(p => p.EventSeat)
                .FirstOrDefaultAsync(p => p.Id == paymentId, cancellationToken);
        }

        public async Task<bool> HasPendingPaymentAsync(int bookingId, CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.AsNoTracking().AnyAsync(p => p.BookingId == bookingId && p.Status == PaymentStatus.Pending, cancellationToken);
        }
    }
}
