using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
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

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await _dbContext.Payments.AddAsync(payment, cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Payment?> GetByIdAsync(int paymentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Payments.Include(p => p.Booking).FirstOrDefaultAsync(p => p.Id == paymentId, cancellationToken);
        }
    }
}
