using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface ISeatRepository
    {
        Task AddAsync(Seat seatEntity, CancellationToken cancellation = default);

        Task<bool> ExistsAsync(Seat seatEntity, CancellationToken cancellation = default);

        Task<IEnumerable<Seat>> GetAllByVenueAsync(int venueId, CancellationToken cancellationToken = default);

    }
}
