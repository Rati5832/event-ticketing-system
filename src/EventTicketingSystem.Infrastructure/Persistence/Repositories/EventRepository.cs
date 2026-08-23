using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;

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
    }
}
