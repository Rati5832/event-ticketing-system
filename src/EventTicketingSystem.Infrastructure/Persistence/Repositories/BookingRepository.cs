using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Booking booking, CancellationToken cancellationToken)
        {
            await _dbContext.Bookings.AddAsync(booking, cancellationToken);
        }

        public async Task<bool> ExistsByReservationIdAsync(int reservationId, CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings.AsNoTracking().AnyAsync(b => b.ReservationId == reservationId, cancellationToken);
        }

        public async Task<Booking?> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .Include(b => b.Reservation)
                .ThenInclude(r => r.EventSeat)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<IEnumerable<Booking>> GetBooksAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Bookings
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
