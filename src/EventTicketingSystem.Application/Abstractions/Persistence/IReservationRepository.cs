using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservationEntity, CancellationToken cancellation = default);
        
        Task<Reservation?> GetByIdAsync(int reservationId, CancellationToken cancellation = default);

        Task<IReadOnlyList<int>> GetExpiredReservationIdsAsync(DateTime currentDateTime, CancellationToken cancellationToken = default);
    }
}
