using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IVenueRepository
    {
        Task AddAsync(Venue venue, CancellationToken cancellation = default);

        Task<Venue?> GetByIdAsync(int id, CancellationToken cancellation = default);

        Task<List<Venue>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
