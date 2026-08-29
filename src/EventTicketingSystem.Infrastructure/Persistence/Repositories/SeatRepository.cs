using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Seat seatEntity, CancellationToken cancellation = default)
        {
            await _context.Seats.AddAsync(seatEntity, cancellation);
        }

        public async Task<bool> ExistsAsync(Seat seatEntity, CancellationToken cancellation = default)
        {
            return !await _context.Seats.AsNoTracking().AnyAsync(s => 
            s.Section == seatEntity.Section && 
            s.Row == seatEntity.Row && 
            s.Number == seatEntity.Number &&
            s.VenueId == seatEntity.VenueId, 
            cancellation);
        }

        public async Task<IEnumerable<Seat>> GetAllByEventAsync(int eventId, CancellationToken cancellationToken = default)
        {
            return await _context.Seats
                .AsNoTracking()
                .Include(s => s.EventSeats.Where(es => es.EventId == eventId))
                .Where(s => s.EventSeats.Any(es => es.EventId == eventId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Seat>> GetAllByVenueAsync(int venueId, CancellationToken cancellationToken = default)
        {
            return await _context.Seats.AsNoTracking().Where(s => s.VenueId == venueId).ToListAsync(cancellationToken);
        }

        public async Task<Seat?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context.Seats.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellation);
        }
    }
}
