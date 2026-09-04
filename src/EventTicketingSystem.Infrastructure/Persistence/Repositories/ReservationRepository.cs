using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using EventTicketingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservationEntity, CancellationToken cancellation = default)
        {
            await _context.Reservations.AddAsync(reservationEntity, cancellation);
        }

        public async Task<IReadOnlyList<int>> GetExpiredReservationIdsAsync(DateTime currentDateTime, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations.Where(
                r => r.Status == ReservationStatus.Active &&
                r.ExpiresAt < currentDateTime &&
                (r.Booking == null || r.Booking.Status != BookingStatus.Pending)).Select(r => r.Id).ToListAsync(cancellationToken);
        }

        public async Task<Reservation?> GetByIdAsync(int reservationId, CancellationToken cancellation = default)
        {
            return await _context.Reservations.Include(r => r.EventSeat).FirstOrDefaultAsync(r => r.Id == reservationId, cancellation);
        }
    }
}
