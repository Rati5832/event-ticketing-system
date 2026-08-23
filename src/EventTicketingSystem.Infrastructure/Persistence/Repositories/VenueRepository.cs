using EventTicketingSystem.Application.Abstractions.Persistence;
using EventTicketingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.Infrastructure.Persistence.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly ApplicationDbContext _context;

        public VenueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Venue venue, CancellationToken cancellation = default)
        {
            await _context.Venues.AddAsync(venue, cancellation);
        }

        public async Task<IEnumerable<Venue>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Venues.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Venue?> GetByIdAsync(int id, CancellationToken cancellation = default)
        {
            return await _context.Venues.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id, cancellation);
        }
    }
}
