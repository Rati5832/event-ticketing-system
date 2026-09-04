using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class EventSeatRepository : IEventSeatRepository
    {
        private readonly ApplicationDbContext _context;

        public EventSeatRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AssignSeatToEventAsync(EventSeat eventSeat, CancellationToken cancellationToken)
        {
            await _context.EventSeats.AddAsync(eventSeat, cancellationToken);
        }

        public async Task<bool> EventAndSeatExistsAsync(int eventId, int seatId, CancellationToken cancellationToken)
        {
            return await _context.EventSeats.AsNoTracking().AnyAsync(es =>
            es.EventId == eventId &&
            es.SeatId == seatId,
            cancellationToken);
        }

        public async Task<EventSeat?> GetEventSeatById(int eventSeatId, CancellationToken cancellationToken)
        {
            return await _context.EventSeats.FirstOrDefaultAsync(es => es.Id == eventSeatId, cancellationToken);
        }
    }
}
