using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Event eventEntity, CancellationToken cancellation = default)
        {
            await _context.Events.AddAsync(eventEntity, cancellation);
        }

        public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Events.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellation);
        }
    }
}
