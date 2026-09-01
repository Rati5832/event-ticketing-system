using EventTicketingSystem.Domain.Entities;

namespace EventTicketingSystem.Application.Abstractions.Persistence
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservationEntity, CancellationToken cancellation = default);
    }
}
